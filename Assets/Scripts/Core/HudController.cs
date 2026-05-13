using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Slider coreHealthBar;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI waveText;

    private int _waveNumber = 0;

    private void OnEnable()
    {
        GameEvents.OnCoreDamaged += UpdateHealthBar;
        GameEvents.OnEnemyKilled += UpdateKillCount;
        GameEvents.OnWaveCompleted += UpdateWave;
    }

    private void OnDisable()
    {
        GameEvents.OnCoreDamaged -= UpdateHealthBar;
        GameEvents.OnEnemyKilled -= UpdateKillCount;
        GameEvents.OnWaveCompleted -= UpdateWave;
    }

    private void UpdateHealthBar(float currentHealth)
    {
        if (coreHealthBar != null)
            coreHealthBar.value = currentHealth;
    }

    private void UpdateKillCount(int kills)
    {
        if (killCountText != null)
            killCountText.text = "Kills: " + kills;
    }

    private void UpdateWave()
    {
        _waveNumber++;
        if (waveText != null)
            waveText.text = "Wave: " + _waveNumber;
    }
}