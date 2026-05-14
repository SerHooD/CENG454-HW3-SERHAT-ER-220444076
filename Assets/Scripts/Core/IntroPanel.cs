using UnityEngine;
using TMPro;

public class IntroPanel : MonoBehaviour
{
    [SerializeField] private GameObject introPanel;

    private void Start()
    {
        Time.timeScale = 0f;
        introPanel.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        introPanel.SetActive(false);
    }
}