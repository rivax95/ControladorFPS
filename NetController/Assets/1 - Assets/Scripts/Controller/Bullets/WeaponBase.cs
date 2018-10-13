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
public enum ModoDeFuego
{
    SemiAuto,
    FullAuto
}
public class WeaponBase : MonoBehaviour {

    protected AudioSource audiosource;
   protected  bool fireLock;
    protected  bool canShoot;
    public bool isReloading = false;
    [Header("Sonidos")]
    public AudioClip Fire;
    public AudioClip DryFire;
    public AudioClip Draw;
    public AudioClip MagOutSound;
    public AudioClip MagInSound;
    public AudioClip boltSound;
    [Header("Referencias")]
    protected Animator animator;
    public ParticleSystem muzzleFlash;
    [Header("Weapon Attributes")]
    public ModoDeFuego fireMode = ModoDeFuego.FullAuto;
    public float damage = 20f;
    public float fireRate = 1f;
    public int bulletsInClip ;
    public int clipSize  =12;
    public int bulletsLeft ;
    public int maxAmmo=100 ;

    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        bulletsInClip = clipSize;
        bulletsLeft = maxAmmo;
        Invoke("EnableWeapon", 1f);
    }
    void EnableWeapon()
    {
        canShoot = true;
    }
    void Update()
    {
        if (fireMode == ModoDeFuego.FullAuto && Input.GetButton("Fire1"))
        {
            checkFire();
        }
        else if (fireMode == ModoDeFuego.SemiAuto && Input.GetButtonDown("Fire1"))
        {
            checkFire();
        }
        if (Input.GetButtonDown("Reload"))
        {
            //Debug.Log("PressR");
            ChekReload();
        }
    }
    void checkFire()
    {
        if (isReloading) return;
        if (!canShoot) { return; }
        if (fireLock) { return; }
        if (bulletsInClip > 0)
        {
            FIRE();
            Debug.Log("DisaproController1");
        }
        else
        {
            Debug.Log("NoDisaproController1");
            DRYFIRE();
        }
    }
    public virtual void PlayFireAnimation()
    {
        animator.CrossFadeInFixedTime("Fire", 0.1f);
    

    }
    void FIRE()
    {
        audiosource.PlayOneShot(Fire);
        fireLock = true;
        muzzleFlash.Stop();
        muzzleFlash.Play();
        PlayFireAnimation();
        bulletsInClip--;
      
        StartCoroutine(CoResetFireLook());
    }
    void DRYFIRE()
    {
        audiosource.PlayOneShot(DryFire);
        fireLock = true;

        // animator.CrossFadeInFixedTime("Fire", 0.1f);
        StartCoroutine(CoResetFireLook());
    }
    IEnumerator CoResetFireLook()
    {
        yield return new WaitForSeconds(fireRate);
        fireLock = false;
    }
    void ChekReload()
    {
        if (bulletsLeft>0 && bulletsInClip < clipSize)
        {
            Debug.Log("Accion de recargar");
            Reload();
        }


    }
    void Reload()
    {
        if (isReloading) return;
        isReloading = true;
        animator.CrossFadeInFixedTime("Reload", 0.1f);
    }
    void ReloadAmmo()
    {
        int bulletsToLoad = clipSize - bulletsInClip;
        int bulletsToSub = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;
        bulletsLeft -= bulletsToSub;
        bulletsInClip += bulletsToLoad;
    }
    public void OnDraw()
    {
        audiosource.PlayOneShot(Draw);
    }
    public void OnMagOut()
    {
        audiosource.PlayOneShot(MagOutSound);
    }
    public void OnMagIn()
    {
        ReloadAmmo();
        audiosource.PlayOneShot(MagInSound);
    }
    public void OnBoltForwarded()
    {
       
        audiosource.PlayOneShot(boltSound);
        CancelInvoke("resetReloading");
        Debug.Log("Recargando");
        Invoke("resetReloading", 1f);// invoke por probar
    }
    void resetReloading()
    {
        isReloading = false;

    }
}
