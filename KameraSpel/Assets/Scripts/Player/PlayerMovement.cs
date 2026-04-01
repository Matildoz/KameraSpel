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
    

    [Header("Jump settings")]
    [SerializeField] float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float rayLength = 2f;


    float bodyRot;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Camera camera = playerHead.GetComponent<Camera>();
      
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(0, lookInput.x * mouseSensitivity * Time.deltaTime, 0);
        //Player rotation
        bodyRot += lookInput.x * mouseSensitivity * Time.deltaTime;
        headRotation += -lookInput.y * mouseSensitivity * Time.deltaTime;
        headRotation = Mathf.Clamp(headRotation, -maxHeadRotation, maxHeadRotation);
       
        playerHead.localRotation = Quaternion.Euler(headRotation, bodyRot, 0);
       
    }
    private void FixedUpdate()
    {
        GroundCheck();
        //Movement code

        float yVelocity = rb.linearVelocity.y;
        targetVelocity = transform.forward * moveInput.y * (movementSpeed + sprintSpeed) + transform.right * moveInput.x * (movementSpeed + sprintSpeed);
        Vector3 velocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, 1 - sluggishness);
        velocity.y = yVelocity;
      
        rb.linearVelocity = velocity;

      
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
