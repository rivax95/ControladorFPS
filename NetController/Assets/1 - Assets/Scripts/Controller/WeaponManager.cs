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

public enum Weapon{

    Police9mm,

    UMP45
}
public class WeaponManager : MonoBehaviour {
    public static WeaponManager instance;
    public Weapon currentWeapon = Weapon.Police9mm;
    private int CurrenWeaponIndex=0;
    private Weapon[] weapons = { Weapon.Police9mm, Weapon.UMP45 };
    [HideInInspector]
    public WeaponBase WeaponbaseCurrent;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        transform.Find(weapons[CurrenWeaponIndex].ToString()).gameObject.SetActive(true);
        WeaponbaseCurrent= transform.Find(weapons[CurrenWeaponIndex].ToString()).GetComponent<WeaponBase>();
    }
    void Update()
    {
        CheckWeaponSwitch();
    }
    void SwitchToCurrentWeapon()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.Find(weapons[CurrenWeaponIndex].ToString()).gameObject.SetActive(true);
        WeaponbaseCurrent = transform.Find(weapons[CurrenWeaponIndex].ToString()).GetComponent<WeaponBase>();
    }
    void CheckWeaponSwitch()
    {
        float mousewheel = Input.GetAxis("Mouse ScrollWheel");
        if (mousewheel > 0)
        {
            SelectPreviousWeapon();
        }
        else if (mousewheel < 0)
        {
            selecNextWeapon();
        }
    }

    void SelectPreviousWeapon()
    {
        if (CurrenWeaponIndex == 0)
        {
            CurrenWeaponIndex = weapons.Length - 1;
        }
        else
        {
            CurrenWeaponIndex--;
        }
        SwitchToCurrentWeapon();
    }
    void selecNextWeapon()
    {
        if (CurrenWeaponIndex >= weapons.Length-1)
        {
            CurrenWeaponIndex = 0;
        }
        else
        {
            CurrenWeaponIndex++;
        }
        SwitchToCurrentWeapon();
    }
}
