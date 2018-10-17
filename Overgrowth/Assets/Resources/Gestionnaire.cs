using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "Gestionnaire", menuName = "ScriptablesObjects/Gestionnaire", order = 1)]
public class Gestionnaire : ScriptableObject {
   
    public float Speed;
    public bool Locked;
    public bool Crouch;
    public bool isGlinding;
    public bool GlideGauche;

    public bool grounded;
    public bool CharJump;

    public int JumpCD;
    public bool Jcd;
    public bool KnockbackCD;

    public bool isFiring;
    public bool SuitActivated;
    public bool disfunction;


    // Power ups
    public int[] PowerUps;
    // 0 = Analysis level
    // 1 = Can Switch
    // 2 = Gravity Anchor
    // 3 = Gun
    // 4 = life 
    // 5 = vineBridge
    // 6 = wall jump


    public int life;

    public bool invicible;
    public Vector3 Checkpoint;
}
