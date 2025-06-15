using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/AgentConfig", fileName = "AgentConfig")]
public class AgentConfig : CharacterConfig
{
    [field: SerializeField] public float TimeForIdle { get; private set; } = 2f;
    [field: SerializeField] public float JumpSpeed { get; private set; } = 5f;
    [field: SerializeField] public AnimationCurve JumpCurve { get; private set; }
}
