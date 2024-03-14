using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public float walkSpeed = 5f;
    public float soulMoveSpeed = 3f;
    public GameObject playerSoul;
    public float maxSoulDistance = 5f; // Maximum distance the soul can move from the player

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
            chainRenderer.SetPosition(0, playerSoul.transform.position);
            chainRenderer.SetPosition(1, transform.position);
            
        }
    }

    void FixedUpdate()
    {
        HandlePlayerMovement();
        UpdateSoulTargetPosition();
        MoveSoul();
    }


    private void HandlePlayerMovement()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        body.velocity = direction * walkSpeed;
    }

    private void MoveSoul()
    {
        if (isSoulActive){
            if (Vector2.Distance(playerSoul.transform.position, body.position) > maxSoulDistance)
            {
                Vector2 directionToTarget = soulTarget - (Vector2)transform.position;
                soulTarget = (Vector2)transform.position + directionToTarget.normalized * maxSoulDistance;
            }

            playerSoul.transform.position = Vector2.MoveTowards(playerSoul.transform.position, soulTarget, soulMoveSpeed * Time.deltaTime);
        }
    }
    


    private void UpdateSoulTargetPosition()
    {
        soulTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
