using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public enum MoveDirection
    {
        Front, Back , Left, Right, Max
    }
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    List<BoxCollider2D> m_collider = new List<BoxCollider2D>();

    float xHorizontalInput;
    float yVerticalInput;

    Vector2 movementInput;
    Vector2 velocityInput;

    Rigidbody2D rb2d;
    Animator animator;
    MoveDirection moveDirection = MoveDirection.Front;
    public bool isAttacking = false;

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
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            UpdateAttacking();
        }
        DisplayCollider();
    }
    void UpdateAttacking()
    {
        if (animator)
        {
            animator.SetBool("IsAttacking", isAttacking);
        }
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
    public void SetAttacking(bool m_isAttacking)
    {
        isAttacking = m_isAttacking;
        UpdateAttacking();

    }
    public void DisplayCollider()
    {
        if (isAttacking)
        {
            ActivateCollider();
        }
        else 
        { 
            DeactivateAllCollider();
        }
    }


    private void ActivateCollider()
    {
        m_collider[(int)moveDirection].enabled = true;
    }
    private void DeactivateAllCollider()
    {
        for (int index = 0; index < (int)MoveDirection.Max; index++)
        {
            m_collider[index].enabled = false;
        }
    }
}
