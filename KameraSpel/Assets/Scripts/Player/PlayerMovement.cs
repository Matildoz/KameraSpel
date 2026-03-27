using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float sluggishness;
    private Rigidbody rb;
    Vector2 moveInput;
    public bool isSprinting;
    Vector3 targetVelocity;

    [Header("Look settings")]
    [SerializeField] float mouseSensitivity;
    [SerializeField] float maxHeadRotation = 80f;
    public Transform playerHead;
    Vector2 lookInput;
    float headRotation;
    float sprintFOV = 90;
    float walkFOV = 60;
    [SerializeField] float viewSmoothing;
    float currentFOV;

    [Header("Jump settings")]
    [SerializeField] float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float rayLength = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Camera camera = playerHead.GetComponent<Camera>();
        currentFOV = camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {



    }
    private void FixedUpdate()
    {
        GroundCheck();
        //Movement code

        float yVelocity = rb.linearVelocity.y;
        targetVelocity = transform.forward * moveInput.y * (movementSpeed + sprintSpeed) + transform.right * moveInput.x * (movementSpeed + sprintSpeed);
        Vector3 velocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, 1 - sluggishness);
        velocity.y = yVelocity;
        /* if(!isSprinting)
         {
             Camera.main.fieldOfView = Mathf.MoveTowards(currentFOV,walkFOV, viewSmoothing * Time.deltaTime);
         }
         else
         {
             Camera.main.fieldOfView = Mathf.MoveTowards(currentFOV, sprintFOV, viewSmoothing * Time.deltaTime);
         }
        */
        rb.linearVelocity = velocity;

        //Player rotation
        transform.Rotate(0, lookInput.x * Time.deltaTime * mouseSensitivity, 0);
        headRotation = Mathf.Clamp(headRotation, -maxHeadRotation, maxHeadRotation);
        headRotation += -lookInput.y * Time.deltaTime * mouseSensitivity;
        playerHead.localRotation = Quaternion.Euler(headRotation, 0, 0);
    }
    private void LateUpdate()
    {

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {

        if (!context.canceled)
        {
            sprintSpeed = 5;

            isSprinting = true;
            Debug.Log(isSprinting);
        }
        else
        {


            isSprinting = false;
            Debug.Log(isSprinting);
            sprintSpeed = 0;
        }

    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isGrounded)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            }
            else
            {
                Debug.Log("Inte på marken");
            }
        }

    }

    public void GroundCheck()
    {
        //Draws a ray, if it hits the ground, the variable isgrounded becomes true, else, youre not on the ground and the variable becomes false
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength, groundLayer))
        {
            isGrounded = true;
            Debug.Log("IsGrounded");
        }
        else
        {
            isGrounded = false;
        }

    }


}
