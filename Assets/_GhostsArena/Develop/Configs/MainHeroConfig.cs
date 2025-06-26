using Cinemachine;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/MainHeroConfig", fileName = "MainHeroConfig")]
public class MainHeroConfig : CharacterConfig
{
    [field: SerializeField] public CharacterControllerCharacter CharacterControllerCharacterPrefab { get; private set; }
    [field: SerializeField] public float JumpHeight { get; private set; } = 3;
    [field: SerializeField] public float DistantAttack { get; private set; } = 15;
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
    [field: SerializeField] public float ShootForce { get; private set; } = 50;
    [field: SerializeField] public float BulletLifetime { get; private set; } = 5;
}
