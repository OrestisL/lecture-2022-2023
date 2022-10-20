using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;

    [Header("Mouse")]
    public float mouseSensitivity;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    private float currentSpeed;

    public float movementDeadzone = 0.1f;
    private float sqrMovementDeadzone = 0;

    [Header("Rotation")]
    public float maxAngle;
    private float rotationX;

    [Header("Jumping")]
    public float gravity = 9.81f;
    public float walkJumpHeight = 1.5f;
    public float runJumpHeight = 2.5f;
    private float currentJumpHeight = 0f;
    [SerializeField] private float verticalVelocity = 0f;

    public enum InvertCamera
    {
        inverted = -1,
        non_inverted = 1
    }

    public InvertCamera invertCameraStatus;

    private void Start()
    {
        sqrMovementDeadzone = movementDeadzone * movementDeadzone;
        //ensure gravity is positive
        gravity = Mathf.Abs(gravity);

        if (!cam)
            cam = Camera.main.transform;

        if (!controller)
            controller = GetComponent<CharacterController>();

       
        SavePlayer.LoadPlayerData(out SavePlayer.PlayerData data);
        if (data != null)
        {
            transform.position = data.position;
            transform.rotation = data.rotation;
            rotationX = data.camRotation.x;
        }
    }

    private void Update()
    {
        Movement();
        Rotation();
        Jump();

        if (Input.GetKeyDown(KeyCode.U))
        {
            SavePlayer.PlayerData data = new SavePlayer.PlayerData(transform, cam);
            SavePlayer.SavePlayerData(data);
        }
    }

    void Movement()
    {
        //get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0, z);

        //apply movement to controller
        if (direction.sqrMagnitude > sqrMovementDeadzone)
        {
            //Quatiernion.AngleAxis creates a rotation of degrees around axis
            //Quaternion * Vector3 = rotate vector3 by quaternion
            direction = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * direction;

            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = runSpeed;
            else
                currentSpeed = walkSpeed;

            controller.Move(currentSpeed * direction.normalized * Time.deltaTime);
        }
    }

    void Rotation()
    {
        //get input from mouse
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //add to rotation
        rotationX += (int)invertCameraStatus * mouseY;
        //clamp camera rotation
        rotationX = Mathf.Clamp(rotationX, -maxAngle, maxAngle);
        //apply rotations
        transform.Rotate(Vector3.up, mouseSensitivity * mouseX * Time.deltaTime);
        cam.transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.right);//Quaternion.Euler(new Vector3(rotationX, 0, 0));
    }

    void Jump() 
    {
        if (!controller.isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else //controller.isGrounded = true
        {
            verticalVelocity = 0f;

            if (Input.GetKey(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    currentJumpHeight = runJumpHeight;
                else
                    currentJumpHeight = walkJumpHeight;

                verticalVelocity = Mathf.Sqrt(2 * gravity * currentJumpHeight);
            }
        }

        //check for head collision
        if (controller.collisionFlags == CollisionFlags.Above)
        {
            verticalVelocity = -gravity * 0.2f;
        }

        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }
}
