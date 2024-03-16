using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header ("Body")]
    public Rigidbody2D body;
    public float walkSpeed = 5f;
    
    [Header ("Soul")]
    public float soulMoveSpeed = 3f;
    public GameObject playerSoul;
    public float maxSoulDistance = 5f; // Maximum distance the soul can move from the player
    public float soulXOffset = 0f; // Offsets for the soul's position
    public float soulYOffset = 0f; 
    
    [Header ("Chain Renderer")]
    public float chainStartXOffset = 0f;  // Offsets for the chain
    public float chainStartYOffset = 0f;
    public float chainEndXOffset = 0f;
    public float chainEndYOffset = 0f;

    public Vector2 direction;  // The direction the player is moving in
    

    private Vector2 soulTarget; // The position the soul is moving towards
    private Coroutine returnSoulRoutine; // Coroutine for returning the soul to the player
    private bool isSoulActive = false;

    public LineRenderer chainRenderer; 
    public Material chainMaterial;
    
    [Header ("Animation")]
    public Animator animator;

    void Start()
    {
        if (chainRenderer != null)
        {
            chainRenderer.material = chainMaterial;
            chainRenderer.positionCount = 2;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isSoulActive = !isSoulActive;
            
            if (isSoulActive)
            {
                playerSoul.SetActive(true);
                Vector2 playerLocationOffset = new Vector2(transform.position.x + soulXOffset, transform.position.y + soulYOffset);
                playerSoul.transform.position = playerLocationOffset; // Spawn at player's position with offset
            }
            
            if (!isSoulActive)
            {
                if (returnSoulRoutine != null)  // Stop the return soul coroutine if it's running
                {
                    StopCoroutine(returnSoulRoutine);
                }
                returnSoulRoutine = StartCoroutine(ReturnSoulToPlayer());
            }
            
        }

        if (chainRenderer != null && playerSoul.activeSelf)  // Draw the chain between the player and the soul with offsets
        {
            chainRenderer.enabled = true;
            Vector2 soulPosition = playerSoul.transform.position;
            Vector3 offsetChainEndPosition = new Vector3(soulPosition.x + chainEndXOffset, soulPosition.y + chainEndYOffset, 0);
            chainRenderer.SetPosition(0, offsetChainEndPosition);
            Vector3 offsetChainStartPosition = new Vector3(transform.position.x + chainStartXOffset, transform.position.y + chainStartYOffset, 0);
            chainRenderer.SetPosition(1, offsetChainStartPosition);
        }
        UpdateAnimationStates();

    }

    void FixedUpdate()
    {
        HandlePlayerMovement();
        MoveSoul();
    }

    private void UpdateAnimationStates()
    {
        animator.SetBool("IsRunning", direction != Vector2.zero);
        
        // flip the player sprite based on the direction they are moving left or right
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HandlePlayerMovement()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        body.velocity = direction * walkSpeed;
    }

    private void MoveSoul()
    {
        if (isSoulActive)
        {
            soulTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // Set the soul's target to the mouse position

            Vector2 directionToTarget = soulTarget - (Vector2)transform.position; 
            float distanceToTarget = directionToTarget.magnitude;

            if (distanceToTarget > maxSoulDistance)  // Guard clause to limit the distance the soul can move to prevent jitters
            {
                soulTarget = (Vector2)transform.position + directionToTarget.normalized * maxSoulDistance;  // Limit the distance the soul can move 
            }

            playerSoul.transform.position = Vector2.MoveTowards(playerSoul.transform.position, soulTarget, soulMoveSpeed * Time.deltaTime); // Move the soul towards the target
        }
    }
    
    private IEnumerator ReturnSoulToPlayer()  // Coroutine to return the soul to the player
    { 
        while (Vector2.Distance(playerSoul.transform.position, new Vector2(transform.position.x + soulXOffset, transform.position.y + soulYOffset)) > 0.1f)
        {
            Vector2 playerLocationOffset2 = new Vector2(transform.position.x + soulXOffset, transform.position.y + soulYOffset);  // offset location for the soul to return to
            playerSoul.transform.position = Vector2.MoveTowards(playerSoul.transform.position, playerLocationOffset2, soulMoveSpeed * 3 * Time.deltaTime); // Move the soul towards the player
            yield return null;
        }
        playerSoul.SetActive(false);
        chainRenderer.enabled = false;
    }
}
