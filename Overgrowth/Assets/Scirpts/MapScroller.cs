using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScroller : MonoBehaviour
{
    public Transform m_Minimap;
    [Range(0.0f, 1.0f)] public float m_MapScale = 0.3f;
    public Transform m_Character;

    private void LateUpdate()
    {
        Vector3 charVel = m_Character.GetComponent<Rigidbody2D>().velocity;
        //if (charVel.x != 0)
        //{
        m_Minimap.Translate(-charVel.x * m_MapScale, -charVel.y * m_MapScale, 1);
        //}
        //if (charVel.y != 0)
        //{

        //}
    }
}