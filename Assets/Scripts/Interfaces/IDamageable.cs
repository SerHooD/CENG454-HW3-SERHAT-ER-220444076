public interface IDamageable
{
    void TakeDamage(float amount);
    float CurrentHealth { get; }
    bool IsDead { get; }
}