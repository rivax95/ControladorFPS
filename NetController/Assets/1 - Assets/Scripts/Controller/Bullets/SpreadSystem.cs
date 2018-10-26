//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// SpreadSystem.cs (26/10/18)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Este systema controla las penalizaciones de disparo
//Mod : 0.1
//Rev :Ini
//..............................................................................................\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alex.Controller;



    public class SpreadSystem : MonoBehaviour
    {

        #region VariablesPublicas
        
        public SpreadSystem instancia;
        public Controller controlador;
        #endregion

        #region VariablesPrivadas
        float MinSpread;
        #endregion

        #region Inicializadores
        private void Awake()
        {
            if (instancia == null)
            {
                instancia = this;
            }
            else
            {
                Debug.LogError("Ya existe un SpreadSystem en la escena");
            }

        }
        void Start()
        {

        }
        #endregion

        #region Actualizadores
        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region MetodosPrivados
        public bool IsMoving()
        {
        return controlador.is_Moving;
        }
        public bool IsGrounded()
        {
        return controlador.is_Grounded;
        }
        public bool IsCrouching()
    {
        return controlador.is_Crouching;
    }
  private float distance()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(WeaponManager.instance.WeaponbaseCurrent.ShootPoint.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, WeaponManager.instance.WeaponbaseCurrent.ShootRayLayer))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
          
        }
        return hit.distance;
    } 
    private float calculateDistanceSpay()
    {
        
    }
        #endregion

        #region MetodosPublicos
        #endregion

        #region MetodosVirtuales
#endregion

        #region Corrutinas

        #endregion
    }
