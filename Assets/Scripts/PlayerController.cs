using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 9f;
    [SerializeField] private float lookSensitivity = 0.1f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -20f;

    private CharacterController controller;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction sprintAction;
    private InputAction jumpAction;
    private float pitch;
    private float verticalSpeed;
 

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Player/Move");
        lookAction = InputSystem.actions.FindAction("Player/Look");
        sprintAction = InputSystem.actions.FindAction("Player/Sprint");
        jumpAction = InputSystem.actions.FindAction("Player/Jump");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 look = lookAction.ReadValue<Vector2>() * lookSensitivity;
        transform.Rotate(0f, look.x,0f);
        pitch = Math.Clamp(pitch - look.y, -80f, 80f);
        playerCamera.localEulerAngles = new Vector3(pitch, 0f, 0f);

        Vector2 input = moveAction.ReadValue<Vector2>();
        float speed = sprintAction.IsPressed() ? sprintSpeed : walkSpeed;
        Vector3 velocity = (transform.right * input.x + transform.forward * input.y) * speed;
        if(controller.isGrounded && verticalSpeed < 0f)
        {
            verticalSpeed = -2f;
        }
        if(controller.isGrounded && jumpAction.WasPressedThisFrame())
        {
            verticalSpeed = (float) Math.Sqrt(-2f * gravity * jumpHeight);
        }
        verticalSpeed += gravity * Time.deltaTime;
        velocity.y = verticalSpeed;
        
        controller.Move(velocity * Time.deltaTime);
    }
}
