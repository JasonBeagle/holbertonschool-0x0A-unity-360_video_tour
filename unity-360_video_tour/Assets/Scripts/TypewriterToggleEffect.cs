using UnityEngine;
using TMPro; // Include the TextMeshPro namespace
using System.Collections;
using UnityEngine.UI;

public class TypewriterToggleEffect : MonoBehaviour
{
    public TextMeshProUGUI textBox; // Reference to the TextMeshProUGUI component
    public string fullText; // The full text to display
    public float delay = 0.1f; // Delay between characters
    
    private bool isTyping = false;
    private bool cancelTyping = false;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe the Toggle method to the Button's onClick event
        GetComponent<Button>().onClick.AddListener(Toggle);
        
        // Initially set the text box to inactive
        textBox.gameObject.SetActive(false);
    }

    public void Toggle()
    {
        // If currently typing, cancel typing
        if (isTyping)
        {
            cancelTyping = true;
        }
        else
        {
            // If text box is active, deactivate it. If it's inactive, start typing.
            if (textBox.gameObject.activeInHierarchy)
            {
                textBox.gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(TypeText());
            }
        }
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        cancelTyping = false;
        textBox.gameObject.SetActive(true);
        textBox.text = "";
        
        // Type out the text letter by letter, unless typing is cancelled
        foreach (char letter in fullText.ToCharArray())
        {
            if (cancelTyping)
                break;
            
            textBox.text += letter;
            yield return new WaitForSeconds(delay);
        }
        
        // Display the full text immediately if typing was cancelled
        if (cancelTyping)
            textBox.text = fullText;

        isTyping = false;
    }
}