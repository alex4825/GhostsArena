using Cinemachine;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/MainHeroConfig", fileName = "MainHeroConfig")]
public class MainHeroConfig : CharacterConfig
{
    [field: SerializeField] public float JumpHeight { get; private set; }
    [field: SerializeField] public CinemachineVirtualCamera FollowCameraPrefab { get; private set; }
}
