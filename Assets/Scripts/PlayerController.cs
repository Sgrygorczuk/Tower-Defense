using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Current player health 
    public float playerHealth = 100f;
    // Maximum player health
    private const float PlayerHealthMax = 100f;

    // Reference to the PlayerUIController to update UI elements
    private PlayerUIController _playerUI;
    private GameObject _mineParent; 
    
    public int playerCash = 150;
    private float _timer = 0f;                 // Tracks elapsed time
    public float interval = 10f;               // Time interval between cash payouts
    
    // Called before the first frame update
    private void Start() {
        // Find the PlayerUIController component on the Canvas GameObject
        if(GameObject.Find("Canvas")) {
            // Initialize UI with current health and cash values
            _playerUI = GameObject.Find("Canvas").GetComponent<PlayerUIController>();
            _playerUI.UpdatePlayerHealth(playerHealth, PlayerHealthMax);
            _playerUI.UpdateCash(playerCash);
        }

        if (GameObject.Find("Mines")) {
            _mineParent = GameObject.Find("Mines");
        }
    }
    
    void Update() {
        _timer += Time.deltaTime;

        // When timer reaches interval, reset timer and give player cash
        if (_timer >= interval) {
            _timer = 0f;
            if (_mineParent) {
                CashChange(10 * _mineParent.transform.childCount);   
            }
        }
    }

    // Returns the current player cash
    public int GetCash() { return playerCash; }

    // Changes the player's cash by a specified amount and updates the UI
    public void CashChange(int amount) {
        playerCash += amount;
        if (playerCash >= 500)
        {
            _playerUI.SetWinLosePanel(true);
        }
        // Update cash display and button states based on current cash
        _playerUI.UpdateCash(playerCash);
        _playerUI.UpdateButtons(playerCash);
    }

    // Called when the player collides with another collider
    private void OnCollisionEnter(Collision other) {
        // Check if collided object is tagged "Enemy"
        if (other.gameObject.CompareTag("Enemy")) {
            // Decrease health and update UI
            playerHealth -= 10;
            if(_playerUI){ _playerUI.UpdatePlayerHealth(playerHealth, PlayerHealthMax);}
            if (playerHealth <= 0) { if(_playerUI){_playerUI.SetWinLosePanel(false);} }
            // Destroy the enemy object on collision
            Destroy(other.gameObject);
        }
    }
}