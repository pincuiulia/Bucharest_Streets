using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildingEntrance : MonoBehaviour
{
    public string shoppingSceneName;
    public KeyCode interactKey = KeyCode.E; // Tasta pentru a interactiona
    public GameObject messageCanvas; // Canvasul sau panoul care să apară

    private bool playerInRange = false;

    void Start()
    {
        // Asigurăm că Canvas-ul este inactiv la început
        if (messageCanvas != null)
        {
            messageCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Verificăm dacă jucătorul este în raza de interacțiune și apasă tasta dorită
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            // Încărcăm scena ShoppingInterior
            SceneManager.LoadScene(shoppingSceneName);
        }
    }

    // Detectăm când jucătorul intră în raza de interacțiune
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // Afișăm mesajul de intrare
            if (messageCanvas != null)
            {
                messageCanvas.SetActive(true);
            }
        }
    }

    // Detectăm când jucătorul iese din raza de interacțiune
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            // Ascundem mesajul
            if (messageCanvas != null)
            {
                messageCanvas.SetActive(false);
            }
        }
    }
}

// Script realizat cu ajutorul ChatGPT