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
}