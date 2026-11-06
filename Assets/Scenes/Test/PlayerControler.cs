using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement
    public Animator animator; // Reference to the Animator component

    private Vector2 lastDirection = Vector2.down; // Last direction the player moved

    private float attackTime = 1f;
    private bool isAttacking = false;

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
        if (!isAttacking)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector2 moveInput = new Vector2(horizontalInput, verticalInput).normalized;
            if (moveInput != Vector2.zero)
            {
                lastDirection = moveInput.normalized;

                // Calculate the animation state based on the direction of movement
                string animationState = GetAnimationState(lastDirection);
                ChangeAnimationState(animationState);

                // Move the player
                transform.Translate(moveInput * moveSpeed * Time.deltaTime);
            }
            else
            {
                // If no movement input, go back to standing state
                ChangeAnimationState(standing);
            }

            if (Input.GetMouseButtonDown(0))
            {
                isAttacking = true;
                string attackState = GetAttackState(lastDirection);
                ChangeAnimationState(attackState);
                StartCoroutine(EndAttackAnimation(attackTime));
            }
        }
    }

    IEnumerator EndAttackAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        isAttacking = false;
        ChangeAnimationState(standing);
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
}
