using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeleportOnCollision : MonoBehaviour
{
	public Animator anim;
	public RootMotionControlScript catController;
    public GameObject player;             // Reference to the player object
    public Image screenFadeImage;         // Reference to the UI Image used for fading
    public Vector2 teleportRange = new Vector2(10f, 10f); // Range for teleportation

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeAndTeleport());
        }
    }

    private IEnumerator FadeAndTeleport()
    {
        // Fade to black
		catController.catIdle = true;
		anim.SetBool("isForward", false);
        yield return StartCoroutine(FadeScreen(1, 1.0f));

        // Teleport player to a random location within range
        Vector3 newPosition = player.transform.position + new Vector3(
            Random.Range(-teleportRange.x, teleportRange.x),
            0, // Keep y unchanged if you only want to move on x and z
            Random.Range(-teleportRange.y, teleportRange.y)
        );
        player.transform.position = newPosition;

        // Fade back in
        yield return StartCoroutine(FadeScreen(0, 1.0f));
		anim.SetBool("isForward", true);
		catController.catIdle = false;
    }

    private IEnumerator FadeScreen(float targetAlpha, float duration)
    {
        float startAlpha = screenFadeImage.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            screenFadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        screenFadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}
