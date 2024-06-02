using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static string gameScene = "Game";
    [SerializeField] private bool toGame = false;
    [SerializeField] private bool toQuit = false;
    private void LoadGame() {
        SceneManager.LoadScene(gameScene);
    }

    private void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon")) {
            if(toGame) LoadGame();
            if(toQuit) QuitGame();
        }
    }
}