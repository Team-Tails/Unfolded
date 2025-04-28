using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] //These SerializeFields mean that these variables are set in editor
    private CharacterController characterController;
    [SerializeField]
    private float moveSpeed, jumpForce;
    private Vector2 moveInput;
    private Vector3 playerVelocity;
    private bool isGrounded;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private bool isMovingBackwards;
    [SerializeField]
    private Animator flipAnimator;
    private const float GRAVITY = -9.81f;
    private const float JUMPMULT = -2.0f;

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");  
        Vector3 move = new(moveInput.x, 0, moveInput.y);  
        move.Normalize();

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * JUMPMULT * GRAVITY);
        }

        playerVelocity.y += GRAVITY * Time.deltaTime;


        animator.SetBool("onGround", isGrounded);

        HandleAnimationFlip();
        
        animator.SetBool("movingBackwards", isMovingBackwards);
       
        Vector3 finalMovement = (move * moveSpeed) + (playerVelocity.y * Vector3.up);
        characterController.Move(finalMovement * Time.deltaTime);

        animator.SetFloat("moveSpeed", characterController.velocity.magnitude);

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
