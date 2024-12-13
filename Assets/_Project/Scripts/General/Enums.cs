using UnityEngine;

public enum States
{ 
    None = 0,
    Main = 1,
    Exercise_1 = 2,
    Exercise_2 = 3,
    Exercise_3 = 4,
    Exercise_4 = 5,
    Minigame = 6
}

public enum HitState
{
    Enter,
    Exit
}

public enum LocalState
{
    None,
   Intro,
   Observation,
   SocialInteraction,
   Closing
}

public enum ViewType
{
    Superior,
    Medium
}

[System.Serializable]
public struct ViewModelSruct
{
    public ViewType ViewType;
    [TextArea(3,3)]public string TextView;
}

[System.Serializable]
public struct PositionProviderStruct
{
    public Vector3 PlayerPosition;
    public Vector3 PlayerRotation;
    
    public Vector3 CanvasPosition;
    public Vector3 CanvasRotation;
}