using System.Collections;
using System.Collections.Generic;
using RPGCharacterAnims.Actions;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public PlayerAttack playerAttack; // Reference to PlayerAttack script
    public PlayerHPandST playerHPandST; // Reference to PlayerHPandST script

    public Animator animator; // Reference to the Animator component

    private Vector2 lastDirection = Vector2.up; // Last direction the player moved
    private bool isAttacking = false;

    public float moveSpeed = 5f;
    public float dashSpeed = 10f; // Speed for dashing
    public float dashDuration = 0.2f; // Duration of the dash
    public int staminaCost = 10; // Stamina cost for dashing

    private Vector2 direction;
    public bool isDashing = false; // Flag to check if dashing is active
    private bool isMovementLocked = false; // Flag to check if movement is locked
    private float movementLockDuration = 0.3f; // Duration of movement lock
    private float attackTime = 1f;

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //Standing State
    const string standing = "Standing";

    // Animation Walking States
    const string upWalking = "Up_Walking";
    const string upRightWalking = "UpRight_Walking";
    const string upLeftWalking = "UpLeft_Walking";
    const string downWalking = "Down_Walking";
    const string downLeftWalking = "DownLeft_Walking";
    const string downRightWalking = "DownRight_Walking";
    const string leftWalking = "Left_Walking";
    const string rightWalking = "Right_Walking";

    // Animation Attacking States
    const string upAttacking = "Up_Attacking";
    const string upRightAttacking = "UpRight_Attacking";
    const string upLeftAttacking = "UpLeft_Attacking";
    const string downAttacking = "Down_Attacking";
    const string downLeftAttacking = "DownLeft_Attacking";
    const string downRightAttacking = "DownRight_Attacking";
    const string leftAttacking = "Left_Attacking";
    const string rightAttacking = "Right_Attacking";

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interruption itself
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(newState)) return;
        //Play the animation
        animator.Play(newState);
    }
    void Update()
    {
        if (!isAttacking && !isDashing) // Added condition !isDashing
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            Vector2 moveInput = new Vector2(horizontalInput, verticalInput).normalized;

            PlayerHPandST playerHPandST = GetComponentInParent<PlayerHPandST>();
            PlayerAttack playerAttack = GetComponentInParent<PlayerAttack>();

            // Check for dash input
            if (Input.GetKeyDown(KeyCode.Space) && !isDashing && !isMovementLocked)
            {
                if(playerHPandST != null  && playerHPandST.stamina >= staminaCost){
                    StartCoroutine(Dash());
                }
            }

            //If there is not dash input, walk
            if (moveInput != Vector2.zero)
            {
                lastDirection = moveInput.normalized;

                // Calculate the animation state based on the direction of movement
                string animationState = GetAnimationState(lastDirection);
                ChangeAnimationState(animationState);

                // Move the player
                //body.velocity = direction * moveSpeed;
                transform.Translate(moveInput * moveSpeed * Time.deltaTime);
            }
            else
            {
                // If no movement input, go back to standing state
                ChangeAnimationState(standing);
                body.velocity = lastDirection * 0;
            }

            // Check if attacking and set attack direction
            if (playerAttack != null)
            {
                playerAttack.SetAttackDirection(GetAttackDirection());
            }   

            //Check if attacking
            if (Input.GetMouseButtonDown(0))
            {
                if (playerHPandST != null && playerHPandST.stamina >= playerAttack.staminaCost)
                {
                    // Deduct stamina for attacking
                    playerHPandST.stamina -= staminaCost;

                    isAttacking = true;
                    string attackState = GetAttackState(lastDirection);
                    ChangeAnimationState(attackState);
                    StartCoroutine(EndAttackAnimation(attackTime));
                }
            }

        }
    }

    string GetAnimationState(Vector2 direction)
    {
        if (direction == Vector2.up)
            return upWalking;
        else if (direction == Vector2.down)
            return downWalking;
        else if (direction == Vector2.left)
            return leftWalking;
        else if (direction == Vector2.right)
            return rightWalking;
        else if (direction == (Vector2.up + Vector2.right).normalized)
            return upRightWalking;
        else if (direction == (Vector2.up + Vector2.left).normalized)
            return upLeftWalking;
        else if (direction == (Vector2.down + Vector2.right).normalized)
            return downRightWalking;
        else if (direction == (Vector2.down + Vector2.left).normalized)
            return downLeftWalking;
        else
            return standing;
    }
    string GetAttackState(Vector2 direction)
    {
        if (direction == Vector2.up)
            return upAttacking;
        else if (direction == Vector2.down)
            return downAttacking;
        else if (direction == Vector2.left)
            return leftAttacking;
        else if (direction == Vector2.right)
            return rightAttacking;
        else if (direction == (Vector2.up + Vector2.right).normalized)
            return upRightAttacking;
        else if (direction == (Vector2.up + Vector2.left).normalized)
            return upLeftAttacking;
        else if (direction == (Vector2.down + Vector2.right).normalized)
            return downRightAttacking;
        else if (direction == (Vector2.down + Vector2.left).normalized)
            return downLeftAttacking;
        else
            return standing;
    }

    IEnumerator EndAttackAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isAttacking = false;
        ChangeAnimationState(standing);
    }
    IEnumerator Dash()
    {
        isDashing = true;
        PlayerHPandST playerHPandST = GetComponentInParent<PlayerHPandST>(); 
        playerHPandST.PlayerAction();
        // Check if there's enough stamina for dashing
        if (playerHPandST != null && playerHPandST.stamina >= staminaCost)
        {
            // Deduct stamina for dashing
            playerHPandST.stamina -= staminaCost;
            body.velocity = direction * dashSpeed; //treba dodati last direction
            yield return new WaitForSeconds(dashDuration);
            Physics2D.IgnoreLayerCollision(4, 4, false); // Ignore collision with enemies during dash
            isDashing = false;
            LockKeys();
            UnlockKeys();
        }
    }

    // Function to lock movement keys
    void LockKeys()
    {
        isMovementLocked = true;
        // Disable movement keys
        if (direction.y > 0) // Moving up
        {
            body.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }
        else if (direction.y < 0) // Moving down
        {
            body.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }

        if (direction.x > 0) // Moving right
        {
            body.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }
        else if (direction.x < 0) // Moving left
        {
            body.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }
    }

    void UnlockKeys()
    {
        isMovementLocked = false;
        body.constraints = RigidbodyConstraints2D.None;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Determine the attack direction based on player movement
    int GetAttackDirection()
    {
        string attackState = GetAttackState(lastDirection); // Get the attack animation state
        int attackDirection = -1; // Default value for no direction

        // Map each attack animation state to a corresponding attack direction
        switch (attackState)
        {
            case upAttacking:
                attackDirection = 0; // Up
                break;
            case downAttacking:
                attackDirection = 1; // Down
                break;
            case rightAttacking:
                attackDirection = 2; // Right
                break;
            case upLeftAttacking:
                attackDirection = 3; // Up Left
                break;
            case upRightAttacking:
                attackDirection = 4; // Up Right
                break;
            case downLeftAttacking:
                attackDirection = 5; // Down Left
                break;
            case downRightAttacking:
                attackDirection = 6; // Down Right
                break;
            case leftAttacking:
                attackDirection = 7; // Left
                break;
            default:
                attackDirection = -1; // No direction
                break;
        }

        return attackDirection;
    }


    IEnumerator LockMovement()
    {
        isMovementLocked = true;
        yield return new WaitForSeconds(movementLockDuration);
        isMovementLocked = false;
    }
}
