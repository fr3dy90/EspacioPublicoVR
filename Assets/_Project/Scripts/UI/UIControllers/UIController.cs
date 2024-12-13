using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIController : UIControllerBase
{
    [Header("Depencies")] [SerializeField] private UIView view;

    [Header("Model")] [SerializeField] private UIModel model;


    public override void Initialize(params object[] parameters)
    {
        base.Initialize(parameters);
        view.Initialize();
    }

    public async UniTask SetView(int _indexView, Action _callback = null)
    {
        await SetView(model.ViewModels[_indexView], _callback);
    }


    private async UniTask SetView(ViewModelSruct _viewModelSruct, Action _onAction = null)
    {
        await view.SetView(_viewModelSruct.ViewType, _viewModelSruct.TextView, _onAction);
    }

    public async UniTask SetView(ViewType _viewType, string _textView, Action _onAction = null)
    {
        await view.SetView(_viewType, _textView, _onAction);
    }
}
  