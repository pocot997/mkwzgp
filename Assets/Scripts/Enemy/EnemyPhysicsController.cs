using UnityEngine;

public class EnemyPhysicsController : MonoBehaviour
{
    [Header("Jumping")]
    [SerializeField] private float landingHelper = 1.5f;
    [SerializeField] private float currentHelper = 0f;

    [Header("Drag")]
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 1f;

    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 0.4f;

    private Rigidbody rb;
    private float playerHight = 2f;
    private bool isGrounded;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, playerHight/2, 0), groundDistance, groundMask);

        ControlDragAndLanding();
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
            {
                currentHelper += 15f;
            }
            rb.AddForce(Vector3.down * currentHelper * Time.deltaTime, ForceMode.Acceleration);
        }
    }
}
