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
using Alex.MouseLook;

public class Health : MonoBehaviour {

    public float value = 100;
    public GameObject camera;
    public bool died;
    public bool res;
    public GameObject DeffectWeapon;
    public GameObject WeaponFather;
    public Transform Respawn;
    private Controller control;
    public void Start()
    {
        control = this.gameObject.GetComponent<Controller>();
    }
    public void Update()
    {
        if (died && !control.die)
        {
            died =false;
            control.die = true;
            Died();
        }
        if (res && control.die)
        {
            control.die = false;
            res = false;
            revive(Respawn);
        }
    }
    public void TakeDamage(float dame)
    {
        value -= dame;
    }
    public void Died()
    {
        WeaponManager.instance.dropIstantiate(WeaponManager.instance.WeaponbaseCurrent.gameObject);
        foreach (Transform item in WeaponFather.transform)
        {
            Destroy(item.gameObject);
        }

        this.gameObject.GetComponent<Animator>().enabled = false;
    //    this.gameObject.GetComponent<Controller>().enabled = false;
        this.gameObject.GetComponent<MauseLook>().enabled = false;
        this.gameObject.transform.Find("Recoil").gameObject.SetActive(false);
        this.gameObject.GetComponent<CharacterController>().enabled = false;
        camera.SetActive(true);
    }
    public void revive(Transform Respawn)
    {
        this.gameObject.GetComponent<Animator>().enabled = true;
       // this.gameObject.GetComponent<Controller>().enabled = true;
        this.gameObject.GetComponent<MauseLook>().enabled = true;
        this.gameObject.transform.Find("Recoil").gameObject.SetActive(true);
        this.gameObject.GetComponent<CharacterController>().enabled = true;
        camera.SetActive(false);

        GameObject DeffectWeapn= Instantiate(DeffectWeapon, WeaponFather.transform);
      
        this.transform.position = Respawn.position;
        WeaponManager.instance.ActualizarInventario();
        DeffectWeapn.SetActive(true);
    }

}
