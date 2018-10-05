//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// .cs (//)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc:
//Mod : 
//Rev :
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    private Animator anim;
    private string MOVE = "Move";
    private string VELOCITY_Y = "VelocityY";
    private string CROUCH="Crouch";
    private string CROUCH_WALK = "CrouchWalk";
	void Awake () {
        anim = GetComponent<Animator>();
	}
    public void Movement(float magnitud)
    {
        anim.SetFloat(MOVE, magnitud);
    }
    public void PlayerForward(float magnitud)
    {
        anim.SetFloat(VELOCITY_Y, magnitud);
    }
    public void PlayerMovementX(float magnitud)
    {
        anim.SetFloat("VelocityX", magnitud);
    }
    public void PlayerCrouch(bool isCrouching)
    {
        anim.SetBool(CROUCH, isCrouching);
    }
    public void PlayerCrounchWalk(float magnitud)
    {
        anim.SetFloat(CROUCH_WALK, magnitud);
    }
	
	void Update () {
		
	}
}
