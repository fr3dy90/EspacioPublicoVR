using MEC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SceneLoaderView : UIViewBase
{
    private Canvas loaderCanvas;
    private Slider loaderProgressBar;

    [SerializeField] private RectTransform fadePanel;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float progressSpeed = 0.5f;

    private Vector2 initialFadePanelPosition;

    protected override void Awake()
    {
        base.Awake();
        loaderCanvas = GetComponent<Canvas>();
        loaderProgressBar = GetComponentInChildren<Slider>();

        initialFadePanelPosition = fadePanel.anchoredPosition;
    }

    public override void Initialize(params object[] parameters)
    {
        base.Initialize(parameters);
        loaderCanvas.worldCamera = Camera.main;
        loaderProgressBar.value = 0;
        fadePanel.anchoredPosition = initialFadePanelPosition;
    }

    public void UpdateProgress(float targetValue)
    {
        targetValue = Mathf.Clamp01(targetValue);
        loaderProgressBar.DOValue(targetValue, progressSpeed)
            .SetEase(Ease.OutQuad)
            .SetAutoKill(true);
    }

    public IEnumerator<float> PlayFade()
    {
        fadePanel.DOAnchorPos(Vector2.zero, fadeDuration).SetEase(Ease.OutCubic).SetAutoKill(true);

        yield return Timing.WaitForSeconds(fadeDuration);
    }
}
