using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonControllerScript : MonoBehaviour
{
    /*
    1.
    Title: FIRST PERSON MOVEMENT in Unity - FPS Controller
    Author: Asbj�rn Thirslund / Brackeys
    Date: 28 July 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=_QajrabyTJc

    2.
    Title: Easy Camera Zoom in Unity 3D! 2024 Tutorial
    Author: Matt's Computer Lab
    Date: 13 August 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=oGVbC7ooUWI
    */

    public bool canJump = true;
    public CharacterController controller;

    [Header("Base Movement")]
    public float speed = 12f;
    public float baseSpeed;
    public float sprintSpeed;
    public float gravity = -9.81f;//though -19.81f seems to work better with the jump mechanic

    [Header("Jumping Mechanic")]
    public float jumpHeight = 3f;
    public float groundDistance = .4f;//radius of Sphere to be used to check ground
    public Transform groundCheck;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public GameObject firstPersonCam;

    private float xRotation = 0f;

    [Header("Camera Zoom")]
    public float zoomFOV;
    public float normalFOV;
    public float zoomSpeed;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        firstPersonCam = GameObject.FindWithTag("MainCamera");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleSight();

        if (controller.gameObject.transform.position.y <= -20)
        {
            FindAnyObjectByType<SceneManagerScript>().Restart();
            FindAnyObjectByType<AudioManagerScript>().Play("Death");
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);//sphere is created from groundcheck gameObject, checking that the player is grounded

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;//resets the velocity that would otherwise build continiously
        }

        Vector3 move = transform.right * moveInput.x + transform.forward *
        moveInput.y;

        controller.Move(move * speed * Time.deltaTime);//"Time.deltaTime" means that the movement speed is now framerate independent

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void HandleSight()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);//clamps the rotation, meaning, can never rotate till camera view inverts

        firstPersonCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);//quaternions are responsible for rotations
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void OnCameraZoom(InputAction.CallbackContext context)
    {
        float targetFOV;
        Camera camera = firstPersonCam.GetComponent<Camera>();

        if (context.performed)
        {
            targetFOV = zoomFOV;
            camera.fieldOfView = targetFOV;//???
        }
        else if (context.canceled)
        {
            targetFOV = normalFOV;
            camera.fieldOfView = targetFOV;
        }

        //camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);//"Lerping" - smoothly moving from one value to another (value 1, to value 2, multiplied by speed)
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded && canJump)
        {
            //Debug.Log("Jump" + context.phase);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            FindAnyObjectByType<AudioManagerScript>().Play("Jump");
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            speed = sprintSpeed;
        }
        else if (context.canceled)
        {
            speed = baseSpeed;
        }
    }
}
