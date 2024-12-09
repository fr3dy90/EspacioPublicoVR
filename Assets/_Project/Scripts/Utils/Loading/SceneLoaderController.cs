using MEC;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoaderController : MonoBehaviour
{
    public Action UnloadSceneComplete;
    public Action SwitchScenesCompleted;

    private CoroutineHandle sceneCoroutine;

    [SerializeField] private SceneLoaderView view;

    public void SwitchScenes(int currentScene, int nextScene)
    {
        view.ShowView();

        view.Initialize();

        sceneCoroutine = Timing.RunCoroutine(ChangeScene(currentScene, nextScene));
    }

    private IEnumerator<float> ChangeScene(int currentScene, int newScene)
    {
        if (SceneManager.GetSceneByBuildIndex(currentScene).isLoaded)
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentScene);

            while (!unloadOperation.isDone)
            {
                view.UpdateProgress(unloadOperation.progress * 0.5f);

                yield return Timing.WaitForOneFrame;
            }
        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            float targetProgress = 0.5f + loadOperation.progress * 0.5f;

            view.UpdateProgress(targetProgress);

            yield return Timing.WaitForOneFrame;
        }

        view.UpdateProgress(1.0f);

        yield return Timing.WaitForSeconds(1f);

        //yield return Timing.WaitUntilDone(view.PlayFade());

        SwitchScenesCompleted?.Invoke();

        Timing.KillCoroutines(sceneCoroutine);

        view.HideView();
    }

    public void LoadScene(int scene)
    {
        view.ShowView();

        view.Initialize();

        sceneCoroutine = Timing.RunCoroutine(LoadSceneOperation(scene));
    }

    private IEnumerator<float> LoadSceneOperation(int scene)
    {
        view.HideView();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            yield return Timing.WaitForOneFrame;
        }

        SwitchScenesCompleted?.Invoke();

        Timing.KillCoroutines(sceneCoroutine);
    }

    public void UnloadScene(int scene)
    {
        view.ShowView();

        view.Initialize();

        sceneCoroutine = Timing.RunCoroutine(UnloadSceneOperation(scene));
    }

    private IEnumerator<float> UnloadSceneOperation(int scene)
    {
        if (SceneManager.GetSceneAt(scene).isLoaded)
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(scene);

            while (!unloadOperation.isDone)
            {
                view.UpdateProgress(unloadOperation.progress);

                yield return Timing.WaitForOneFrame;
            }
        }

        UnloadSceneComplete?.Invoke();

        Timing.KillCoroutines(sceneCoroutine);

        view.HideView();
    } 
}
