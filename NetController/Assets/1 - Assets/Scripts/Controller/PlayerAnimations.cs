//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// .cs (//)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Pasamos animaciones desde aqui para que sea todo mas limpio
//Mod : 
//Rev : 0.1
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    private Animator anim;
  //  public Animator animHand;
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
    public void Is_Jumping(bool jump)
    {
        anim.SetBool("Is_Jumping", jump);
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
       // animHand.SetTrigger("Shoot");
    }
    public void Reload()
    {
        anim.SetTrigger(RELOAD);
       // animHand.SetTrigger("Reload");
    
       
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
    public void IsGrounded(bool isGrounded)
    {
        anim.SetBool("Is_Grounded", isGrounded);
    }
    public float TimeSHott()
    {

        AnimatorStateInfo currInfo = anim.GetCurrentAnimatorStateInfo(0);
        //currInfo.length
        //Debug.Log(currInfo.length + "esto q es");
        return currInfo.length-6f * currInfo.speedMultiplier;
    }
    public void rotationY(float Mousey)
    {
        anim.SetFloat("MauseY", Mousey);
    }
    public void GroundDistance(float Distance)
    {
        anim.SetFloat("GroundDistance", Distance);
    }
    //public void Caida(bool caida)
    //{
    //    anim.SetBool("Caida", caida);
    //}
    //public bool Get_FailJump()
    //{
    //    Debug.Log("entro");
    //    AnimatorStateInfo info=
    //    anim.GetCurrentAnimatorStateInfo(0);
    //    Debug.Log(info.IsName("caida") + " Estado");
    //    if (info.IsName("Callendo"))
    //    {

    //        return true;
    //    }

    //    return false;
    //}
    
}
