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
using Alex.Controller;
public enum ModoDeFuego
{
    SemiAuto,
    FullAuto
}
public class WeaponBase : MonoBehaviour {

    protected AudioSource audiosource;
   protected  bool fireLock;
    protected  bool canShoot;
    public bool Shoot = false;
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
    public ParticleSystem bloodFX;
    [Header("Weapon Attributes")]
    public LayerMask ShootRayLayer;
    public ModoDeFuego fireMode = ModoDeFuego.FullAuto;
    public float damage = 20f;
    public float fireRate = 1f;
    public int bulletsInClip;
 //   public float BulletAmountPenetration;
    public float penetration;
    public float minpenetration;
    public int clipSize  =12;
    public int bulletsLeft ;
    public int maxAmmo=100 ;
    public Animator Playeranim;
    public GameObject ShootPoint;
    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        bulletsInClip = clipSize;
        bulletsLeft = maxAmmo;
        Invoke("EnableWeapon", 1f); // pasar a corrutinas
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
    public void CreateBlood(Vector3 pos, Quaternion rot)
    {
        ParticleSystem blood = Instantiate(bloodFX, pos, rot);
        blood.Play();
        Destroy(blood, 1f);
    }
    void DetectedHit()
    {
        RaycastHit[] hit;
        float penetrationHit=penetration;
        float damegehit=damage;
       // float amount = BulletAmountPenetration;
         //Ray Ray = ShootPoint.ScreenPointToRay(Input.mousePosition);
       //  Debug.DrawRay(ShootPoint.ViewportPointToRay(Vector3.forward),);
        hit = Physics.RaycastAll(ShootPoint.transform.position, ShootPoint.transform.forward, ShootRayLayer);
       // Debug.Log(hit.transform.gameObject.name);
        for (int i = 0; i < hit.Length; i++)
        {

            Penetration takevalue = hit[i].transform.GetComponent<Penetration>();
            float body = takevalue.value;
            Health health = hit[i].transform.GetComponent<Health>();

            if (minpenetration <= body)
            {
                Debug.Log("Enrta");
                if (takevalue != null)
                {
                    Debug.Log("Enrta1");
                    if (penetrationHit - body > 0)
                    {
                        Debug.Log("Enrta2");
                        penetrationHit -= body;
                      //sigue haciendo daño
                        float porcentaje = penetrationHit * 100 / penetration ;//cuanto por ciento
                        float damageHit = porcentaje / damage;
                        float finalDamage = damageHit - damage;
                        finalDamage = Mathf.Abs(finalDamage);
                        Debug.Log("damge" + finalDamage);

                    }
                    else {
                        //final damage
                        penetrationHit -= minpenetration;
                        
                        break; }


                }
            }

            else
            {
                penetrationHit -= minpenetration;
                //calcula daño
                // haz daño una vez
                if (hit[i].transform.CompareTag("Enemy"))
                {
                    //Debug.Log("hace");

                    if (health == null)
                    {
                        //ealth.take
                        Debug.Log("No ahi vida");
                    }
                    else
                    {
                        Debug.Log("hit");
                        health.TakeDamage(damage);
                        CreateBlood(hit[i].point, Quaternion.identity);
                    }
                }
                break;
            }
        }
    }

    void FIRE()
    {
        Shoot = true;
        audiosource.PlayOneShot(Fire);
        fireLock = true;
        DetectedHit();
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
        Shoot = false;
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
        Playeranim.CrossFadeInFixedTime("Reload", 0.1f);
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
