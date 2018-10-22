﻿//                                          ▂ ▃ ▅ ▆ █ ZEN █ ▆ ▅ ▃ ▂ 
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
using Alex.MouseLook;
public class RecoilAffect : MonoBehaviour {

	
      
        #region VariablesPublicas


    public MauseLook father;
        private float minimum_X = -15;
        private float maximun_X = 0;



        public float recoil=0f;
        public float recox;
        private Quaternion originalRotation;

        private float mouseSensivity = 1.7f;
        #endregion
        #region inicializadores
        void Start()
        {
            originalRotation = transform.rotation;
        }
        #endregion
        #region Actualizadores
        void FixedUpdate()
        {

        }
        void Update()
        {
            HandleRotation();
        }

        #endregion
        #region MetodosPrivados
        /// <summary>
        /// Sirve para clampear en los 360grados
        /// </summary>
        float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
            {
                angle += 360f;
            }
            if (angle > 350f)
            {
                angle -= 360f;
            }
            return Mathf.Clamp(angle, min, max);
        }
        /// <summary>
        /// Controla la rotacion de la camra
        /// </summary>
        void HandleRotation()
        {

                
                Quaternion reco = Quaternion.Euler(-recoil, 0f, 0f);
                reco.x = ClampAngle(reco.x, minimum_X, maximun_X);

                Quaternion xquuat = Quaternion.AngleAxis(father.rotation_X, Vector3.up);

                transform.rotation = (reco*xquuat);
                if (recoil > 0f)
                {
                    recoil -= Time.deltaTime*4;
                }
                else
                {
                    recoil = 0f;
                }
            
       
         
        }
        #endregion
        public void Recoil(float amount)
        {
            recoil += amount;
        }
    
}
