using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;
    public GameObject panel; // Referință la Panel-ul chatbox-ului
    public string npcName;
    public string[] dialogues;
    private int currentDialogueIndex = 0;

    void Start()
    {
        if (!string.IsNullOrEmpty(npcName))
        {
            npcNameText.text = npcName;
        }

        if (dialogues.Length > 0)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }
        else
        {
            panel.SetActive(false); // Ascunde panelul dacă nu există dialoguri
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Detectează apăsarea tastei Enter
        {
            ShowNextDialogue();
        }
    }

    public void ShowNextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }
        else
        {
            panel.SetActive(false); // Ascunde panelul când toate dialogurile au fost afișate
        }
    }
}
