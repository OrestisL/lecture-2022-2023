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
    private float rotationX, rotationY;

    [Header("Jumping")]
    public float gravity = -9.81f;


    public enum InvertCamera
    {
        inverted = -1,
        non_inverted = 1
    }

    public InvertCamera invertCameraStatus;

    private void Start()
    {
        sqrMovementDeadzone = movementDeadzone * movementDeadzone;

        if (!cam)
            cam = Camera.main.transform;

        if (!controller)
            controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
        Rotation();
        Jump();
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
        //add to rotations
        rotationY += mouseX * Time.deltaTime;
        rotationX += (int)invertCameraStatus * mouseY;
        //clamp camera rotation
        rotationX = Mathf.Clamp(rotationX, -maxAngle, maxAngle);
        //apply rotations
        transform.Rotate(Vector3.up, mouseSensitivity * mouseX * Time.deltaTime);
        cam.transform.localRotation = Quaternion.Euler(new Vector3(rotationX, 0, 0));
    }

    void Jump() 
    {
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * gravity * Time.deltaTime);
        }

    }
}
