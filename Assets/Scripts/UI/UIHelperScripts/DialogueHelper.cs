using UnityEngine;

/// <summary>
/// Custom dialogue implementation for game start as an introduction.
/// </summary>
public class DialogueHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueOne;

    [SerializeField]
    private GameObject dialogueOneText;

    [SerializeField]
    private GameObject dialogueTwo;

    [SerializeField]
    private GameObject DialogueTwoText;

    [SerializeField]
    private GameObject keyBind;

    [SerializeField]
    private GameObject dialogueBackground;

    private float waitTime = 2.5f; // initial time to display message before it disappears
    private float tutorialTime = 8f;

    private void Start()
    {
        // Should not be displayed if player is on a second run after completing the game.
        if(!GameManager.Instance.Player.IsNewGame)
        {
            gameObject.SetActive(false);
        }
        dialogueOne.SetActive(true);
        dialogueBackground.SetActive(true);
    }

    void FixedUpdate()
    {
       // Checking if Shepherd's line has been completely written.
       if (dialogueOneText.GetComponent<TypeWriterDialogue>().TextWritten)
        {
            waitTime -= Time.deltaTime;
            if (waitTime < 0 && dialogueOne.activeSelf)
            {
                dialogueOne.SetActive(false);
                dialogueTwo.SetActive(true);
                waitTime = 4.5f;
            }
        }

        // Checking if Dog's line has been completely written.
        if (DialogueTwoText.GetComponent<TypeWriterDialogue>().TextWritten && dialogueTwo.activeSelf) {

            waitTime -= Time.deltaTime;
            if (waitTime < 0)
            {
                dialogueTwo.SetActive(false);
                keyBind.SetActive(true);
            }        
        }

        // Checking if keybind introduction is active and setting its decay timer.
        if (keyBind.activeSelf)
        {
            tutorialTime -= Time.deltaTime;
            if (tutorialTime < 0)
            {
                keyBind.SetActive(false);
                dialogueBackground.SetActive(false);
            }
        }

    }
}
