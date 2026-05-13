using UnityEngine;

public class RapidFireDecorator : IWeapon
{
    private IWeapon _wrapped;
    public float FireRate => _wrapped.FireRate * 3f;

    public RapidFireDecorator(IWeapon weapon)
    {
        _wrapped = weapon;
    }

    public void Shoot(Transform firePoint)
    {
        _wrapped.Shoot(firePoint);
    }
}