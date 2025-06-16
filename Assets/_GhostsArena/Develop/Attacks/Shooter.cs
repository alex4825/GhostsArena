using UnityEngine;

public class Shooter
{
    private IShootable _shootable;
    private Bullet _bulletPrefab;
    private float _bulletDamage;
    private float _bulletLifeTime;

    private float _shootForceValue;

    public Shooter(IShootable shootable, Bullet bulletPrefab, float bulletDamage, float bulletLifeTime, float shootForceValue)
    {
        _shootable = shootable;
        _bulletPrefab = bulletPrefab;
        _bulletDamage = bulletDamage;
        _bulletLifeTime = bulletLifeTime;
        _shootForceValue = shootForceValue;
    }

    public void Shoot(Vector3 direction)
    {
        Bullet bullet = Object.Instantiate(_bulletPrefab, _shootable.ShootPoint.position, Quaternion.identity);
        bullet.Launch(_shootable, direction * _shootForceValue, _bulletDamage, _bulletLifeTime);
    }
}
