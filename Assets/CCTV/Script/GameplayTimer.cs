using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
public class GameplayTimer : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private Text txtTime; 

    // Public auto-property with private setter (Encapsulation)
    public int CurrentTime { get; private set; }

    private GameSetting _data;
    private float _durationGameplay;
    private int _strangeCount;

    // Cache the Wait object to avoid GC allocations
    private readonly WaitForSeconds _oneSecondWait = new WaitForSeconds(1f);

    public void StartTimer(GameSetting dataSetting, int roomCount, params int[] totalStrange)
    {
        _data = dataSetting;
        CurrentTime = 0;
        _strangeCount = totalStrange.Length; // params is already an array, just get Length!

        if (_data.UseAuto_DurationGameplay)
            _durationGameplay = roomCount * GameSetting.DurationOneRoom;

        if (_data.UseAuto_DurationSpawnRange)
        {
            _data.DurationSpawnRange = (_durationGameplay - _data.DurationGrace) / _strangeCount;
        }

        StopAllCoroutines(); // Safety: prevent multiple timers running
        StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        int fakeTimeCounter = 0;

        for (int i = 0; i < _durationGameplay; i++)
        {
            yield return _oneSecondWait;
            CurrentTime = i;

            // Simple math instead of nested If-statements
            if (CurrentTime % _data.IntervalTimeBetweenFakeMinute == 0)
            {
                fakeTimeCounter++;
            }

            // Convert total fake minutes into H:M format mathematically
            int hours = (fakeTimeCounter / 60);
            int minutes = (fakeTimeCounter % 60);

            // String Interpolation (C# 10+) is cleaner and highly optimized in Unity 6
            txtTime.text = $"{hours:D2}:{minutes:D2}";
        }
    }
}