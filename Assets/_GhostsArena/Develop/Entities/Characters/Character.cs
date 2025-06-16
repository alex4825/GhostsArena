using System;
using System.Diagnostics;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamagable, IKillable, IMovable, IJumpable
{
    protected float RotationSpeed;

    private float _deadDuration;
    private float _health;
    private bool _isInDesroyProcess;

    private DirectionalRotator _rotator;

    public event Action<IKillable, float> Dead;
    public event Action Hit;

    public Races Race { get; private set; }
    public bool IsAlive => Health > 0;
    public bool IsDead => Health <= 0;
    public float Lifetime { get; private set; } = 0;
    public float MaxHealth { get; private set; }
    public float Health
    {
        get { return _health; }

        private set { _health = value >= 0 ? value : 0; }
    }
    public float WalkingSpeed { get; private set; }
    public float RunSpeed { get; private set; }
    public float MeleeAttack { get; private set; }
    public Vector3 Position => transform.position;
    public abstract Vector3 CurrentVelocity { get; }
    public abstract bool InJumpProcess { get; }

    protected void Initialize(CharacterConfig config)
    {
        Race = config.Race;
        WalkingSpeed = config.WalkingSpeed;
        RunSpeed = config.RunSpeed;
        MeleeAttack = config.MeleeAttack;
        RotationSpeed = config.RotationSpeed;
        _deadDuration = config.DeadDuration;
        MaxHealth = config.MaxHealth;
        Health = MaxHealth;

        _rotator = new DirectionalRotator(transform, RotationSpeed);
    }

    private void Update()
    {
        if (IsAlive)
        {
            _rotator.Update();
            Lifetime += Time.deltaTime;
        }

        if (IsDead && _isInDesroyProcess == false)
            Kill();
    }

    public void SetRotation(Vector3 direction) => _rotator.SetDirection(direction);

    public void TakeDamage(float damage)
    {
        if (IsDead)
            return;

        Hit?.Invoke();

        if (damage > 0)
            Health -= damage;
    }

    public virtual void Kill()
    {
        Dead?.Invoke(this, _deadDuration);
        Destroy(gameObject, _deadDuration);
        _isInDesroyProcess = true;
    }
}
