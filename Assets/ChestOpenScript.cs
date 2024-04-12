using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpenScript : MonoBehaviour
{
    public AudioClip ChestOpenSound;
    [SerializeField] private Animator animator;
    private bool HasBeenCollected = false;

    public void Collect()
    {
        if (HasBeenCollected)
        {
            return;
        }
        HasBeenCollected = true;
        
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.UpgradeStat(PlayerStats.UpgradeType.increaseLevel);
        animator.Play("ChestOpen");
        if (ChestOpenSound)
        {
            AudioManager.Instance.PlaySound(ChestOpenSound, 0.15f);
        }
        
        // Start the coroutine to wait and then destroy the GameObject
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        // Wait for one second
        yield return new WaitForSeconds(1);

        // Destroy this GameObject
        Destroy(gameObject);
    }
}
