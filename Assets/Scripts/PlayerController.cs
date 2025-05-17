using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] //These SerializeFields mean that these variables are set in editor
    private CharacterController characterController;
    [SerializeField]
    private float moveSpeed, baseJumpForce, launchHeight, flyingForwardSpeed;
    public bool isFlying;
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
    [SerializeField]
    private PlayerStateController stateController;

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new(moveInput.x, 0, moveInput.y);  
        move.Normalize();

        if (isFlying)
        {
            move += Vector3.forward * flyingForwardSpeed;
        }
        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
        }
        playerVelocity.y += GRAVITY * stateController.CurrentState.GravityMultiplier * Time.deltaTime;


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
        if ((context.performed || context.canceled) && isGrounded )
        {
            playerVelocity.y = Mathf.Sqrt((baseJumpForce + (jumpTimer * 4.3f)) * JUMPMULT * GRAVITY * stateController.CurrentState.JumpHeight);
            isJumping = false;
            jumpTimer = 0.0f;
        }
    }

    public void OnLaunch()
    {
        playerVelocity.y = launchHeight;
        isFlying = true;
    }

    public void OnRabbitChange(InputAction.CallbackContext context)
    {
        // Automatically checks if the state is already rabbit, and then does nothing.
        stateController.ChangeState(stateController.BunnyState);
    }

    public void OnRhinoChange(InputAction.CallbackContext context)
    {
        // Automatically checks if the state is already rhino, and then does nothing.
        stateController.ChangeState(stateController.RhinoState);
    }
    void HandleAnimationFlip()
    {
        if(!spriteRenderer.flipX && moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
            flipAnimator.SetTrigger("Flip");
        }
        else if (spriteRenderer.flipX && moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
            flipAnimator.SetTrigger("Flip");
        }

        if(!isMovingBackwards && moveInput.y > 0)
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
