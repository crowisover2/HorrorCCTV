using System;
using Unity.VisualScripting;

public class Constant
{
    [Serializable]
    public enum Type
    {
        SWAP,
        EXTRA,
        MISSING,
        MOVEMENT
    }

    [Serializable]
    public enum SettingStage
    {
        SCHOOL
    }

    [Serializable]
    public enum StateSpawn
    {
        TASK_AVAILABLE,
        TASK_AVAILABLE_NOT,
        GAME_OVER_WARNING
    }
}