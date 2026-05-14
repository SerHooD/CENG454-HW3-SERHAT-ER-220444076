using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifetime = 3f;

    private ObjectPool _pool;

    public void Init(ObjectPool pool)
    {
        _pool = pool;
    }

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), lifetime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(damage);
        }
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (_pool != null)
            _pool.ReturnToPool(gameObject);
        else
            gameObject.SetActive(false);
    }
}