using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
    }
}
