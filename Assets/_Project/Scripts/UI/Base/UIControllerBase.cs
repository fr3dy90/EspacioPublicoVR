using UnityEngine;

public abstract class UIControllerBase : MonoBehaviour
{
    protected virtual void Awake()
    {

    }

    public virtual void Initialize(params object[] parameters)
    {
        AddListeners();
    }

    protected virtual void AddListeners()
    {

    }

    protected virtual void RemoveListeners()
    {

    }

    public virtual void Conclude()
    {
        RemoveListeners();
    }
    
    public virtual void OnShow()
    {
        
    }
}
