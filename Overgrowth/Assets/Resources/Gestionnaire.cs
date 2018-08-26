using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "Gestionnaire", menuName = "ScriptablesObjects/Gestionnaire", order = 1)]
public class Gestionnaire : ScriptableObject {
   
    public float Speed;
    public bool Locked;
    
    public bool isGlinding;
    public bool GlideGauche;
    public bool isReloading;
    public bool grounded;
    public bool CharJump;

    public int JumpCD;
    public bool Jcd;
    public bool KnockbackCD;

    public bool isFiring;
    public bool SuitActivated;
    public bool canSwitch;
    public bool atPoint;

    public Vector3 Checkpoint;
    public Vector3 bigCheckpoint;

    // Suit Power ups
    public int analysisLevel;
    public bool Switchbeam;
    public bool GravityAnchor;
    public bool WallProps;
    public bool Flamenwerfer;
    public bool Laser;
    public bool ShockWave;

    //plant power ups
    public bool Planeur;
    public bool VineBridge;
  
    public int life;
    public int maxLife;
    public bool invicible;

    public bool resetPU;

    public int currentSalle;
    public bool manetteMode;
}
