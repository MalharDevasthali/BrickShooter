using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIService : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameOverPanel;
    public GameObject GameWinPanel;
    public static UIService instace;
    private void Awake()
    {
        if (instace != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instace = this;
        }
    }

    public void EnableGameOverPanel()
    {
        Debug.Log("GameOver");
        BallLauncher.instance.enabled = false;
        GameOverPanel.SetActive(true);
    }
    public void RestartTheGame()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        BallLauncher.instance.enabled = false;
    }
    public void EnableGameWinPanel()
    {
        GameWinPanel.SetActive(true);
    }
    public void DropAllBalls()
    {
        BallsPool.instace.DropAllBalls();
        BrickDropper.instace.DisableBrickColiders();

    }
}
