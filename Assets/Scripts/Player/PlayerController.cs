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
    public LineRenderer chainRenderer; 
    public Material chainMaterial;
    
    [Header ("Dash")]
    public float dashDuration = 0.5f;  // Fixed duration of the dash in seconds
    public float minDashDistance = 1f;  // Minimum distance required to initiate a dash
    
    private float dashSpeed;            // Calculated speed based on distance and duration
    private Vector2 dashTarget;         // The target position of the dash
    private Vector2 dashStart;          // Starting position of the player when the dash begins
    private bool isDashing = false;     // Flag to indicate if the player is currently dashing
    private float dashDistanceTravelled = 0f;  // Total distance travelled during the dash
    private float dashStartTime;        // Time when the dash started
    
    
    private Vector2 soulTarget; // The position the soul is moving towards
    private Coroutine returnSoulRoutine; // Coroutine for returning the soul to the player
    private bool isSoulActive = false;


    
    [Header ("Animation")]
    public Animator animator;
    public Vector2 direction;  // The direction the player is moving in


    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        if (chainRenderer != null)
        {
            chainRenderer.material = chainMaterial;
            chainRenderer.positionCount = 2;
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    void Update()
    {
        // if (Time.timeScale == 0) return;  // Pause the game if the timescale is 0 (for menus etc.)
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ToggleSoulActive();
            
        }
        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1)) && isSoulActive && !isDashing)
        {
            StartDash();
        }

        if (chainRenderer && playerSoul.activeSelf)  // Draw the chain between the player and the soul with offsets
        {
            DrawChain();
        }
        
        UpdateAnimationStates();

    }

    void FixedUpdate()
    {   
        if (isDashing)
        {
            PerformDash();
        }
        else
        {
            HandlePlayerMovement();
            MoveSoul();
        }
    }

    private void UpdateAnimationStates()
    {
        bool isMoving = direction != Vector2.zero;
        animator.SetBool("IsRunning", isMoving && !isDashing);

        if (isMoving && !isDashing)
        {
            // transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
            spriteRenderer.flipX = direction.x < 0;
        }
        
    }

    private void DrawChain()
    {
        chainRenderer.enabled = true;
        Vector2 soulPosition = playerSoul.transform.position;
        Vector3 offsetChainEndPosition = new Vector3(soulPosition.x + chainEndXOffset, soulPosition.y + chainEndYOffset, 0);
        chainRenderer.SetPosition(0, offsetChainEndPosition);
        Vector3 offsetChainStartPosition = new Vector3(transform.position.x + chainStartXOffset, transform.position.y + chainStartYOffset, 0);
        chainRenderer.SetPosition(1, offsetChainStartPosition);
    }

    private void HandlePlayerMovement()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        body.velocity = direction * walkSpeed;
    }

    private void ToggleSoulActive()
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
    

    private void MoveSoul()
    {
        if (isSoulActive && !isDashing)
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
    
    private void StartDash()
    {
        float distanceToSoul = Vector2.Distance(transform.position, playerSoul.transform.position);

        if (distanceToSoul < minDashDistance)
        {
            return; // Too close to dash
        }

        isDashing = true;
        dashTarget = playerSoul.transform.position;
        dashStart = transform.position;
        dashDistanceTravelled = 0f;
        dashSpeed = distanceToSoul / dashDuration;
        dashStartTime = Time.time;  // Initialize dash start time

        // Determine the direction to the soul and face the player in that direction
        float directionToSoul = Mathf.Sign(dashTarget.x - transform.position.x);
        spriteRenderer.flipX = directionToSoul < 0;

        animator.SetBool("IsDashing", true);
    }
 

    private void PerformDash()
    {
        Vector2 directionToSoul = (dashTarget - body.position).normalized;
        body.velocity = directionToSoul * dashSpeed;

        if (Vector2.Distance(dashStart, body.position) >= Vector2.Distance(dashStart, dashTarget) || 
            Time.time - dashStartTime >= dashDuration)
        {
            FinishDash();
        }

        dashDistanceTravelled += Time.fixedDeltaTime * dashSpeed;
    }


    private void FinishDash()
    {
        isDashing = false;
        body.velocity = Vector2.zero;
        animator.SetBool("IsDashing", false);
        playerSoul.SetActive(false);
        chainRenderer.enabled = false;
        ToggleSoulActive();
        // Use dashDistanceTravelled for damage calculations if needed
    }

    public void TriggerDeathState()
    {
        StartCoroutine(Wait());
        animator.SetBool("IsDead", true);
        walkSpeed = 0;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        GameManager.Instance.endScreen.SetActive(true);
        GameManager.Instance.PauseGame();
    }

}
