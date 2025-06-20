using UnityEngine;

public class CharacterView : MonoBehaviour, IInitializable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private float _damageEffectDuration;

    private readonly int SpeedKey = Animator.StringToHash("Speed");
    private readonly int HitKey = Animator.StringToHash("Hit");
    private readonly int DieKey = Animator.StringToHash("Die");

    private const string DissolveKey = "_DissolveAdge";
    private const string DamageStranghtKey = "_DamageStranght";

    private Character _character;
    private ShortEffectView _shortEffectView;

    private bool _isInit;

    private float CharacterMaxSpeed => _character.RunSpeed;

    public void Initialize()
    {
        _character = GetComponentInParent<Character>();

        _shortEffectView = new ShortEffectView(GetComponentsInChildren<Renderer>(), this);
        _healthBar.Initialize(_character);
        _shortEffectView.PlayDecreaseEffect(DissolveKey, _character.ShowDuration);

        _character.Hit += OnCharacterHit;
        _character.Dead += OnCharacterDead;

        _isInit = true;
    }

    private void Update()
    {
        if (_isInit)
            return;

        _animator.SetFloat(SpeedKey, _character.CurrentVelocity.magnitude / CharacterMaxSpeed);
    }

    private void OnDestroy()
    {
        _character.Hit -= OnCharacterHit;
        _character.Dead -= OnCharacterDead;
    }

    private void OnCharacterDead(IKillable killable, float deadDuration)
    {
        _animator.SetTrigger(DieKey);
        _shortEffectView.PlayIncreaseEffect(DissolveKey, deadDuration);
        _healthBar.gameObject.SetActive(false);
    }

    private void OnCharacterHit()
    {
        if (_character.IsAlive)
            _animator.SetTrigger(HitKey);

        _shortEffectView.PlayIncreaseDecreaseEffect(DamageStranghtKey, _damageEffectDuration);
    }
}
