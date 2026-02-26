using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingData", menuName = "Game CCTV/GameSettingData")]
public class GameSetting : ScriptableObject
{
    [Header("Stage Settings")]
    [SerializeField] private Constant.SettingStage stage;

    [Header("Presence Settings")]
    [SerializeField] private float maxStrangePresence;

    [Header("Spawn Timing (Second)")]
    [SerializeField] private bool useAuto_DurationSpawnRange;
    public float DurationSpawnRange;

    [Header("Gameplay Timing (Second)")]
    [SerializeField] private bool useAuto_DurationGameplay;
    [SerializeField] private float durationGameplay;
    [SerializeField] private float durationGrace;
    [SerializeField] private float intervalTimeBetweenFakeMinute;

    [Header("Reporting Setting (Second)")]
    [SerializeField] private float durationDelayAfterReport;

    public const float DurationOneRoom = 600; // sec

    public Constant.SettingStage Stage => stage;
    public float MaxStrangePresence => maxStrangePresence;
    public bool UseAuto_DurationSpawnRange => useAuto_DurationSpawnRange;
    public bool UseAuto_DurationGameplay => useAuto_DurationGameplay;
    public float DurationGameplay => durationGameplay;
    public float DurationGrace => durationGrace;
    public float IntervalTimeBetweenFakeMinute => intervalTimeBetweenFakeMinute;
    public float DurationDelayAfterReport => durationDelayAfterReport;

}