using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/CharacterConfig", fileName = "CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 100f;
    [field: SerializeField] public float WalkingSpeed { get; private set; } = 3f;
    [field: SerializeField] public float RunSpeed { get; private set; } = 7f;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 500f;
    [field: SerializeField] public float TimeToSpawn { get; private set; } = 2f;
    [field: SerializeField] public float DeadDuration { get; private set; } = 5f;
}
