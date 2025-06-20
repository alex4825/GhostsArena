using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public float EnemiesSpawnDuration { get; private set; } = 10;
    [field: SerializeField] public Vector3 MainHeroStartPosition { get; private set; }
    [field: SerializeField] public WinConditions WinCondition { get; private set; }
    [field: SerializeField] public float TimeToWin { get; private set; } = 30;
    [field: SerializeField] public int KillEnemiesToWin { get; private set; } = 10;
    [field: SerializeField] public DefeatConditions DefeatCondition { get; private set; }
    [field: SerializeField] public int MaxSpawnedEnemies { get; private set; } = 20;

    [ContextMenu("UpdateMainHeroSpawnPoint")]
    private void UpdateStartHeroPosition()
    {
        GameObject point = GameObject.FindGameObjectWithTag("MainHeroSpawnPoint");
        MainHeroStartPosition = point.transform.position;
    }
}
