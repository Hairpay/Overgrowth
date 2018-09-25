using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimaCameraScirpt : MonoBehaviour {

    public GameObject character;
    public GameObject directioneur;
    public Gestionnaire gestionnaire;

    public Vector2 camOffset;
    [Range( 0.01f, 5.0f )]
    public float lerpSpeed;

    public float distance = 20f;
    public float distanceTop = 11f;
    public float distanceBot = 10f;

    // Si tu as besoin de certaines variables privées en dehors de ce script, regarde plus bas dans "Properties" pour voir comment il faut faire

   // private float dirAngle;
   // private float dirAngleY;

    private bool isLookingRight;
    private bool isLookingBot;
    private bool wasLookingRight;
    private bool wasLookingBot;

    private Vector2 charPos;
    private Vector2 basePos;
    private Vector2 camPos;
    private Vector2 newPos;

    private Vector2 rightPos;
    private Vector2 leftPos;

    private int layerMaskWalls;
    private int layerMaskGround;
    private int layerMaskCeiling;

    private bool isCollidingLeft;
    private bool isCollidingRight;
    private bool isCollidingTop;
    private bool isCollidingBot;

    private float t;

    // Properties
    public bool IsLookingRight { get { return isLookingRight; } }

    void Start()
    {
        t = lerpSpeed;
        basePos = new Vector2(-camOffset.x, camOffset.y);
        character = GameObject.Find("character");
        gameObject.transform.position = character.transform.position;
        gestionnaire = character.GetComponent<PowerUps>().Gestionnaire;

        directioneur = GameObject.Find("Directioneur");
        layerMaskWalls = LayerMask.GetMask("CameraWall");
        layerMaskGround = LayerMask.GetMask("CameraGround");
        layerMaskCeiling = LayerMask.GetMask("CameraCeiling");
    }
    
    void Update () {
        //dirAngleY = directioneur.transform.localRotation.z;
        //dirAngle = Mathf.Abs( dirAngleY );
        charPos = character.transform.position;
        camPos = gameObject.transform.position;

        leftPos = new Vector2( charPos.x + basePos.x, charPos.y + basePos.y + camOffset.x * 0.5f);
        rightPos = new Vector2( charPos.x - basePos.x, charPos.y + basePos.y + camOffset.y * 0.5f );

        wasLookingRight = isLookingRight;
        wasLookingBot = isLookingBot;
        
        Debug.DrawRay( transform.position, Vector2.left * distance );

        CheckCollisions();
        CheckCameraOffset();
        LerpCamera();
    }

    ///<summary>Checks for any collision with boundaries</summary>
    private void CheckCollisions()
    {
        RaycastHit2D left = Physics2D.Raycast( transform.position, Vector2.left, distance, layerMaskWalls );
        Debug.DrawRay( transform.position, Vector2.left * distance );

        RaycastHit2D right = Physics2D.Raycast( transform.position, Vector2.right, distance, layerMaskWalls );
        Debug.DrawRay( transform.position, Vector2.right * distance );

        RaycastHit2D top = Physics2D.Raycast( transform.position, Vector2.up, distanceTop, layerMaskCeiling );
        Debug.DrawRay( transform.position, Vector2.up * distanceTop );

        RaycastHit2D bot = Physics2D.Raycast( transform.position, Vector2.down, distanceBot, layerMaskGround );
        Debug.DrawRay( transform.position, Vector2.down * distanceBot );

        if( isCollidingLeft = left.collider )
            camPos.x = left.collider.gameObject.transform.position.x + distance + 1;

        if( isCollidingRight = right.collider )
            camPos.x = right.collider.gameObject.transform.position.x - distance - 1;

        if( isCollidingBot = bot.collider )
            camPos.y = bot.collider.gameObject.transform.position.y + distanceBot + 1;

        if( isCollidingTop = top.collider )
            camPos.y = top.collider.gameObject.transform.position.y - distanceTop - 1;
    }

    ///<summary>Checks where the camera should look using mouse position</summary>
    private void CheckCameraOffset()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );

        if( !gestionnaire.isGlinding )
        {
            if( mousePos.x > charPos.x + camOffset.x )
            {
                isLookingRight = true;
            }
            else if( mousePos.x < charPos.x - camOffset.x )
            {
                isLookingRight = false;
            }
        }
        else
        {
            isLookingRight = gestionnaire.GlideGauche;
        }

        if( mousePos.y > charPos.y + camOffset.y )
        {
            isLookingBot = false;
        }
        else if( mousePos.y < charPos.y - camOffset.y )
        {
            isLookingBot = true;
        }
    }

    ///<summary>Lerps the camera from its actual position to a new position</summary>
    private void LerpCamera()
    {
        if( isLookingRight )
        {
            newPos = rightPos;
        }
        else
        {
            newPos = leftPos;
        }

        if( isLookingBot )
        {
            newPos.y = rightPos.y - camOffset.y * 2;
        }
        else
        {
            newPos.y = rightPos.y;
        }

        if( isLookingRight != wasLookingRight || isLookingBot != wasLookingBot )
        {
            t = 0.0f;
        }

        CheckBoundaries();

        gameObject.transform.position = new Vector2( Mathf.Lerp( camPos.x, newPos.x, t ), Mathf.Lerp( camPos.y, newPos.y, t ) );
        if( t < lerpSpeed )
        {
            t += lerpSpeed * Time.deltaTime;
        }
    }

    ///<summary>Checks if the camera isn't out of the boundaries</summary>
    private void CheckBoundaries()
    {
        if( isCollidingLeft && newPos.x < camPos.x )
        {
            newPos.x = camPos.x;
        }
        else if( isCollidingRight && newPos.x > camPos.x && !isCollidingLeft )
        {
            newPos.x = camPos.x;
        }

        if( isCollidingTop && newPos.y > camPos.y && !isCollidingBot )
        {
            newPos.y = camPos.y;
        }
        else if( isCollidingBot && newPos.y < camPos.y )
        {
            newPos.y = camPos.y;
        }
    }
}