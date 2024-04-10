using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Text drawing helper to mimic a typing animation for text
/// </summary>
public class TypeWriterDialogue : MonoBehaviour
{
    public float characterDelay = 0.1f;
    public string dialogueText;
    private string currentText = "";

    private bool textWritten;
    public bool TextWritten
    {
        get => textWritten;
        set => textWritten = value;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(ShowText());
    }

    /// <summary>
    /// Iterating through every character in a text and updating it after every tick.
    /// </summary>
    IEnumerator ShowText()
    {
        for(int i = 0; i < dialogueText.Length; i++)
        {
            currentText = dialogueText.Substring(0, i);
            this.GetComponent<TextMeshProUGUI>().text = currentText;
            yield return new WaitForSeconds(characterDelay);
        }

        textWritten = true;
    }
}
