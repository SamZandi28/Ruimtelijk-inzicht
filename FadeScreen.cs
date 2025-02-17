using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    public Image fadeImage;            // Reference to the black fade image in the UI Canvas
    public float fadeDuration = 1.5f;  // Duration of the fade effect
    public Animator doorAnimator;      // Reference to the door animation controller
    public AudioSource doorSound;

    [SerializeField] private BoxCollider doorCollider;  // Reference to the door's BoxCollider
    [SerializeField] private RoomTrigger roomTrigger;   // Reference to RoomTrigger

    private bool isTransitioning = false;

    private void Start()
    {
        // Ensure the door has its collider enabled at the start
        if (doorCollider != null)
        {
            doorCollider.enabled = true;
        }
        else
        {
            Debug.LogWarning("Door BoxCollider is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Trigger fade and door animation only if the player collides with the door and shaderObjectDeactivated is true
        if (other.CompareTag("Player") && !isTransitioning && roomTrigger != null && roomTrigger.shaderObjectDeactivated)
        {
            isTransitioning = true;
            StartCoroutine(PlayDoorAndFadeOut());
        }
    }

    public IEnumerator PlayDoorAndFadeOut()
    {
        /*// Start the door animation
        doorAnimator.SetTrigger("isOpening");

        // Play the door sound if available
        if (doorSound != null)
        {
            doorSound.Play();
        }

        // Disable the door collider after the animation starts
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }*/

        // Wait for a moment to sync with the door animation timing (adjust as needed)
        yield return new WaitForSeconds(1.0f);

        // Fade to black
        yield return StartCoroutine(FadeToBlack());
    }

    public IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color fadeColor = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = fadeColor;
            yield return null;
        }
    }
}
