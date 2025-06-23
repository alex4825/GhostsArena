using System;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamagable, IKillable, IMovable, IJumpable
{
    private float _rotationSpeed;
    private float _health;
    private bool _isInDesroyProcess;

    private DirectionalRotator _rotator;

    public event Action<IKillable> Dead;
    public event Action<IKillable> KilledBySomeone;
    public event Action Hit;

    public Races Race { get; private set; }
    public bool IsAlive => Health > 0;
    public bool IsDead => Health <= 0;
    public float Lifetime { get; private set; } = 0;
    public float DeadDuration { get; private set; }
    public float ShowDuration { get; private set; }
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
        _rotationSpeed = config.RotationSpeed;
        ShowDuration = config.ShowDuration;
        DeadDuration = config.DeadDuration;
        MaxHealth = config.MaxHealth;
        Health = MaxHealth;

        _rotator = new DirectionalRotator(transform, _rotationSpeed);

        foreach (var initializable in GetComponentsInChildren<IInitializable>())
            initializable.Initialize();
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

        if (IsDead)
            KilledBySomeone?.Invoke(this);
    }

    public virtual void Kill()
    {
        if (_isInDesroyProcess == false)
        {
            Dead?.Invoke(this);
            Destroy(gameObject, DeadDuration);
            _isInDesroyProcess = true;
        }
    }
}
