using UnityEngine;

[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(Collider))]
public class CharacterControllerCharacter : Character, IDirectionalMovable, IShootable
{
    private const string BulletSpawnPointName = "BulletSpawnPoint";

    private CharacterControllerMover _mover;
    private CharacterControllerJumper _jumper;
    private Shooter _shooter;

    private Vector3 _currentVelocity;

    public override Vector3 CurrentVelocity => _currentVelocity;
    public override bool InJumpProcess => _jumper.InProcess;
    public Transform ShootPoint => transform.Find(BulletSpawnPointName);
    public Collider SelfCollider => GetComponent<Collider>();

    public void Initialize(
        MainHeroConfig config,
        CharacterControllerMover mover,
        CharacterControllerJumper jumper, 
        Shooter shooter)
    {
        base.Initialize(config);

        _mover = mover;
        _jumper = jumper;
        _shooter = shooter;
    }

    public void SetMovement(Vector3 direction)
    {
        _currentVelocity = direction * RunSpeed;
        _mover.Move(CurrentVelocity);
    }

    public void Jump() => _jumper.Jump();

    public void Shoot() => _shooter.Shoot(transform.forward);
}
