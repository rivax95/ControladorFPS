using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBase : MonoBehaviour {

    protected AudioSource sonido;
    public LayerMask GrenadeLayer;
    public AudioClip click_Grenade;
    public AudioClip click_GrenadeUp;
    [Header("Referencias")]
    protected Animator anim;
    public GameObject Proyectile;
    public Rigidbody Physics_Proyectile;
    public int maxGrenade;
    public int GrenadeinInventory;
    public float damage;
    public GameObject InstantiatePoint;
    public float fireRate;
    private bool fireLock;
    private bool GrenadeEnable;
    public Vector3 Forces;
	
	void Awake () {
        GrenadeEnable = false;
        anim = GetComponent<Animator>();
        sonido = GetComponent<AudioSource>();
        StartCoroutine(ActivarGrenade());
        Physics_Proyectile = Proyectile.GetComponent<Rigidbody>();
        Physics_Proyectile.isKinematic = true;

	}
	
	
	void Update () {
		
	}

    public void CheckFire()
    {
        if (fireLock) return;
        if (!GrenadeEnable) return;
    }

   public  void Fire()
    {
        CheckFire();
        if (Input.GetMouseButtonUp(0))
        {
            GrenadeEnable = false;

            StartCoroutine(GrenadeRate());
        }
    }

    public virtual void ForcesProyectile()
    {
        // Solo fuerzas
        Physics_Proyectile.isKinematic = false;
    }

    public virtual void explosionEffect() //
    {
        
    }


    IEnumerator ActivarGrenade()
    {
        yield return new WaitForSeconds(1f);
        GrenadeEnable = true;
    }
    IEnumerator GrenadeRate()
    {
        yield return new WaitForSeconds(1.5f);
        GrenadeEnable = true;
    }
}
