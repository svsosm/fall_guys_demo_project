using UnityEngine;


namespace Player.Controller
{
    public enum PlayerState
    {
        IDLE = 0,
        RUNNING = 1,
        FALLING = 2
    }

    public class PlayerController : MonoBehaviour
    {
        [Header("General Settings")]
        public PlayerState playerState;

        //Vectors
        [Header("Movement Vectors")]
        [SerializeField] Transform finishLine;
        private Vector3 startPos;
        private Quaternion startRot;

        //Variables
        [Header("Movement Speed")]
        [SerializeField] private float playerTransitionSpeed;
        [SerializeField] private float turnBackSpeed;
        [SerializeField] private float forwardSpeed;
        private bool isGround;
        private const float fallingLimit = -1f;

        //Components
        [Header("Movement Controller")]
        [SerializeField] private FloatingJoystick playerJoystick;
        private Animator playerAnim;
        private Rigidbody rb;
        

        //Tags
        private const string staticPlatformTag = "StaticPlatform";
        private const string finishLineTag = "FinishLine";
        private const string rotatingPlatformTag = "RotatingPlatform";
        private const string lastRotatingPlatformTag = "LastRotatingPlatform";
        private const string playerStateTag = "PlayerState";


        #region Awake, Start and Initialize
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            playerAnim = GetComponent<Animator>();
            startPos = transform.position;
            startRot = transform.rotation;
        }

        void Initialize()
        {
            playerState = PlayerState.IDLE;
            transform.SetParent(null);
            transform.position = startPos;
            transform.rotation = startRot;
        }
        #endregion

        #region Update and FixedUpdate
        private void Update()
        {


            if (GameManager.Instance.gameState.Equals(GameState.RUNNER) && isGround)
            {

                PlayerInput();

            }
    
            //check falling!
            if (transform.position.y < fallingLimit)
            {
                playerState = PlayerState.FALLING;
                Initialize();
            }

            playerAnim.SetInteger(playerStateTag, (int)playerState);

        }

        private void FixedUpdate()
        {
            if(playerState.Equals(PlayerState.RUNNING))
            {
                PlayerMoveForward();
            }

            if(GameManager.Instance.gameState.Equals(GameState.TRANSITION))
            {
                transform.position = Vector3.Lerp(transform.position, finishLine.position, playerTransitionSpeed);
                playerState = PlayerState.IDLE;
            }
        }
        #endregion

        #region Player Input
        void PlayerInput()
        {
            if(InputManager.Instance.ForwardMovement())
            {
                playerState = PlayerState.RUNNING;
            }
            else if(InputManager.Instance.BackwardMovement())
            {
                PlayerTurnBack();
            }
            else if(InputManager.Instance.IdleMovement())
            {
                playerState = PlayerState.IDLE;
            }
        }
        #endregion

        #region Player Movement
        void PlayerTurnBack()
        {
            Quaternion targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnBackSpeed);
        }

        void PlayerMoveForward()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            float mV = Mathf.PingPong(Input.GetAxis("Vertical"), 1);  //player just move forward!
#else
            float mV = Mathf.PingPong(playerJoystick.Vertical, 1);  //player just move forward!
#endif
            Vector3 move = transform.forward * mV;
            rb.MovePosition(transform.position + move * forwardSpeed * Time.deltaTime);
        }
        #endregion

        #region Player Collision
        private void OnCollisionEnter(Collision collision)
        {
            switch(collision.transform.tag)
            {
                case staticPlatformTag:
                    isGround = true;
                    break;
                case finishLineTag:
                    GameManager.Instance.OpenPaintWall();
                    break;
                case rotatingPlatformTag:
                    transform.SetParent(collision.transform, true);
                    break;
                default:
                    break;
            }
        }



        private void OnCollisionExit(Collision collision)
        {
            switch (collision.transform.tag)
            {
                case lastRotatingPlatformTag:
                    transform.SetParent(null,true);
                    transform.rotation = Quaternion.Euler(new Vector3(0, rb.rotation.y, 0));
                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}