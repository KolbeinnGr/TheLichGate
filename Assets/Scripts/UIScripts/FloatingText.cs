using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float fadeDuration = 1f;
    public TextMeshProUGUI textMesh;
    public Vector3 worldPositionOffset = new Vector3(0, 1f, 0); // Default offset

    private float startTime;
    private Vector3 worldPosition;
    private Camera mainCamera;
    private RectTransform canvasRectTransform;

    void Start()
    {
        startTime = Time.time;
        mainCamera = Camera.main;
        if (transform.parent != null)
        {
            canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        // Apply the offset to the world position
        Vector3 adjustedWorldPosition = worldPosition + worldPositionOffset;

        // Update the position
        if (mainCamera && canvasRectTransform)
        {
            Vector2 screenPosition = mainCamera.WorldToScreenPoint(adjustedWorldPosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, mainCamera, out Vector2 localPoint);
            transform.localPosition = localPoint + (Vector2)(Time.deltaTime * moveSpeed * transform.up); // Moving up
        }

        // Fade out over time
        float elapsed = Time.time - startTime;
        if (elapsed < fadeDuration)
        {
            float alpha = Mathf.Clamp01(1 - (elapsed / fadeDuration));
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);
        }
        else
        {
            Destroy(gameObject); // Destroy the object after fade out
        }
    }

    public void SetText(string text, Color color)
    {
        textMesh.text = text;
        textMesh.color = color;
    }

    public void SetWorldPosition(Vector3 position)
    {
        worldPosition = position;
    }
}