using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 30f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float damageToCore = 10f;
    [SerializeField] private bool useFlanking = false;

    private float _currentHealth;
    private IMovementStrategy _movementStrategy;
    private Transform _coreTransform;
    private static int _killCount = 0;

    public float CurrentHealth => _currentHealth;
    public bool IsDead => _currentHealth <= 0f;

    private void OnEnable()
    {
        _currentHealth = maxHealth;

        if (useFlanking)
            _movementStrategy = new FlankMoveStrategy(moveSpeed);
        else
            _movementStrategy = new DirectMoveStrategy(moveSpeed);

        GameObject core = GameObject.FindWithTag("Core");
        if (core != null)
            _coreTransform = core.transform;
    }

    private void Update()
    {
        if (_coreTransform == null) return;
        _movementStrategy.Move(transform, _coreTransform);
    }

    public void TakeDamage(float amount)
    {
        if (IsDead) return;

        _currentHealth -= amount;

        if (IsDead)
        {
            _killCount++;
            GameEvents.EnemyKilled(_killCount);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Core"))
        {
            other.GetComponent<IDamageable>()?.TakeDamage(damageToCore);
            gameObject.SetActive(false);
        }
    }
}