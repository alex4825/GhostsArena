using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private IShootable _shootable;
    private float _damage;

    public void Launch(IShootable shootable, Vector3 force, float damage, float lifeTime)
    {
        _shootable = shootable;
        _damage = damage;

        GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagable damagable) && damagable != _shootable)
        {
            damagable.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
