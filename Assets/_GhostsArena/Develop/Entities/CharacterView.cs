using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private float _damageEffectDuration;

    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int HitKey = Animator.StringToHash("Hit");
    private readonly int InJumpProcessKey = Animator.StringToHash("InJumpProcess");
    private readonly int DieKey = Animator.StringToHash("Die");
    private readonly int AliveKey = Animator.StringToHash("Alive");

    private const string InjuryLayerName = "Injury Layer";
    private const float MaxInjuryWeight = 1f;

    private const string DamageStranghtKey = "_DamageStranght";
    private const string DissolveAdgeKey = "_DissolveAdge"; 

    private ShortEffectView _shortEffectView;

    /*private bool IsCharacterRunning => _character.CurrentVelocity != Vector3.zero;

    private void Awake()
    {
        _animator.SetBool(IsRunningKey, false);
        _shortEffectView = new ShortEffectView(GetComponentsInChildren<Renderer>(), this);

        _character.Hit += OnCharacterHit;
        _character.Dead += OnCharacterDead;
    }

    private void Update()
    {
        _animator.SetBool(IsRunningKey, IsCharacterRunning);

        if (_character.IsStrongInjury)
            SetInjuryWeight(MaxInjuryWeight);
        else
            SetInjuryWeight(0);

        _animator.SetBool(InJumpProcessKey, _character.InJumpProcess);
    }

    private void OnDestroy()
    {
        _character.Hit -= OnCharacterHit;
        _character.Dead -= OnCharacterDead;
    }*/


    private void SetInjuryWeight(float value)
    {
        float step = _transitionDuration * Time.deltaTime;
        int injuryIndex = _animator.GetLayerIndex(InjuryLayerName);
        float currentWeight = _animator.GetLayerWeight(injuryIndex);

        _animator.SetLayerWeight(injuryIndex, Mathf.Lerp(currentWeight, value, step));
    }

    private void OnCharacterDead(Character character, float deadDuration)
    {
        _animator.SetTrigger(DieKey);
        _shortEffectView.PlayIncreaseEffect(DissolveAdgeKey, deadDuration);
    }

    private void OnCharacterHit()
    {
        if (_character.IsAlive)
            _animator.SetTrigger(HitKey);

        _shortEffectView.PlayIncreaseDecreaseEffect(DamageStranghtKey, _damageEffectDuration);
    }
}
