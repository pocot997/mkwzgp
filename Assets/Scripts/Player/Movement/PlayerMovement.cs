using System.Collections;
using UnityEngine;
using DVSN.GameManagment;

namespace DVSN.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float playerHight = 2f;
        private Rigidbody rb;

        [Header("Movement")]
        [SerializeField] private Transform orientation;
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float movementMultiplier = 10f;
        [SerializeField] private float airMultiplier = 0.4f;
        private Vector3 moveDirection;
        private float horizontalMovement;
        private float verticalMovement;
        private bool isMoving;

        [Header("Keybinds")]
        [SerializeField] private KeyCode jumpKey = KeyCode.Space;
        [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode dashKey = KeyCode.LeftControl;

        [Header("Jumping")]
        [SerializeField] private float jumpForce = 8f;
        [SerializeField] private float landingHelper = 1.5f;
        [SerializeField] private float currentHelper = 0f;

        [Header("Dash")]
        [SerializeField] private float dashForce = 8f;
        [SerializeField] private float dashCooldown = 3f;
        private bool dashReady = true;

        [Header("Drag")]
        [SerializeField] private float groundDrag = 6f;
        [SerializeField] private float airDrag = 1f;

        [Header("Ground Detection")]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.4f;
        private bool isGrounded;

        [Header("Sprinting")]
        [SerializeField] private float walkSpeed = 4f;
        [SerializeField] private float sprintSpeed = 6f;
        [SerializeField] private float acceleration = 10f;

        [Header("Slopes")]
        [SerializeField] private PhysicMaterial bodyMaterial;
        [SerializeField] private float slopeMaxAngle = 40f;
        [SerializeField] private float slopeSlideMultiplier = 10f;
        private Vector3 slopeMoveDirection;
        private RaycastHit slopeHit;
        private float slopeNormalized;
        private bool isOnSharpSlope;



        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            slopeNormalized = Mathf.Sin(slopeMaxAngle);
        }

        private void Update()
        {
            bodyMaterial.staticFriction = 0;
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (!isGrounded || isOnSharpSlope)
            {
                Managers.Spellcasting.isAbleToCast = false;
            }
            else
            {
                Managers.Spellcasting.isAbleToCast = true;
            }

            MoveInput();

            if (isOnSharpSlope)
            {
                moveDirection = Vector3.zero;
            }

            ControlDragAndLanding();
            ControlSpeed();

            if (!isOnSharpSlope)
            {
                CheckInput();
            }

            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

            if (isGrounded && CheckIsOnSlope())
            {
                if (!isOnSharpSlope && !isMoving)
                {
                    bodyMaterial.staticFriction = 20;
                }
                else if (isOnSharpSlope)
                {
                    rb.AddForce(Vector3.down * slopeSlideMultiplier, ForceMode.Acceleration);
                }
            }
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private bool CheckIsOnSlope()
        {
            if (slopeHit.normal.y < slopeNormalized)
            {
                if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHight / 2 + 0.7f))
                {
                    if (slopeHit.normal != Vector3.up)
                    {
                        isOnSharpSlope = true;
                        return true;
                    }
                    else
                    {
                        isOnSharpSlope = false;
                        return false;
                    }
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHight / 2 + 0.5f))
                {
                    if (slopeHit.normal != Vector3.up)
                    {
                        isOnSharpSlope = false;
                        return true;
                    }
                    else
                    {
                        isOnSharpSlope = false;
                        return false;
                    }
                }
            }
            isOnSharpSlope = false;
            return false;
        }

        private void MoveInput()
        {
            if (!Managers.Spellcasting.effectBlockMoving)
            {
                horizontalMovement = Input.GetAxisRaw("Horizontal");
                verticalMovement = Input.GetAxisRaw("Vertical");

                moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
                if (moveDirection != Vector3.zero)
                {
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                }
            }
            else
            {
                moveDirection = Vector3.zero;
                isMoving = false;
            }
        }

        private void CheckInput()
        {
            if (!Managers.Spellcasting.effectBlockMoving)
            {
                if (Input.GetKeyDown(jumpKey) && isGrounded)
                {
                    Jump();
                }

                if (Input.GetKeyDown(dashKey) && isGrounded && dashReady)
                {
                    Dash();
                }
            }
        }

        private void Jump()
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void Dash()
        {
            rb.AddForce(moveDirection * dashForce, ForceMode.Impulse);
            dashReady = false;
            StartCoroutine(dashCooldownReloading());
        }

        private void ControlDragAndLanding()
        {
            if (isGrounded)
            {
                currentHelper = 100;
                rb.drag = groundDrag;
            }
            else if (rb.velocity.y >= 0)
            {
                rb.drag = airDrag;
            }
            else
            {
                rb.drag = airDrag;
                if (currentHelper < landingHelper)
                    currentHelper += 15f;
                rb.AddForce(Vector3.down * currentHelper * Time.deltaTime, ForceMode.Acceleration);
            }
        }

        private void ControlSpeed()
        {
            if (Input.GetKey(sprintKey) && isGrounded)
            {
                moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            }
            else
            {
                moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            }
        }

        private void MovePlayer()
        {
            if (isGrounded && !CheckIsOnSlope())
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            }
            else if (isGrounded && CheckIsOnSlope())
            {
                if (!isOnSharpSlope)
                {
                    rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
                }
            }
            else if (!isGrounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Acceleration);
            }
        }

        private IEnumerator dashCooldownReloading()
        {
            yield return new WaitForSeconds(dashCooldown);
            dashReady = true;
        }
    }
}
