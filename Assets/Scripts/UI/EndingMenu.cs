using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenu : MonoBehaviour
{
    public void PlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        SceneManager.LoadScene(nextSceneIndex);
    }
}
