using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class FadeManager
{
    private static async UniTask Fade(CanvasGroup cg, float targetAlpha, float duration, Action onComplete = null)
    {
        float startAlpha = cg.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            await UniTask.Yield();   
        }

        cg.alpha = targetAlpha; 
        onComplete?.Invoke();   
    }

    public static async UniTask FadeIn(CanvasGroup cg, float duration, Action onComplete = null)
    {
        await Fade(cg, 1f, duration, onComplete);
    }
    
    public static async UniTask FadeInCanvas(CanvasGroup cg, float duration)
    {       
        await Fade(cg, 1f, duration);
    }
    
    public static async UniTask FadeOut(CanvasGroup cg, float duration, Action onComplete = null)
    {
        await Fade(cg, 0f, duration, onComplete);
    }
    
    public static async UniTask Transition(CanvasGroup fromCg, CanvasGroup toCg, float duration, Action onComplete = null)
    {
        await FadeOut(fromCg, duration, () =>
        {
            FadeIn(toCg, duration, onComplete);
        });
    }
}