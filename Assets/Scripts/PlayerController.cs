using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private CharacterController characterController;
    [SerializeField]
    private float moveSpeed, jumpForce;
    private Vector2 moveInput;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimer = 0.0f;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private bool isMovingBackwards;
    [SerializeField]
    private Animator flipAnimator;
    private const float GRAVITY = -9.81f;
    private const float JUMPMULT = -2.0f;

    public enum PlayerState { Rabbit, Rhino }
    private PlayerState playerState = PlayerState.Rabbit;

    [Header("UI Icons")]
    [SerializeField] private Image rabbitIcon;
    [SerializeField] private Image rhinoIcon;

    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new(moveInput.x, 0, moveInput.y);
        move.Normalize();

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
        }
        playerVelocity.y += GRAVITY * Time.deltaTime;

        animator.SetBool("onGround", isGrounded);

        HandleAnimationFlip();

        animator.SetBool("movingBackwards", isMovingBackwards);

        Vector3 finalMovement = (move * moveSpeed) + (playerVelocity.y * Vector3.up);
        characterController.Move(finalMovement * Time.deltaTime);

        animator.SetFloat("moveSpeed", characterController.velocity.magnitude);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            isJumping = true;
        }
        if ((context.performed || context.canceled) && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt((jumpForce + (jumpTimer * 4.3f)) * JUMPMULT * GRAVITY);
            isJumping = false;
            jumpTimer = 0.0f;
        }
    }

    public void OnRabbitChange(InputAction.CallbackContext context)
    {
        if (context.performed && playerState != PlayerState.Rabbit)
        {
            playerState = PlayerState.Rabbit;
            UpdateIconOpacity();
        }
    }

    public void OnRhinoChange(InputAction.CallbackContext context)
    {
        if (context.performed && playerState != PlayerState.Rhino)
        {
            playerState = PlayerState.Rhino;
            UpdateIconOpacity();
        }
    }

    private void UpdateIconOpacity()
    {
        if (rabbitIcon != null && rhinoIcon != null)
        {
            Color rabbitColor = rabbitIcon.color;
            Color rhinoColor = rhinoIcon.color;

            if (playerState == PlayerState.Rabbit)
            {
                rabbitColor.a = 1f;
                rhinoColor.a = 0.25f;
            }
            else
            {
                rabbitColor.a = 0.25f;
                rhinoColor.a = 1f;
            }

            rabbitIcon.color = rabbitColor;
            rhinoIcon.color = rhinoColor;
        }
    }

    void HandleAnimationFlip()
    {
        if (!spriteRenderer.flipX && moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
            flipAnimator.SetTrigger("Flip");
        }
        else if (spriteRenderer.flipX && moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
            flipAnimator.SetTrigger("Flip");
        }

        if (!isMovingBackwards && moveInput.y > 0)
        {
            isMovingBackwards = true;
            flipAnimator.SetTrigger("Flip");
        }
        else if (isMovingBackwards && moveInput.y < 0)
        {
            isMovingBackwards = false;
            flipAnimator.SetTrigger("Flip");
        }
    }
}
