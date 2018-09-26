using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScroller : MonoBehaviour
{
    public Transform m_Minimap;
    [Range(0.01f, 0.1f)] public float m_MapScale = 0.3f;
    public Transform m_Character;

    private void LateUpdate()
    {
        Vector2 charVel = m_Character.GetComponent<Rigidbody2D>().velocity;
        if( m_Character.GetComponent<SuitMove>().m_HasMoved )
            m_Minimap.Translate( -charVel.x * m_MapScale, -charVel.y * m_MapScale, 1 );
    }
}