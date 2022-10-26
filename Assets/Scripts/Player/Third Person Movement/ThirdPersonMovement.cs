using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam;
    public CharacterController characterController;
    public Animator anim;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    private float currentSpeed;
    private bool isRunning;
    public bool canMove;
    public bool isMoving;

    [Header("Jumping")]
    public LayerMask groundLayer;
    public float walkJumpHeight, runJumpHeight;
    private float currentJumpHeight;
    public bool isHeadColliding;
    public float gravity;

    public Vector3 velocity;

    //initialization
    private void OnEnable()
    {
        cam = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        Movement();
        Jump();
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    //tags
    //    if (hit.gameObject.CompareTag("Moveable"))
    //    {
    //        Vector3 dir = hit.point - transform.position;
        
    //        Rigidbody otherBody = hit.gameObject.GetComponent<Rigidbody>();
    //        if (otherBody == null)
    //        {
    //            //no rigibody attached, move transform
    //            hit.transform.position = Vector3.MoveTowards(hit.transform.position, hit.transform.position + dir, Time.deltaTime);
    //        }
    //        else
    //        {
    //            //rigibody exists apply force
    //            otherBody.AddForce(dir);
    //        }
    //    }

    //    //layers
    //    //layermask.compareto returns 0 if it hits the layers you want
    //    if (hit.gameObject.layer.CompareTo(LayerMask.NameToLayer("Cube")) == 0)
    //    {
    //        Debug.Log("Layer hit");
    //    }
    //}

    private void Movement()
    {
        //get user input
        //a,d and left,right
        float x = Input.GetAxis("Horizontal");
        //w,s and up,down
        float z = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(x, 0, z).normalized;

        anim.SetFloat("Speed", currentSpeed * inputDirection.magnitude);
        anim.SetFloat("SpeedMult",currentSpeed * 2f);
        if (inputDirection.magnitude >= 0.1f)
        {
            isMoving = true;
            float targetAngle = cam.eulerAngles.y + Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

            if (canMove)
                transform.localRotation = Quaternion.Euler(0, targetAngle, 0);

            Vector3 moveDirection = transform.localRotation * Vector3.forward;
            //running code
            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = runSpeed;
            else
                currentSpeed = walkSpeed;

            if (!canMove)
                currentSpeed = 0;

            characterController.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }

    }

    private void Jump()
    {
        //in order for character controller.isgrounded to work properly,
        //move must be called before checking it
        //check if head collides with ceiling

        //isHeadColliding = Physics.CheckBox(headCheck.position, Vector3.one, Quaternion.identity, groundLayer);
        isHeadColliding = characterController.collisionFlags == CollisionFlags.Above;
        if (isHeadColliding)
        {
            velocity.y = gravity * 0.2f;
        }

        //apply gravity
        if (!characterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        //move controller
        characterController.Move(velocity * Time.deltaTime);

        //jump
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space) && canMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                currentJumpHeight = runJumpHeight;
            else
                currentJumpHeight = walkJumpHeight;

            velocity.y = Mathf.Sqrt(-2f * currentJumpHeight * gravity);
        }


        //set velocity.y after touching the ground to avoid inf
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = gravity;
        }
        anim.SetBool("Jump", !characterController.isGrounded);
    }

    //public void JumpAnimation()
    //{
    //    anim.SetBool("Jump", false);
    //}   

}
