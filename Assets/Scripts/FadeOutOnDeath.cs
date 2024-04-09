using UnityEngine;
using System.Collections;

public class FadeOutOnDeath : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Assign this in the inspector or find it via GetComponent.
    public Color fadeToColor = new Color(1f, 1f, 1f, 0f); // The color to fade to (transparent white).
    public float fadeDuration = 0.5f; // How long the fade should last.

    void Start()
    {
        // If spriteRenderer is not assigned, try to find it on the same GameObject.
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void FadeOut()
    {
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            // Increment timer by the time passed since last frame
            timer += Time.deltaTime;
        
            // Wait until the next frame
            yield return null;
        }
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        Color originalColor = spriteRenderer.color; // Remember the original color.
        float timer = 0;

        while (timer < fadeDuration)
        {
            // Increment timer by the time passed since last frame
            timer += Time.deltaTime;

            // Calculate the fraction of the total duration that has passed
            float progress = timer / fadeDuration;

            // Lerp the color from the original to the fadeToColor over time
            spriteRenderer.color = Color.Lerp(originalColor, fadeToColor, progress);

            // Wait until the next frame
            yield return null;
        }

        // Ensure the color is set to the final fadeToColor after the loop
        spriteRenderer.color = fadeToColor;
        Destroy(gameObject);
    }
}
