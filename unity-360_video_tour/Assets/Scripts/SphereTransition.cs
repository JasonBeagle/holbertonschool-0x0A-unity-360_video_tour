using UnityEngine;
using UnityEngine.Video;

public class SphereTransition : MonoBehaviour
{
    public GameObject currentSphere; // Reference to the currently active sphere
    public GameObject targetSphere; // Reference to the sphere to transition to
    public WorldSpaceFader fader; // Reference to the WorldSpaceFader script

    public void OnButtonClicked()
    {
        Debug.Log("Button was clicked");
        // Disable the current sphere and enable the target sphere
        fader.StartFade(currentSphere, targetSphere);
        // currentSphere.SetActive(false);
        // targetSphere.SetActive(true);
        
        // Optionally: Start playing the video on the target sphere
        // VideoPlayer videoPlayer = targetSphere.GetComponent<VideoPlayer>();
        // if(videoPlayer != null) videoPlayer.Play();
    }
}
