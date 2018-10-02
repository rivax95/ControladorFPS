//                                          ▂ ▃ ▅ ▆ █ ARC █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// Controller.cs (01/10/18)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Controlador FPS
//Mod : 0.1
//Rev Ini
//..............................................................................................\\
#region Librerias
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
namespace Alex.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class Controller : MonoBehaviour
    {


        #region VariablesPublicas

        [HideInInspector] public float walkSpeed = 6.75f;
        [HideInInspector] public float runSpeed = 10f;
        [HideInInspector] public float crunchSpeed = 8f;
        [HideInInspector] public float gravity = 20f;
        #endregion
        #region VariablesPrivadas
        private Transform firstPerson_View;
        private Transform FirstPerson_Camera;

        private Vector3 firstPerson_View_Rotation = Vector3.zero;

        private float speed;
        private bool is_Moving, is_Grounded, is_Crouching;

        private float inputX, inputY;
        private float inputX_Set, inputY_Set;
        private float inputModifyFactor;

        private bool limitDiagonalSpeed = true;

        private float antiBumpFactor = 0.75f;
        private CharacterController charController;
        private Vector3 moveDirection = Vector3.zero;
        #endregion
        void Start()
        {
            firstPerson_View = transform.Find("FPS View").transform;
            charController = GetComponent<CharacterController>();
            speed = walkSpeed;
            is_Moving = false;
        }


        void Update()
        {
            PlayerMovement();
        }
        /// <summary>
        /// Movimiento del jugador
        /// </summary>
        /// <remarks>
        /// Movimiento del Jugador usado con el CharacterController
        /// </remarks>
     
        void PlayerMovement()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {

                if (Input.GetKey(KeyCode.W))
                {
                    inputY_Set = 1f;
                }
                else
                {
                    inputY_Set = -1f;
                }

            }
            else
            {
                inputY_Set = 0f;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {

                if (Input.GetKey(KeyCode.A))
                {
                    inputX_Set = 1f;
                }
                else
                {
                    inputX_Set = -1f;
                }

            }
            else
            {
                inputX_Set = 0f;
            }
            inputY = Mathf.Lerp(inputY, inputY_Set, Time.deltaTime * 19f);
            inputX = Mathf.Lerp(inputX, inputX_Set, Time.deltaTime * 19f);
            //Condicional directo, si ocurre esto, lo pone a 0,75, si no a 1.
            inputModifyFactor = Mathf.Lerp(inputModifyFactor, (inputY_Set != 0 && inputX_Set != 0 && limitDiagonalSpeed) ? 0.75f : 1, Time.deltaTime * 19f);

            firstPerson_View_Rotation = Vector3.Lerp(firstPerson_View_Rotation, Vector3.zero, Time.deltaTime * 5f);

            firstPerson_View.localEulerAngles = firstPerson_View_Rotation;
            if (is_Grounded)
            {
                moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
                moveDirection = transform.TransformDirection(moveDirection) * speed;
            }
            moveDirection.y -= gravity * Time.deltaTime;

            is_Grounded = (charController.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;

            is_Moving = charController.velocity.magnitude > 0.15f;
        }


    } //Fin
}