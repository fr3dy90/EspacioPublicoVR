using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView : UIViewBase
{
    [Header("Cavnvas Group")] 
    [SerializeField] private CanvasGroup globalCanvasGroup;
    [SerializeField] private CanvasGroup interiorCanvasGroup;
    
    [Header("Contents")] 
    [SerializeField] private Image superiorContent;
    [SerializeField] private Image mediumContent;

    [Header("Texts")] 
    [SerializeField] private TextMeshProUGUI textSuperiorContent;
    [SerializeField] private TextMeshProUGUI textmediumContent;

    [Header("Button")]
    [SerializeField] private Button iniciarButton;
    
    [Header("Extras")]
    [SerializeField] private GameObject tutorialInstructions;
    
    //Consts
    private const int DELAY = 1;
    private const float DURATION = .5f;

    public override void Initialize(params object[] parameters)
    {
        base.Initialize(parameters);
        globalCanvasGroup.alpha = 0;
        interiorCanvasGroup.alpha = 0;
    }

    public async UniTask SetView(ViewType _viewType, string _string, Action _onCallButton = null)
    {
        superiorContent.gameObject.SetActive(_viewType == ViewType.Superior);
        mediumContent.gameObject.SetActive(_viewType != ViewType.Superior);

        if (string.IsNullOrEmpty(_string))
        {
            tutorialInstructions.SetActive(true);
            textSuperiorContent.gameObject.SetActive(false);
             textmediumContent.gameObject.SetActive(false);
        }
        else
        {
            textSuperiorContent.text = _string;
            textmediumContent.text = _string;
            textSuperiorContent.gameObject.SetActive(_viewType == ViewType.Superior);
            textmediumContent.gameObject.SetActive(_viewType != ViewType.Superior);
        }
        
        if (globalCanvasGroup.alpha < 1)
        {
            await FadeManager.FadeIn(globalCanvasGroup, DURATION);
            await UniTask.WaitForSeconds(DELAY);
        }

        iniciarButton.gameObject.SetActive(_onCallButton != null);
        
        if (_onCallButton != null)
        {
            iniciarButton.onClick.RemoveAllListeners();
            iniciarButton.onClick.AddListener(() =>
            {
                _onCallButton?.Invoke();

            });
            
        }
        
        await FadeManager.FadeIn(interiorCanvasGroup, DURATION);
    }
}