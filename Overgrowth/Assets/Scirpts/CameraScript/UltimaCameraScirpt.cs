using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimaCameraScirpt : MonoBehaviour {

    public GameObject Character;
    public GameObject Directioneur;
    public float Directiangle;
    public float DirectiangleY;

    public bool direction;
    public bool lastDirection;

    public bool directionY;
    public bool lastDirectionY;

    private Vector3 CharPos;
    private Vector3 basePos;
    public Vector3 actualPos;
    private Vector3 newPos;

    private Vector3 rightPos;
    private Vector3 leftPos;

    private int layer_mask;
    private int layer_maskG;
    private int layer_maskC;
    public float distance = 20f;
    public float distanceUp = 11f;
    public float distanceBot = 10f;

    public bool touchLeft;
    public bool touchRight;
    public bool touchUp;
    public bool touchBot;

    public float t = 2f;
    public Gestionnaire Gestionnaire;
   
    // Use this for initialization
    void Start()
    {
        Character = GameObject.Find("character");
        Gestionnaire = Character.GetComponent<PowerUps>().Gestionnaire;

        Directioneur = GameObject.Find("Directioneur");
        basePos = gameObject.transform.localPosition;
        layer_mask = LayerMask.GetMask("CameraWall");
        layer_maskG = LayerMask.GetMask("CameraGround");
        layer_maskC = LayerMask.GetMask("CameraCeiling");
    }

    // Update is called once per frame
    void Update () {

        // IMPORTANT: si le plafond n'est pas assez haut, le raycast de la gauche et droite vont se coller dessus ! ( effets imprévisibles)
        // NOTE: éviter de mettre des fermetures d'ecran de chaque coté.
        Directiangle = Directioneur.transform.localRotation.z;
        DirectiangleY = Directiangle;
        Directiangle = Mathf.Abs(Directiangle);
        CharPos = Character.transform.position;
        actualPos = gameObject.transform.position;

        leftPos = new Vector3(CharPos.x + basePos.x, CharPos.y + basePos.y, CharPos.z + basePos.z);
        rightPos = new Vector3(CharPos.x - basePos.x, CharPos.y + basePos.y, CharPos.z + basePos.z);

        lastDirection = direction;
        lastDirectionY = directionY;

        RaycastHit2D left = Physics2D.Raycast(gameObject.transform.position, Vector2.left, distance, layer_mask);
        Debug.DrawLine(gameObject.transform.position, left.point);

        // raycasts
        if (left.collider != null)
        {       
            touchLeft = true;
            if (actualPos.x < left.collider.gameObject.transform.position.x + distance)
            {
                actualPos.x = left.collider.gameObject.transform.position.x + distance +1;
            }         
        }
        else
        {          
            touchLeft = false;
        }

        RaycastHit2D right = Physics2D.Raycast(gameObject.transform.position, Vector2.right, distance, layer_mask);
        Debug.DrawLine(gameObject.transform.position, right.point);

        if (right.collider != null)
        {        
            touchRight = true;
            if (actualPos.x > right.collider.gameObject.transform.position.x - distance && touchLeft == false)
            {            
                actualPos.x = right.collider.gameObject.transform.position.x - distance - 1;
            }
        }
        else
        {        
            touchRight = false;
        }
     
        RaycastHit2D bot = Physics2D.Raycast(gameObject.transform.position, Vector2.down, distanceBot, layer_maskG);
        Debug.DrawLine(gameObject.transform.position, bot.point);

        if (bot.collider != null)
        {
            touchBot = true;
            if (actualPos.y < bot.collider.gameObject.transform.position.y + distanceBot)
            {          
                actualPos.y = bot.collider.gameObject.transform.position.y + distanceBot +1;
            }
        }
        else
        {
            touchBot = false;
        }

        RaycastHit2D Up = Physics2D.Raycast(gameObject.transform.position, Vector2.up, distanceUp, layer_maskC);
        Debug.DrawLine(gameObject.transform.position, Up.point);

        if (Up.collider != null)
        {
            touchUp = true;
            if (actualPos.y > Up.collider.gameObject.transform.position.y - distanceUp && touchBot == false)
            {
                actualPos.y = Up.collider.gameObject.transform.position.y - distanceUp - 1;
            }
        }
        else
        {
            touchUp = false;
        }

        //end of raycasts

        // x axis switching
        if (Gestionnaire.isGlinding == false)
        {
            if (Directiangle < 0.55f)
            {
                direction = true;
            }
            if (Directiangle > 0.85f)
            {
                direction = false;
            }
        }
        else
        {
            if (Gestionnaire.directGlide < -1)
            {
                direction = false;
            }
            else if(Gestionnaire.directGlide > 1)
            {
                direction = true;
            }
        }
            
        
        if(direction == true)
        {
            newPos = rightPos;
        }
        else
        {
            newPos = leftPos;
        } 

        if(direction != lastDirection)
        {
            t = 0.0f;
        }

        // Y axis switching
        if (DirectiangleY < -0.2f)
        {
            directionY = true;
        }
        if (DirectiangleY > 0.2f)
        {
            directionY = false;
        }

        if (directionY == true)
        {
            newPos.y = rightPos.y - 6;
        }
        else
        {
            newPos.y = rightPos.y;
        }

        if (directionY != lastDirectionY)
        {
            t = 0.0f;
        }

        // rempalcement des mouvements si sort de l'ecran
        if (touchLeft == true && newPos.x < actualPos.x)
        {              
            newPos.x = actualPos.x;                   
        }
        else if (touchRight == true && newPos.x > actualPos.x && touchLeft == false)
        {      
            newPos.x = actualPos.x;
        }

        if (touchUp == true && newPos.y > actualPos.y && touchBot == false)
        {
            newPos.y = actualPos.y;
        }
        else if (touchBot == true && newPos.y < actualPos.y )
        {
            newPos.y = actualPos.y;
        }
        // LA ligne qui bouge la camera
        gameObject.transform.position = new Vector3(Mathf.Lerp(actualPos.x, newPos.x, t), Mathf.Lerp(actualPos.y, newPos.y, t),newPos.z);
        if(t < 2)
        {
            t += 2f * Time.deltaTime;
        }                     
    }
}

