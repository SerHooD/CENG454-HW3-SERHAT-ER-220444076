using UnityEngine;

public class CoreHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    public float CurrentHealth => _currentHealth;
    public bool IsDead => _currentHealth <= 0f;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (IsDead) return;

        _currentHealth -= amount;
        _currentHealth = Mathf.Max(_currentHealth, 0f);

        GameEvents.CoreDamaged(_currentHealth);

        if (IsDead)
            GameEvents.CoreDestroyed();
    }
}