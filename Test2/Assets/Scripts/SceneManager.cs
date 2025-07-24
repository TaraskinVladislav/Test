using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [Header("Loading UI (опционально)")]
    public GameObject loadingScreen;
    public Slider progressBar;
    public float fakeLoadTime = 1.5f; 

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void LoadSceneWithDelay(string sceneName, float delay = 0.5f)
    {
        StartCoroutine(LoadWithDelayCoroutine(sceneName, delay));
    }

    private IEnumerator LoadWithDelayCoroutine(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return LoadSceneAsync(sceneName);
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
                progressBar.value = progress;

            timer += Time.deltaTime;

            if (progress >= 1f || (operation.progress >= 0.9f && timer >= fakeLoadTime))
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
