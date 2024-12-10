using UnityEngine;

public class Globals
{
    //Audio
    public static bool MusicActivated = true;
    public static bool SFXActivated = true;

    //Debug Mode
#if UNITY_EDITOR
    public static bool DebugModeActivated = true;
    public static bool DebugCollisionActivated = false;
#else    
    public static bool DebugModeActivated = false;
#endif

}
