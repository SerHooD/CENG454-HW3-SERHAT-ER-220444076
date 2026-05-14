using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 30f;
    [SerializeField] private float damageToCore = 20f;

    private float _currentHealth;
    private IMovementStrategy _movementStrategy;
    private Transform _coreTransform;
    private ObjectPool _pool;
    private static int _killCount = 0;

    public float CurrentHealth => _currentHealth;
    public bool IsDead => _currentHealth <= 0f;

    public void Init(ObjectPool pool)
    {
        _pool = pool;
    }

    public void SetStrategy(IMovementStrategy strategy)
    {
        _movementStrategy = strategy;
    }

    private void OnEnable()
    {
        _currentHealth = maxHealth;

        GameObject core = GameObject.FindWithTag("Core");
        if (core != null)
            _coreTransform = core.transform;
    }

    private void Update()
    {
        if (_coreTransform == null || _movementStrategy == null) return;
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
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Core"))
        {
            other.GetComponent<IDamageable>()?.TakeDamage(damageToCore);
            _killCount++;
            GameEvents.EnemyKilled(_killCount);
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (_pool != null)
            _pool.ReturnToPool(gameObject);
        else
            gameObject.SetActive(false); 
    }
}