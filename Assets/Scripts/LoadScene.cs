using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private AnimationCurve fadeCurve;
    [SerializeField] private string sceneName;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        AsyncOperation o = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        o.completed += FinishLoad;
    }

    private void FinishLoad(AsyncOperation op)
    {
        StartCoroutine(FadeLoading());
    }

    private IEnumerator FadeLoading()
    {
        float elapsed = 0;
        while(elapsed <= 0.6f)
        {
            group.alpha = fadeCurve.Evaluate(elapsed / 0.6f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        group.alpha = 0;
        SceneManager.UnloadSceneAsync(0);
    }
}
