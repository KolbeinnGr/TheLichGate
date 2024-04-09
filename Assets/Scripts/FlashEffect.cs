using UnityEngine;
using System.Collections;

public class FlashEffect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Assign this in the inspector or find it via GetComponent.
    public Color flashColor = Color.white; // The color to flash.
    public float flashDuration = 0.1f; // How long the flash should last.

    void Start()
    {
        // If spriteRenderer is not assigned, try to find it on the same GameObject.
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        Color originalColor = spriteRenderer.color; // Remember the original color.
        spriteRenderer.color = flashColor; // Change to the flash color.
        yield return new WaitForSeconds(flashDuration); // Wait for the duration of the flash.
        spriteRenderer.color = originalColor; // Change the color back to the original color.
    }
}
