using MEC;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InteractableProgressBarController : MonoBehaviour
{
    public Action progressBarCompleted;

    [SerializeField] private float fillSpeed = 0.2f;
    [SerializeField] private float drainSpeed = 0.1f;
    [SerializeField] private float requiredTime = 5f;

    private Slider slider;
    private CoroutineHandle fillCoroutine;
    private bool isPlayedStartSound = false;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
    }

    public void EnableProgress(bool value)
    {
        CancelFillCoroutine();

        if (value)
        {
            if (!isPlayedStartSound) 
            {
                isPlayedStartSound = true; 
            }

            fillCoroutine = Timing.RunCoroutine(FillSlider());
        }
        else
        {
            fillCoroutine = Timing.RunCoroutine(DrainSlider());
        }
    }

    private IEnumerator<float> FillSlider()
    {
        while (slider.value < 1f)
        {
            slider.value += fillSpeed * Time.deltaTime / requiredTime;
            yield return Timing.WaitForOneFrame;

            if (slider.value >= 1f)
            {
                OnSliderFilled();
                yield break;
            }
        }
    }

    private IEnumerator<float> DrainSlider()
    {
        while (slider.value > 0f)
        {
            slider.value -= drainSpeed * Time.deltaTime;
            yield return Timing.WaitForOneFrame;
        }

        isPlayedStartSound = false;
    }

    private void OnSliderFilled()
    {
        CancelFillCoroutine();
        progressBarCompleted?.Invoke();
    }

    private void CancelFillCoroutine()
    {
        if(fillCoroutine != null)
            Timing.KillCoroutines(fillCoroutine);
    }

    public void Conclude()
    {
        gameObject.SetActive(false);
    }
}
