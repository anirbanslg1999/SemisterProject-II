using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public enum MoveDirection
    {
        Front, Back , Left, Right
    }
    [SerializeField]
    float moveSpeed;

    float xHorizontalInput;
    float yVerticalInput;

    Vector2 movementInput;
    Vector2 velocityInput;

    Rigidbody2D rb2d;
    Animator animator;
    MoveDirection moveDirection = MoveDirection.Front;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateDirection();
    }

    void UpdateDirection()
    {
        if(Mathf.Abs(xHorizontalInput) > Mathf.Abs(yVerticalInput))
        {
            if(xHorizontalInput > 0)
            {
                moveDirection = MoveDirection.Right;
            }
            else if(xHorizontalInput < 0)
            {
                moveDirection = MoveDirection.Left;
            }
        }
        else if(Mathf.Abs(xHorizontalInput) < Mathf.Abs(yVerticalInput))
        {
            if(yVerticalInput > 0)
            {
                moveDirection = MoveDirection.Back;
            }
            else if(yVerticalInput < 0)
            {
                moveDirection = MoveDirection.Front;
            }
        }
        animator.SetInteger("DirectionInput", (int)moveDirection);
    } 

    void UpdateMovement()
    {
        xHorizontalInput = Input.GetAxisRaw("Horizontal");
        yVerticalInput = Input.GetAxisRaw("Vertical");
        movementInput = new Vector2(xHorizontalInput, yVerticalInput);
        velocityInput = movementInput.normalized * moveSpeed;
        animator.SetFloat("MovementInput", velocityInput.magnitude);
    }

    private void FixedUpdate()
    {
        if(rb2d != null)
        {
            rb2d.MovePosition(rb2d.position + velocityInput * Time.deltaTime);
        }
    }


}
