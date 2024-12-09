using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float InitialSpeed;
    [SerializeField] private float JumpPower;
    [SerializeField] private float Gravity;
    [SerializeField] public float SpeedModifier;
    [SerializeField] public float JumpModifier;
    [SerializeField] public float GravityModifier;

    private CharacterController controller;
    //private Vector2 inputDirection;
    private Vector3 movementDirection;
    private float velocity;
    
    private float _movementSpeed => InitialSpeed * SpeedModifier;
    private float _gravity => Gravity * GravityModifier;
    private float _jumpPower => JumpPower * JumpModifier;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        ApplyGravity();
        ApplyMovement();
    }

    private void ApplyGravity()
    {
        if(controller.isGrounded && velocity < 0.0f)
        {
            velocity = -1;
        }
        else
        {
            velocity += _gravity * Time.deltaTime;
        }

        movementDirection.y = velocity;
    }

    private void ApplyMovement()
    {
        controller.Move(Time.deltaTime * movementDirection);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float speedX = _movementSpeed * Input.GetAxis("Vertical");
        float speedY = _movementSpeed * Input.GetAxis("Horizontal");
        movementDirection = (forward * speedX) + (right * speedY);
    }

/*
    public void MoveAction(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }
*/
    public void JumpAction(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(controller.isGrounded)
            {
                velocity += _jumpPower;
            }
        }
    }
}