using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private float _damageEffectDuration;

    private readonly int SpeedKey = Animator.StringToHash("Speed");
    private readonly int HitKey = Animator.StringToHash("Hit");
    private readonly int InJumpProcessKey = Animator.StringToHash("InJumpProcess");
    private readonly int DieKey = Animator.StringToHash("Die");
    private readonly int AliveKey = Animator.StringToHash("Alive");

    private const string DamageStranghtKey = "_DamageStranght";
    private const string DissolveAdgeKey = "_DissolveAdge";

    private ShortEffectView _shortEffectView;

    private float _characterMaxSpeed => _character.RunSpeed;

    private void Awake()
    {
        _shortEffectView = new ShortEffectView(GetComponentsInChildren<Renderer>(), this);

        _character.Hit += OnCharacterHit;
        _character.Dead += OnCharacterDead;
    }

    private void Update()
    {
        _animator.SetFloat(SpeedKey, _character.CurrentVelocity.magnitude / _characterMaxSpeed);

        //_animator.SetBool(InJumpProcessKey, _character.InJumpProcess);
    }

    private void OnDestroy()
    {
        _character.Hit -= OnCharacterHit;
        _character.Dead -= OnCharacterDead;
    }

    private void OnCharacterDead(IKillable killable, float deadDuration)
    {
        _animator.SetTrigger(DieKey);
        _shortEffectView.PlayIncreaseEffect(DissolveAdgeKey, deadDuration);
    }

    private void OnCharacterHit()
    {
        if (_character.IsAlive)
            _animator.SetTrigger(HitKey);

        //_shortEffectView.PlayIncreaseDecreaseEffect(DamageStranghtKey, _damageEffectDuration);
    }
}
