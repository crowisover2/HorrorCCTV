using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

public class StageRoom : MonoBehaviour
{
    [SerializeField] private CinemachineCamera kamera;
    [SerializeField] private List<StrangeTrigger> ListOfStranges;

    List<int> doneIndices = new List<int>();

    private bool debugStatus = false;

    public int StrangeCount => ListOfStranges.Count;

    public int Priority
    {
        set
        {
            kamera.Priority = value;
            if (value == 0) gameObject.SetActive(false);
            else gameObject.SetActive(true);
        }
        get { return kamera.Priority; }
    }

    public bool IsGameOver()
    {
        // Counts items where HasBeenUsed is true AND HasBeenSolved is false
        int count = ListOfStranges.Count(t => t != null && t.HasBeenUsed && !t.HasBeenSolved);
        return count >= 3;
    }

    public Constant.StateSpawn SpawnStrange()
    {
        if (IsGameOver())
        {
            return Constant.StateSpawn.GAME_OVER_WARNING;
        }

        var availableIndices = Enumerable.Range(0, ListOfStranges.Count)
                                         .Where(i => !doneIndices.Contains(i))
                                         .ToList();

        if (availableIndices.Count == 0)
        {
            Debug.LogWarning("All Stranges have been used!");
            return Constant.StateSpawn.TASK_AVAILABLE_NOT;
        }

        int randomIndexInAvailable = Random.Range(0, availableIndices.Count);
        int actualIndex = availableIndices[randomIndexInAvailable];

        doneIndices.Add(actualIndex);
        ListOfStranges[actualIndex].Activate();
        return Constant.StateSpawn.TASK_AVAILABLE;
    }

    public void DebugSpawnStrange()
    {
        foreach (var item in ListOfStranges)
        {
            if (debugStatus) item.Activate();
            else item.Deactivate();
        }

        debugStatus = !debugStatus;
    }

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}

#if UNITY_EDITOR
[CustomEditor(typeof(StageRoom))]
public class StageRoomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        StageRoom data = (StageRoom)target;

        if (GUILayout.Button("On/Off Strange"))
        {
            data.DebugSpawnStrange();
        }
    }
}
#endif
