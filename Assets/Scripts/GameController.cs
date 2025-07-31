using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
    Rigidbody2D playerRb;
    [SerializeField] private float fallThreshold = -5000f; // Y position threshold for falling death

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        checkpointPos = transform.position; // Set initial checkpoint to starting position
    }

    private void Update()
    {
        // Check if the player's Y position is below the fall threshold
        if (transform.position.y < fallThreshold)
        {
            Die(); // Trigger death and respawn
        }
    }

    public void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos; // Update checkpoint position
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        playerRb.velocity = new Vector2(0, 0);
        transform.localScale = new Vector3(0, 0, 0); // Hide ghost during respawn
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos; // Move to last checkpoint
        transform.localScale = new Vector3(1, 1, 1); // Restore ghost
        playerRb.simulated = true;

        // Reset health to max after respawn
        HealthController healthController = GetComponent<HealthController>();
        if (healthController != null)
        {
            // Call a method to reset health (requires GetCurrentHealth and modified TakeDamage)
            healthController.TakeDamage(-healthController.GetCurrentHealth());
        }
    }
}