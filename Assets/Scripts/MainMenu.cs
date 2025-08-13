using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public string nextLevel = "LevelOne";
    
    public void LoadLevel() { SceneManager.LoadScene(nextLevel); }
}
