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
    public float randomY;
    public float miny, maxy;

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


            Quaternion reco = Quaternion.Euler(-recoil, randomY, 0f);
                reco.x = ClampAngle(reco.x, minimum_X, maximun_X);
                Vector3 direct = Vector3.up * 1;
                Quaternion xquuat =  Quaternion.AngleAxis(this.transform.rotation.x, direct);
                transform.localRotation = (father.originalRotation * xquuat);

                transform.localRotation *= reco;
                if (recoil > 0f)
                {
                    recoil -= Time.deltaTime*4;
                }
                else
                {
                    recoil = 0f;
                }
        if (randomY > 0)
        {
            randomY -= Time.deltaTime;
        }
        else
        {
            randomY = 0;
        }
       
         
        }
        #endregion
        public void Recoil(float amount)
        {
        randomY = Random.Range(miny, maxy);
        if (recoil > 5) return;
            recoil += amount;
        
       
        }
    
}
