using UnityEngine;

public class BaseGun : IWeapon
{
    private ObjectPool _bulletPool;
    public float FireRate => 1f;

    public BaseGun(ObjectPool bulletPool)
    {
        _bulletPool = bulletPool;
    }

    public void Shoot(Transform firePoint)
    {
        GameObject bullet = _bulletPool.Get();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
    }
}