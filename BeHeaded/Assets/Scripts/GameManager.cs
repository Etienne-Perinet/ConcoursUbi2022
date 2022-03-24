using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;

    public void EndGame()
    {
        if(!gameEnded)
        {
            gameEnded = !gameEnded;
            Restart();
        }
    }

    void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

}
