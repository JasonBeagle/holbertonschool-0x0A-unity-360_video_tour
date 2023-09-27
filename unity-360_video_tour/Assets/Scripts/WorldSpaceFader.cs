using UnityEngine;
using System.Collections;
using UnityEngine.Video; // Add this line to use VideoPlayer

public class WorldSpaceFader : MonoBehaviour
{
    public Renderer fadeSphereRenderer;
    public float fadeSpeed = 1f;

    private bool isFading = false;

    // Set the initial alpha to 0
    void Start()
    {
        SetAlpha(0f);
    }
    
    public void StartFade(GameObject currentSphere, GameObject targetSphere)
    {
        if (!isFading)
        {
            StartCoroutine(FadeInOut(currentSphere, targetSphere));
        }
    }

    IEnumerator FadeInOut(GameObject currentSphere, GameObject targetSphere)
    {
        isFading = true;

        // Fade to black
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }

        // Perform the transition here (e.g., enable/disable spheres)
        currentSphere.SetActive(false);
        targetSphere.SetActive(true);
        Debug.Log("Target Sphere Active: " + targetSphere.activeSelf); // Log the active state of targetSphere

        // Optionally: Prepare and wait for the video on the target sphere
        VideoPlayer videoPlayer = targetSphere.GetComponent<VideoPlayer>();
        if(videoPlayer != null) 
        {
            videoPlayer.Prepare(); // Prepare the video for playback

            // Wait until the video is prepared
            while (!videoPlayer.isPrepared)
            {
                yield return null; // Wait for the next frame
            }

            videoPlayer.Play(); // Start playing the video
        }


        // Fade to clear
        alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }

        isFading = false;
    }

    private void SetAlpha(float alpha)
    {
        Color color = fadeSphereRenderer.material.color;
        color.a = alpha;
        fadeSphereRenderer.material.color = color;
    }
}