public interface IWeapon
{
    void Shoot(UnityEngine.Transform firePoint);
    float FireRate { get; }
}