using System;
using UnityEngine;

[CreateAssetMenu (fileName = "VIewModel", menuName = "ILS/ViewModel")]
[Serializable]
public class UIModel : ScriptableObject
{
    public ViewModelSruct[] ViewModels;
}

