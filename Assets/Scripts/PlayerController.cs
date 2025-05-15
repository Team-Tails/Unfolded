using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] //These SerializeFields mean that these variables are set in editor
    private CharacterController characterController;
    [SerializeField]
    private float moveSpeed, baseJumpForce;
    private Vector2 moveInput;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimer = 0.0f;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private ParticleSystem jumpParticles;

    private const float GRAVITY = -9.81f;
    private const float JUMPMULT = -2.0f;
    [SerializeField]
    private PlayerStateController stateController;

    private void Start()
    {
        stateController.OnStateChange.AddListener(OnStateChange);
    }

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

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
        }

        float stateGravity = stateController.CurrentState != null ? stateController.CurrentState.GravityMultiplier : 1;

        playerVelocity.y += GRAVITY * stateGravity * Time.deltaTime;


        animator.SetBool("onGround", isGrounded);

        HandleAnimationFlip();
       
        Vector3 finalMovement = (move * moveSpeed) + (playerVelocity.y * Vector3.up);
        characterController.Move(finalMovement * Time.deltaTime);

        animator.SetFloat("moveSpeed", characterController.velocity.magnitude);
        animator.SetBool("jumpCharging", isJumping);
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
            jumpParticles.Play();
        }
    }

    private void OnStateChange(PlayerState state, PlayerState oldState)
    {
        if (oldState == stateController.BunnyState)
        {
            animator.SetTrigger("exitRabbit");
        }
        else if (oldState == stateController.RhinoState)
        {
            animator.SetTrigger("exitRhino");
        }
        else if (oldState == stateController.PlaneState)
        {
            animator.SetTrigger("exitPlane");
        }

        if (state == stateController.BunnyState)
        {
            animator.SetTrigger("changeRabbit");
        }
        else if (state == stateController.RhinoState) 
        {
            animator.SetTrigger("changeRhino");
        }
        else if (state == stateController.PlaneState)
        {
            animator.SetTrigger("changePlane");
        }
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
        spriteRenderer.transform.LookAt(Camera.main.transform.position);
        spriteRenderer.transform.rotation = Quaternion.Euler(0, spriteRenderer.transform.rotation.y, 0);

        if (spriteRenderer.flipX && moveInput.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (!spriteRenderer.flipX && moveInput.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
