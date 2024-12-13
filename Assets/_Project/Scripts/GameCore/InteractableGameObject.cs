using System;
using UnityEngine;

public class InteractableGameObject : MonoBehaviour
{
    [Header("Dependncies")]
    [SerializeField] private GameStateBase state;
    [SerializeField] private BoxCollider collider;
    
    public bool isGoodPlaced;
    [SerializeField] public int score;

    private void Awake()
    {
        if (collider == null) collider = GetComponent<BoxCollider>();
    }

    public void OnInteraction()
    {
        if (state is Excercise1State _state)
        {
            _state.GetObjectsFounded(this);
            collider.enabled = false;
            ScoreManager.Instance.AddScore(score);
        }
    }
    
    
}