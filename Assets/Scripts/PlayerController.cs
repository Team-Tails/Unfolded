using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody theRB;
    public float moveSpeed, jumpForce;

    private Vector2 moveInput;

    public LayerMask whatIsGround;
    public Transform groundPoint;
    private bool isGrounded;

    public Animator anim;

    public SpriteRenderer theSR;

    private bool movingBackwards;

    public Animator flipAnim;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();

        theRB.velocity = new Vector3(moveInput.x * moveSpeed, theRB.velocity.y, moveInput.y * moveSpeed);

        anim.SetFloat("moveSpeed", theRB.velocity.magnitude);

        RaycastHit hit;
        if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, .3f, whatIsGround))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            theRB.velocity += new Vector3(0f, jumpForce, 0f);
        }

        anim.SetBool("onGround", isGrounded);

        if(!theSR.flipX && moveInput.x < 0)
        {
            theSR.flipX = true;
            flipAnim.SetTrigger("Flip");
        }
        else if (theSR.flipX && moveInput.x > 0)
        {
            theSR.flipX = false;
            flipAnim.SetTrigger("Flip");
        }

        if(!movingBackwards && moveInput.y > 0)
        {
            movingBackwards = true;
            flipAnim.SetTrigger("Flip");

        }
        else if (movingBackwards && moveInput.y < 0)
        {
            movingBackwards = false;
            flipAnim.SetTrigger("Flip");
        }
        anim.SetBool("movingBackwards", movingBackwards);
       
    }
}
