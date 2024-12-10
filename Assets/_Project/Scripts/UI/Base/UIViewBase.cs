using UnityEngine;

public abstract class UIViewBase : MonoBehaviour
{
    protected virtual void Awake()
    {

    }

    public virtual void Initialize(params object[] parameters)
    {
        AddListeners();
    }

    protected virtual void AddListeners() { }

    protected virtual void RemoveListeners() { }

    public virtual void Conclude()
    {
        RemoveListeners();
    }

    public void ShowView()
    {
        gameObject.SetActive(true);
    }

    public void HideView()
    {
        gameObject.SetActive(false);
    }
}
