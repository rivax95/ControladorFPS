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

public class Penetration : MonoBehaviour
{

    public float value;
    public enum Material { Cemento, Metal, Madera, Cristal, Suelo }
    public Material TipoMaterial;
    private void Start()
    {
        switch(TipoMaterial){ 
            case Material.Cemento:
                value = 2700f;
                break;
            case Material.Madera:
                value = 1200f;
                break;
            case Material.Metal:
                value = 7200;
                break;
            case Material.Suelo:
                value = 100000;
                break;
            case Material.Cristal:
                value = 300;
                break;
    }

}

}