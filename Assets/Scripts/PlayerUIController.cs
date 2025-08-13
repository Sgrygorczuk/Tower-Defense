using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Image playerHealth;               // UI Image showing player health (fill amount)
    public TextMeshProUGUI cash;             // UI text displaying current cash amount

    public Button mineButton;                // Button to build a mine
    public Button towerButton;               // Button to build a tower
    
    public GameObject winPanel;
    public GameObject losePanel;
    
    // Updates the health bar fill based on current and max health
    public void UpdatePlayerHealth(float health, float maxHealth)
    {
        playerHealth.fillAmount = health / maxHealth;
    }

    // Updates the cash text display with formatted amount
    public void UpdateCash(int cashAmount)
    {
        cash.text = "$" + cashAmount;
    }

    // Enables or disables buttons based on available cash
    public void UpdateButtons(int cashAmount)
    {
        mineButton.interactable = cashAmount >= 50;
        towerButton.interactable = cashAmount >= 100;
    }

    public void ResetGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetWinLosePanel(bool win) {
        if (win) {
            winPanel.SetActive(true);
        }
        else {
            losePanel.SetActive(true);
        }
    }
}