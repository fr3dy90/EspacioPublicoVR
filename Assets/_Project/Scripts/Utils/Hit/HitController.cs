using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitController : MonoBehaviour
{
    public Action<Collider, HitState> Hit;
    protected Collider hitCollider;

    protected virtual void Awake()
    {
        hitCollider = GetComponentInChildren<Collider>();
        hitCollider.isTrigger = true;
        hitCollider.enabled = false;
        this.tag = "Hit";
    }

    public virtual void Active()
    {
        gameObject.SetActive(true);
        hitCollider.enabled = true;
    }
    public virtual void Desactive()
    {
        hitCollider.enabled = false;
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider hit)
    {
        Hit?.Invoke(hit,HitState.Enter);
    }

    protected virtual void OnTriggerExit(Collider hit)
    {
        Hit?.Invoke(hit, HitState.Exit);
    }
}
