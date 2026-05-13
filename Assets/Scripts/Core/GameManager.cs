using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private void OnEnable()
    {
        GameEvents.OnCoreDestroyed += HandleLose;
        GameEvents.OnGameWon += HandleWin;
    }

    private void OnDisable()
    {
        GameEvents.OnCoreDestroyed -= HandleLose;
        GameEvents.OnGameWon -= HandleWin;
    }

    private void HandleWin()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void HandleLose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}