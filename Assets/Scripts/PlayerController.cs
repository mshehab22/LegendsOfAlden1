using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


// TODO: Part 11 at 31:00 mentions wall climbing 

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;

    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;
    public bool _isFacingRight = true;


    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _isRunning = false;

    [SerializeField] private float rollSpeed = 1f;        // tweak feel
    [SerializeField] private float rollDuration = 0.35f;  // match animation length


    private bool isRolling = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity) rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);
    }

    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool IsRunning
    {
        get { return _isRunning; }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning) { return runSpeed; }
                        else { return walkSpeed; }
                    }
                    else { return airWalkSpeed; } // Air move
                }
                else { return 0; } // Idle speed is 0
            }
            else { return 0; } // movement locked
        }
    }

    public bool IsFacingRight
    {
        get { return _isFacingRight; }

        private set
        {
            if (_isFacingRight != value)
            {
                // Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    public bool IsAlive
    {
        get { return animator.GetBool(AnimationStrings.isAlive); }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = (moveInput != Vector2.zero);
            SetFacingDirection(moveInput);
        }
        else { IsMoving = false; }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight) { IsFacingRight = true; } // Facing right direction
        else if (moveInput.x < 0 && IsFacingRight) { IsFacingRight = false; } // Facing left direction
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started) { IsRunning = true; }
        else if (context.canceled) { IsRunning = false; }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO add is Alive condition
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnDashAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.dashAttackTrigger);

            damageable.LockVelocity = true; // <- prevents FixedUpdate from overriding
            float dashForce = 12f;          // tweak
            float dir = IsFacingRight ? 1f : -1f;
            rb.linearVelocity = new Vector2(dir * dashForce, rb.linearVelocity.y);
            StartCoroutine(DashUnlock(0.20f));
        }
    }
    public void StopDash()
    {
        damageable.LockVelocity = false;                 // resume normal movement
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y); // optional: stop slide
    }
    private IEnumerator DashUnlock(float t)
    {
        yield return new WaitForSeconds(t);
        StopDash();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove && !damageable.LockVelocity && !isRolling)
        {
            animator.SetTrigger(AnimationStrings.rollTrigger);
        }
    }
    public void RollStart()
    {
        isRolling = true;
        damageable.LockVelocity = true;   // prevents FixedUpdate from overwriting
        damageable.ExternalInvincible = true;     // optional i-frames

        float dir = IsFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(dir * rollSpeed, 0f);
    }

    public void RollStop()
    {
        isRolling = false;
        damageable.LockVelocity = false;
        damageable.ExternalInvincible = false;

        // optional: stop any sliding
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    public void OnHit (int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }
}
