using Unity.Mathematics;
using UnityEngine;

public class PositionProvider : MonoBehaviour
{
    [Header("Dependencies")] 
    [SerializeField] private Transform vrPlayer;
    public Transform VrCanvas;

    public static PositionProvider Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void SetPosition(PositionProviderStruct provider)
    {
        vrPlayer.SetPositionAndRotation(provider.PlayerPosition, quaternion.Euler(provider.PlayerRotation));
        VrCanvas.SetPositionAndRotation(provider.CanvasPosition, quaternion.Euler(provider.CanvasRotation));
    }
}