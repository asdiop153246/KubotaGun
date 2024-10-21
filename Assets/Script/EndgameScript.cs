using UnityEngine;
using UnityEngine.SceneManagement;
public class EndgameScript : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
