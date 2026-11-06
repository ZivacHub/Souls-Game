using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;


public class EnemyControllerBoss : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public float frameRate = 30f;
    public DetectionAreaBoss detectionAreaBoss;
    public GameObject playerObject;
    public GameObject objectToTurnOff;
    public GameObject firstGameObject; // New variable to represent the first game object
    public GameObject objectToMatchPosition; // Second game object whose position will match the first one
    public SkeletonIdleSpritesBoss idle;
    public SkeletonWalkingSpritesBoss walking;
    public GameObject aiPathGameObject; // Reference to the GameObject with the AIPath component
    private AIPath aiPath;
    private Vector2 direction;
    public float walkSpeed = 1.5f;
    private bool isMovementLocked = false; // Flag to check if movement is locked
    private float movementLockDuration = 0.5f; // Duration of movement lock
    public EnemyAttack enemyAttack; // Reference to PlayerAttack script
    void Start()
    {
        objectToTurnOff.SetActive(false);
        // Get the AIPath component attached to the enemy
        if (aiPathGameObject != null)
        {
            aiPath = aiPathGameObject.GetComponent<AIPath>();
        }
        else
        {
            Debug.LogError("AIPath GameObject reference is not set in the EnemyController script.");
        }
    }

    private void Update()
    {
        CollisionDetection();
        MatchPositions();
        UpdateSkeletonSprite();
        if (aiPath != null && !isMovementLocked)
        {
            direction = aiPath.desiredVelocity.normalized;
            body.velocity = direction * walkSpeed;
        }

        // Check if attacking and set attack direction
        if (enemyAttack != null && enemyAttack.IsAttacking())
        {
            enemyAttack.SetAttackDirection(GetAttackDirection());
            body.velocity = direction * 0;
            StartCoroutine(LockMovement());
        }

    }

    private void CollisionDetection()
    {
        DetectionAreaBoss playerDetectionBoss = FindObjectOfType<DetectionAreaBoss>();
        // Check if the collider involves both the player object and the target object
        if (playerDetectionBoss.collisionDetectedBoss == true)
        {
            Debug.Log("Player entered the trigger zone of the target object!");
            objectToTurnOff.SetActive(true);
        }
    }
    private void UpdateSkeletonSprite()
    {
        List<Sprite> directionSprites = GetSpriteDirection();
        if (directionSprites != null && directionSprites.Count > 0)
        {
            int spriteIndex = Mathf.FloorToInt(Time.time * frameRate) % directionSprites.Count;
            spriteRenderer.sprite = directionSprites[spriteIndex];
        }
    }

    private List<Sprite> GetSpriteDirection()
    {
        List<Sprite> selectedSprites = null;
        if (direction.y > 0) // Moving up
        {
            if (direction.x > 0) selectedSprites = walking.upRightSprites;
            else if (direction.x < 0) selectedSprites = walking.upLeftSprites;
            else selectedSprites = walking.upSprites;
        }
        else if (direction.y < 0) // Moving down
        {
            if (direction.x > 0) selectedSprites = walking.downRightSprites;
            else if (direction.x < 0) selectedSprites = walking.downLeftSprites;
            else selectedSprites = walking.downSprites;
        }
        else // Not moving vertically
        {
            if (direction.x > 0) selectedSprites = walking.rightSprites;
            else if (direction.x < 0) selectedSprites = walking.leftSprites;
        }
        return selectedSprites;
    }

    // Determine the attack direction based on player movement
    int GetAttackDirection()
    {
        if (direction.y > 0) // Moving up
        {
            if (direction.x > 0) return 5; // Up Right
            else if (direction.x < 0) return 4; // Up Left
            else return 0; // Up
        }
        else if (direction.y < 0) // Moving down
        {
            if (direction.x > 0) return 7; // Down Right
            else if (direction.x < 0) return 6; // Down Left
            else return 1; // Down
        }
        else // Not moving vertically
        {
            if (direction.x > 0) return 3; // Right
            else if (direction.x < 0) return 2; // Left
            else return -1; // No direction
        }
    }

    private void MatchPositions()
    {
        if (firstGameObject != null && objectToMatchPosition != null)
        {
            // Set the position of the second game object to match the position of the first game object
            objectToMatchPosition.transform.position = firstGameObject.transform.position;
        }
    }
    IEnumerator LockMovement()
    {
        isMovementLocked = true;
        yield return new WaitForSeconds(movementLockDuration);
        isMovementLocked = false;
    }
}

[System.Serializable]
public class SkeletonIdleSpritesBoss
{
    public List<Sprite> upSprites;
    public List<Sprite> downSprites;
    public List<Sprite> rightSprites;
    public List<Sprite> leftSprites;
    public List<Sprite> upRightSprites;
    public List<Sprite> upLeftSprites;
    public List<Sprite> downRightSprites;
    public List<Sprite> downLeftSprites;
}

[System.Serializable]
public class SkeletonWalkingSpritesBoss
{
    public List<Sprite> upSprites;
    public List<Sprite> downSprites;
    public List<Sprite> rightSprites;
    public List<Sprite> leftSprites;
    public List<Sprite> upRightSprites;
    public List<Sprite> upLeftSprites;
    public List<Sprite> downRightSprites;
    public List<Sprite> downLeftSprites;
}
