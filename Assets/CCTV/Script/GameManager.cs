using Assets.CCTV.Script;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameSetting gameSetting;

    [Header("UI")]
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;

    [Header("Rooms")]
    [SerializeField] private SpawnSystem spawnSystem;

    [Header("Timer")]
    [SerializeField] private GameplayTimer gameplayTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitCamera();
        InitGameplayTimer();
    }

    private void InitGameplayTimer()
    {
        gameplayTimer.StartTimer(gameSetting, spawnSystem.NumCam, spawnSystem.AllStrangeCountEachRoom);
    }

    #region Camera
    void InitCamera()
    {
        spawnSystem.StartSpawnSystem(gameSetting);
        btnLeft.onClick.AddListener(() => { spawnSystem.Increase(1); });
        btnRight.onClick.AddListener(() => { spawnSystem.Increase(-1); });

        gameplayTimer.OnSpawnTime += spawnSystem.DoSpawnWorkAsync;

        spawnSystem.OnActive = ()=> gameplayTimer.OnSpawnTime += spawnSystem.DoSpawnWorkAsync;
        spawnSystem.OnDeactive = () => gameplayTimer.OnSpawnTime -= spawnSystem.DoSpawnWorkAsync;
    }

    #endregion
}
