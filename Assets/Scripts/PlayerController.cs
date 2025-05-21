using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private float moveSpeed, baseJumpForce, launchHeight;
    [HideInInspector] public bool isFlying;
    private Vector2 moveInput;
    private Vector3 playerVelocity, lastFramesVelocity = Vector3.zero;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimer = 0.0f;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private ParticleSystem jumpParticles;
    [SerializeField] private float minWalkAudioDelay;
    [SerializeField] private float maxWalkAudioDelay;

    private const float GRAVITY = -9.81f;
    private const float JUMPMULT = -2.0f;
    [SerializeField]
    private PlayerStateController stateController;
    private Rigidbody rb;
    private Coroutine walkDelayCoroutine;





    private void Start()
    {
        stateController.OnStateChange.AddListener(HandleStateChange);
        rb = GetComponent<Rigidbody>();
    }

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
            if (walkDelayCoroutine == null)
            {
                walkDelayCoroutine = StartCoroutine("DelayWalkSounds");
            }
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

        Vector3 horizontalMovement = move * moveSpeed;

        // make plane constantly move forward
        if (move == Vector3.zero && isFlying)
        {
            horizontalMovement = lastFramesVelocity;
        }
        else
        {
            lastFramesVelocity = move * moveSpeed;
        }

        Vector3 finalMovement = horizontalMovement + (playerVelocity.y * Vector3.up);
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
            SoundManager.Instance.PlaySound("SmallJump", 1.2f);
        }
        if ((context.performed || context.canceled) && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt((baseJumpForce + (jumpTimer * 4.3f)) * JUMPMULT * GRAVITY * stateController.CurrentState.JumpHeight);
            isJumping = false;
            jumpTimer = 0.0f;
            jumpParticles.Play();
        }
    }

    private void HandleStateChange(PlayerState state, PlayerState oldState)
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

    public void OnLaunch()
    {
        rb.isKinematic = false;
        SoundManager.Instance.PlaySound("BigJump", 5);
        playerVelocity.y = launchHeight;
        isFlying = true;
    }

    public void OnRabbitChange(InputAction.CallbackContext context)
    {
        // Automatically checks if the state is already rabbit, and then does nothing.
        stateController.ChangeState(stateController.BunnyState);
        rb.isKinematic = true;
        if (isFlying) isFlying = false;
    }

    public void OnRhinoChange(InputAction.CallbackContext context)
    {
        // Automatically checks if the state is already rhino, and then does nothing.
        stateController.ChangeState(stateController.RhinoState);
        rb.isKinematic = false;
        if (isFlying) isFlying = false;
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

    private IEnumerator DelayWalkSounds()
    {
        float delay = Random.Range(minWalkAudioDelay, maxWalkAudioDelay);
        SoundManager.Instance.PlaySound("PlayerStep", 0.5f);

        yield return new WaitForSeconds(delay);

        walkDelayCoroutine = null;
    }
}
