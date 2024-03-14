using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public float walkSpeed = 5f;
    public float soulMoveSpeed = 3f;
    public GameObject playerSoul;
    public float maxSoulDistance = 5f; // Maximum distance the soul can move from the player

    private Vector2 soulTarget;
    private Coroutine returnSoulRoutine;

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
        HandleSoulActivation();

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
    }

    private void HandleSoulActivation()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerSoul.SetActive(true);
            playerSoul.transform.position = transform.position; // Spawn at player's position
            UpdateSoulTargetPosition();
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            UpdateSoulTargetPosition();
            MoveSoul();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (returnSoulRoutine != null)
            {
                StopCoroutine(returnSoulRoutine);
            }
            returnSoulRoutine = StartCoroutine(ReturnSoulToPlayer());
        }
    }

    private void HandlePlayerMovement()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        body.velocity = direction * walkSpeed;
    }

    private void MoveSoul()
    {
        if (Vector2.Distance(playerSoul.transform.position, soulTarget) > maxSoulDistance)
        {
            Vector2 directionToTarget = soulTarget - (Vector2)transform.position;
            soulTarget = (Vector2)transform.position + directionToTarget.normalized * maxSoulDistance;
        }

        playerSoul.transform.position = Vector2.MoveTowards(playerSoul.transform.position, soulTarget, soulMoveSpeed * Time.deltaTime);
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
