using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static string titleScene = "Title";
    private static string gameScene = "Game";
    [SerializeField] private bool toTitle = false;
    [SerializeField] private bool toGame = false;
    [SerializeField] private bool toQuit = false;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon")) {
            if (toTitle) SceneManager.LoadScene(titleScene);
            if (toGame) SceneManager.LoadScene(gameScene);
            if (toQuit) 
            {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
            }
        }
    }
}