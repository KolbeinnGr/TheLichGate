using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public float walkSpeed = 5f;
    public float soulMoveSpeed = 3f;
    public GameObject playerSoul;
    public float maxSoulDistance = 5f; // Maximum distance the soul can move from the player
    public float chainStartXOffset = 0f; 
    public float chainStartYOffset = 0f;
    public float chainEndXOffset = 0f;
    public float chainEndYOffset = 0f;

    public Vector2 direction;
    

    private Vector2 soulTarget; // The position the soul is moving towards
    private Coroutine returnSoulRoutine; 
    private bool isSoulActive = false;

    // Optional: Line Renderer for visual representation
    public LineRenderer chainRenderer;
    public Material chainMaterial;
    

    void Start()
    {
        // Optional: Initialize the Line Renderer
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
                playerSoul.transform.position = transform.position; // Spawn at player's position
            }
            
            if (!isSoulActive)
            {
                if (returnSoulRoutine != null)
                {
                    StopCoroutine(returnSoulRoutine);
                }
                returnSoulRoutine = StartCoroutine(ReturnSoulToPlayer());
            }
        }

        // Optional: Update the line renderer positions
        if (chainRenderer != null && playerSoul.activeSelf)
        {
            chainRenderer.enabled = true;
            Vector3 offsetChainEndPosition = new Vector3(playerSoul.transform.position.x + chainEndXOffset, playerSoul.transform.position.y + chainEndYOffset, playerSoul.transform.position.y);
            chainRenderer.SetPosition(0, offsetChainEndPosition);
            Vector3 offsetChainStartPosition = new Vector3(transform.position.x + chainStartXOffset, transform.position.y + chainStartYOffset, transform.position.y);
            chainRenderer.SetPosition(1, offsetChainStartPosition);
        }
    }

    void FixedUpdate()
    {
        HandlePlayerMovement();
        MoveSoul();
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
            soulTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 directionToTarget = soulTarget - (Vector2)transform.position;
            float distanceToTarget = directionToTarget.magnitude;

            if (distanceToTarget > maxSoulDistance)
            {
                soulTarget = (Vector2)transform.position + directionToTarget.normalized * maxSoulDistance;
            }

            playerSoul.transform.position = Vector2.MoveTowards(playerSoul.transform.position, soulTarget, soulMoveSpeed * Time.deltaTime);
        }
    }
    
    private IEnumerator ReturnSoulToPlayer()
    {
        while (Vector2.Distance(playerSoul.transform.position, transform.position) > 0.1f)
        {
            playerSoul.transform.position = Vector2.MoveTowards(playerSoul.transform.position, transform.position, soulMoveSpeed * 3 * Time.deltaTime);
            yield return null;
        }
        playerSoul.SetActive(false);
        chainRenderer.enabled = false;

    }
}
