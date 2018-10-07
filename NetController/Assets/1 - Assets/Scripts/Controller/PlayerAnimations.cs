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

    private string STAND_SHOOT = "StandShoot";
    private string CROUCH_SHOO = "CrouchShoot";
    private string RELOAD = "Reload";
    public RuntimeAnimatorController animcontroller_pistol,animcontroller_MachineGun;
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
    public void Shoor(bool isSTanding)
    {
        if (isSTanding)
        {
            anim.SetTrigger(STAND_SHOOT);
        }
        else
        {
            anim.SetTrigger(CROUCH_SHOO);
        }
    }
    public void Reload()
    {
        anim.SetTrigger(RELOAD);
    }
    public void changeController(bool isPistol)
    {
        if (isPistol)
        {
            anim.runtimeAnimatorController = animcontroller_pistol;
        }
        else
        {
            anim.runtimeAnimatorController = animcontroller_MachineGun;
        }
    }
    public void IsShotting(bool isShooting)
    {
        anim.SetBool("StandShooting", isShooting);
    }

    public float TimeSHott()
    {

        AnimatorStateInfo currInfo = anim.GetCurrentAnimatorStateInfo(0);
        //currInfo.length
        Debug.Log(currInfo.length + "esto q es");
        return currInfo.length-6f * currInfo.speedMultiplier;
    }

}
