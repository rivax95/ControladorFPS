//                                          ▂ ▃ ▅ ▆ █ ARC █ ▆ ▅ ▃ ▂ 
//                                        ..........<(+_+)>...........
// Controller.cs (01/10/18)
//Autor: Alejandro Rivas                 alejandrotejemundos@hotmail.es
//Desc: Controlador FPS
//Mod : 0.2
//Rev Ini
//..............................................................................................\\
#region Librerias
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alex.MouseLook;
#endregion
namespace Alex.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class Controller : MonoBehaviour
    {
        //Variables de Control
        #region ControlVars
        [Header("Control Vars Settings")]
        public float timeGrounded;
        public float timeAir,timeCrouch,timeShifting,timeMoving,timeRuning,GroundedDistance,MaxAirDistance;
        public bool is_Moving, is_Grounded, is_Crouching, is_Shiftting, is_Jumping,is_Runing,is_FailJump;
        public bool can_Moving ;
        #endregion
        public float MinRayFailAir=4f;
        //Variables publicas
        #region VariablesPublicas
        [Header("Controller Settings")]
         public float walkSpeed = 6.75f;
        public float runSpeed = 10f;
         public float crunchSpeed = 2f;
        
        public float jumpSpeed = 8f;
        [HideInInspector] public float gravity = 30f;
        public LayerMask groundLayer;
        public MauseLook thisMouseLook;
        #endregion
        //Variables privadas
        #region VariablesPrivadas 
        private float default_ControllerHeight;
        private float camHeight;
        private float rayDistance;

        private Transform firstPerson_View;
        private Transform FirstPerson_Camera;

       

        public float speed;
    

        private float inputX, inputY;
        private float inputX_Set, inputY_Set;
        private float inputModifyFactor;

        private float distanciamaxima = 0;

        private bool limitDiagonalSpeed = true;
        public bool WalkForward;
        private float antiBumpFactor = 0.75f;
        private CharacterController charController;
        private Vector3 moveDirection = Vector3.zero;
        private Vector3 default_CamPos;
        private Vector3 firstPerson_View_Rotation = Vector3.zero;

     
        private PlayerAnimations playerAnimations;
        [SerializeField]
        public WeaponManager weapon_Manager;
        private Weapon currentWeapon;

        public WeaponManager HandsWeapon_Manager;
        private HandWeapon currentHandWeapon;

        private float fireRate = 0.15f;
        private float nextTimeToFire = 0f;

        #endregion
        //Inicializadores
        #region Inicializadores
        void Start()
        {
            firstPerson_View = transform.Find("FPS View").transform;
            charController = GetComponent<CharacterController>();
            speed = walkSpeed;
            is_Moving = false;
                        // altura por la mitad mas el radio
            rayDistance = charController.height * 0.5f + charController.radius + 0.3f;
            default_ControllerHeight = charController.height;
            default_CamPos = firstPerson_View.localPosition;

            playerAnimations = GetComponent<PlayerAnimations>();

            weapon_Manager.Weapons[0].SetActive(true);
            currentWeapon = weapon_Manager.Weapons[0].GetComponent<Weapon>();
            HandsWeapon_Manager.Weapons[0].SetActive(true);
            currentHandWeapon = HandsWeapon_Manager.Weapons[0].GetComponent<HandWeapon>();
            can_Moving = true;
        }
        #endregion
        //Actualizadores
        #region Actualizadores
        private void FixedUpdate()
        {
            controlState();
        }
        void Update()
        {
            controlStateAir();
            if (can_Moving)
            {
                PlayerMovement();
            }
            SelectWeapon();
        }
        #endregion
        //Logica Armas
        #region Logica Armas
        void SelectWeapon()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (!weapon_Manager.Weapons[0].activeInHierarchy)
                {
                    for (int i = 0; i < weapon_Manager.Weapons.Length; i++)
                    {
                        weapon_Manager.Weapons[i].SetActive(false);
                    }
                    currentWeapon = null;
                    weapon_Manager.Weapons[0].SetActive(true);
                    currentWeapon = weapon_Manager.Weapons[0].GetComponent<Weapon>();

                    playerAnimations.changeController(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (!weapon_Manager.Weapons[1].activeInHierarchy)
                {
                    for (int i = 0; i < weapon_Manager.Weapons.Length; i++)
                    {
                        weapon_Manager.Weapons[i].SetActive(false);
                    }
                    currentWeapon = null;
                    weapon_Manager.Weapons[1].SetActive(true);
                    currentWeapon = weapon_Manager.Weapons[1].GetComponent<Weapon>();

                    playerAnimations.changeController(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (!weapon_Manager.Weapons[2].activeInHierarchy)
                {
                    for (int i = 0; i < weapon_Manager.Weapons.Length; i++)
                    {
                        weapon_Manager.Weapons[i].SetActive(false);
                    }
                    currentWeapon = null;
                    weapon_Manager.Weapons[2].SetActive(true);
                    currentWeapon = weapon_Manager.Weapons[2].GetComponent<Weapon>();

                    playerAnimations.changeController(false);
                }
            }





        }
        #endregion
        //Movimiento
        #region Movimiento
        void PasarAnimaciones()
        {
            //Debug.Log(charController.velocity.x +"x---z"+ charController.velocity.z);
            playerAnimations.Is_Jumping(is_Jumping);
            playerAnimations.Movement(charController.velocity.magnitude);
            playerAnimations.PlayerForward(inputY);
            playerAnimations.PlayerMovementX(inputX);
            playerAnimations.rotationY(thisMouseLook.rotation_Y);
            playerAnimations.IsGrounded(is_Grounded);
            if (is_Crouching && charController.velocity.magnitude > 0f)
            {

                playerAnimations.PlayerCrounchWalk(charController.velocity.magnitude);

            }

            //Disparo
            if (Input.GetMouseButton(0)  )
            {
                Debug.Log("Pulsado");
             

                if (is_Crouching)
                {
                    playerAnimations.Shoor(false);

                }
                else
                {
                    playerAnimations.Shoor(true);
                }
                if ((Time.time > nextTimeToFire) &&(Input.GetMouseButton(0)))

                {
                  //  fireRate = playerAnimations.TimeSHott();
                    nextTimeToFire = Time.time + fireRate;
                    playerAnimations.IsShotting(true);

                    Debug.Log("Disparo desde el controller");
                    currentWeapon.Shoot();
                    currentHandWeapon.ShootMet();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                playerAnimations.IsShotting(false);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerAnimations.Reload();
                currentHandWeapon.reload();
            }


        }
        /// <summary>
        /// Movimiento del jugador
        /// </summary>
        /// <remarks>
        /// Movimiento del Jugador usado con el CharacterController
        /// </remarks>
     
        void PlayerMovement()
        {
          //  Debug.Log(charController.velocity.x);
            
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {

                if (Input.GetKey(KeyCode.W))
                {
                    WalkForward = true;
                    inputY_Set = 1f;
                }
                else
                {
                    WalkForward = false;
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
                    inputX_Set = -1f;
                }
                else
                {
                    inputX_Set = 1f;
                }

            }
            else
            {
                inputX_Set = 0f;
            }
            
            inputY = Mathf.Lerp(inputY , inputY_Set, Time.deltaTime * 19f);
            inputX = Mathf.Lerp(inputX, inputX_Set, Time.deltaTime * 19f);
            if (inputY < -0.5f) inputY = -0.5f; //Note TEST
            if (inputX<  -0.5f) inputX = -0.5f;//Note TEST
            if (inputX > 0.5f) inputX = 0.5f;//Note TEST
            //Notee hace lo mismo con la x
            //NOTEE Condicional directo, si ocurre esto, lo pone a 0,75, si no a 1.
            inputModifyFactor = Mathf.Lerp(inputModifyFactor, (inputY_Set != 0 && inputX_Set != 0 && limitDiagonalSpeed) ? 0.75f : 1, Time.deltaTime * 19f);

            firstPerson_View_Rotation = Vector3.Lerp(firstPerson_View_Rotation, Vector3.zero, Time.deltaTime * 5f);

            firstPerson_View.localEulerAngles = firstPerson_View_Rotation;
            // NOTEE cosas que hara en el suelo
            if (is_Grounded)
            {
                is_Jumping = false;
                PlayerCrouchAndSprint();
                moveDirection = new Vector3(inputX * inputModifyFactor, -antiBumpFactor, inputY * inputModifyFactor);
                moveDirection = transform.TransformDirection(moveDirection) * speed;

                // LLAMADAS DE SALTO
                playerJump();
                Shifteo();
             
            }
            moveDirection.y -= gravity * Time.deltaTime;

            is_Grounded = (charController.Move(moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;

            is_Moving = charController.velocity.magnitude > 0.15f;
            PasarAnimaciones();
           
        }
        void Shifteo()
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if ((CanGetUp())&&(is_Crouching))
                {
                    is_Crouching = false;
                    StopCoroutine(MoveCameraCrounch());
                    StartCoroutine(MoveCameraCrounch());
                    return;
                }
                else
                {
                    if (is_Grounded)
                    {
                        is_Shiftting = true;
                        moveDirection.z *= 0.5f;
                        moveDirection.x *= 0.5f;
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                is_Shiftting = false;
            }
        }
        void PlayerCrouchAndSprint()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!is_Crouching)
                {
                    is_Crouching = true;
                }
                else
                {
                    if (CanGetUp())
                    {
                        is_Crouching = false;
                    }
                }
                StopCoroutine(MoveCameraCrounch());
                StartCoroutine(MoveCameraCrounch());

            }
            if (is_Crouching)
            {
                speed = crunchSpeed;
            }
            else
            {

                if (Input.GetKey(KeyCode.LeftShift) && (!Input.GetKey(KeyCode.S)) &&(Input.GetKey(KeyCode.W)))
                {

                    speed = runSpeed;
                    is_Runing = true;

                }
                else
                {
                    is_Runing = false;
                    speed = walkSpeed;
                }

            }
            playerAnimations.PlayerCrouch(is_Crouching);
        }
        bool CanGetUp() // no funciona
        {
            //Debug.Log("entro");
            Ray groundRay = new Ray(transform.position, transform.up);
            RaycastHit groundHit;
           
            if (Physics.SphereCast(groundRay, charController.radius + 0.05f, out groundHit, rayDistance, groundLayer)) 
            {
                Debug.Log("tiro la esfera");
             //no entra
                if (Vector3.Distance(transform.position, groundHit.point) < 2.3f)
                {
                    Debug.Log("No deja");
                    return false;
                }
            }
            //Debug.Log(groundHit.collider.tag);
            return true;
        }
        //void OnDrawGizmos()
        //{
        //    Gizmos.DrawSphere(transform.position, charController.radius + 0.05f);
        //}
        void playerJump() //Salto ME CAGO EN LA PUTA OSTIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                if (is_Crouching)
                {
                    if (CanGetUp())
                    {
                        
                        is_Crouching = false;
                        playerAnimations.PlayerCrouch(is_Crouching);
                        StopCoroutine(MoveCameraCrounch());
                        StartCoroutine(MoveCameraCrounch());
                    }
                }
                else
                {
                    is_Jumping = true;
                    moveDirection.x *= 0.5f;
                    moveDirection.z *= 0.5f;
                    moveDirection.y = jumpSpeed;
                }
            }
          
            //Penalizacion caida
           
          
        }
        #endregion
        //Corrutines Movement
        #region corrutinasMovimiento
            IEnumerator PenaltyMovement(float PenalityTiming)
        {
            Debug.Log("penalizado");
            moveDirection.x = 0f;
            moveDirection.z = 0f;
            can_Moving = false;
            yield return new WaitForSeconds(PenalityTiming);
            can_Moving = true;
        }
        IEnumerator MoveCameraCrounch()
        {
            charController.height = is_Crouching ? default_ControllerHeight / 1.5f : default_ControllerHeight;

            charController.center = new Vector3(0f, charController.height / 2f, 0f);

            camHeight = is_Crouching ? default_ControllerHeight / 1.5f : default_CamPos.y;

            //if (is_Crouching)
            //{
            //    camHeight = default_CamPos.y / 1.5f;
            //}

            while (Mathf.Abs(camHeight - firstPerson_View.localPosition.y) > 0.001f)
            {
                firstPerson_View.localPosition = Vector3.Lerp(firstPerson_View.localPosition,
                    new Vector3(default_CamPos.x, camHeight, default_CamPos.z),
                    Time.deltaTime * 10f);
                //Debug.Log("Soy un contador");
                yield return null;
            }
        }
#endregion
        public void controlStateAir()
        {
            if (is_Grounded && MaxAirDistance > MinRayFailAir)
            {
                //efecto caida

                //activo corrutina penalizacion
                StopCoroutine(PenaltyMovement(0f));
                StartCoroutine(PenaltyMovement(0.7f));
            }
            //Rycasting
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity, groundLayer))
            {
                Debug.DrawRay(transform.position + new Vector3(0, 0.1f, 0), transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
                //   Debug.Log("Did Hit");
            }
            GroundedDistance = Vector3.Distance(transform.position, hit.point);
            if (!is_Grounded)
            {

                if (GroundedDistance > 0)
                {
                    if (GroundedDistance > distanciamaxima)
                    {
                        distanciamaxima = GroundedDistance;
                    }
                    //   Debug.Log(distanciamaxima+ " Distanceeee " + GroundedDistance);
                    MaxAirDistance = distanciamaxima;
                }
                else
                {

                }
            }
            else
            {
                distanciamaxima = 0;
                MaxAirDistance = 0;
            }


        }
    
    //Control de estados
    public void controlState()
    {
        // Animations
        playerAnimations.GroundDistance(MaxAirDistance);
        //Ifs
        timeCrouch = (is_Crouching) ? timeCrouch + Time.deltaTime : 0;
        timeAir = (!is_Grounded) ? timeAir + Time.deltaTime : 0;
        timeGrounded = (is_Grounded) ? timeGrounded + Time.deltaTime : 0;
        timeMoving = (is_Moving) ? timeMoving + Time.deltaTime : 0;
        timeShifting = (is_Shiftting) ? timeShifting + Time.deltaTime : 0;
        timeRuning = (is_Runing) ? timeRuning + Time.deltaTime : 0;
    }
          
    } //Fin de la clase
}