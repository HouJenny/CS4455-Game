using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeleportOnCollision : MonoBehaviour
{
    public Animator anim;                 // Animator for the cat
    public RootMotionControlScript catController; // Custom script to control the cat
    public GameObject player;             // Reference to the player object
    public Image screenFadeImage;         // UI Image used for fading
    public Vector2 teleportRange = new Vector2(10f, 10f); // Range for teleportation

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure it's the cat/player
        {
			
            StartCoroutine(FadeAndTeleport());
        }
    }

    private IEnumerator FadeAndTeleport()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // Disable movement for the cat
        catController.catIdle = true;
        anim.SetBool("isForward", false);

        // Fade to black
        yield return StartCoroutine(FadeScreen(1, 1.0f));

        // Teleport the cat to a random position within the range
        Vector3 newPosition = player.transform.position + new Vector3(
            Random.Range(-teleportRange.x, teleportRange.x),
            0, // Maintain the current y-position
            Random.Range(-teleportRange.y, teleportRange.y)
        );
        player.transform.position = newPosition;

		// Resume the game
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f; // Reset fixedDeltaTime

        // Fade back in
        yield return StartCoroutine(FadeScreen(0, 1.0f));

        // Re-enable movement for the cat
        anim.SetBool("isForward", true);
        catController.catIdle = false;

        
    }

    private IEnumerator FadeScreen(float targetAlpha, float duration)
	{
    	float startAlpha = screenFadeImage.color.a;
	    float elapsedTime = 0f;

    	while (elapsedTime < duration)
    	{
        	elapsedTime += Time.unscaledDeltaTime; // Use unscaled time for the fade
        	float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
        	screenFadeImage.color = new Color(0, 0, 0, alpha);
        	yield return null; // Continue on the next frame, even when the game is paused
    	}

    	screenFadeImage.color = new Color(0, 0, 0, targetAlpha);
	}
}
