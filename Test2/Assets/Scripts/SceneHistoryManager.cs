using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHistoryManager : MonoBehaviour
{
    public static SceneHistoryManager Instance;

    private Stack<string> sceneHistory = new Stack<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // чтобы не удалялся при переходе между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        sceneHistory.Push(currentScene);
        SceneManager.LoadScene(sceneName);
    }

    public void GoBack()
    {
        if (sceneHistory.Count > 0)
        {
            string previousScene = sceneHistory.Pop();
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.Log("Нет предыдущей сцены.");
        }
    }

    public void ClearHistory()
    {
        sceneHistory.Clear();
    }
    public void BackButton()
    {
        SceneHistoryManager.Instance.GoBack();
    }
    public void GoToMiniGame()
    {
        SceneHistoryManager.Instance.LoadScene("MiniGameSceneName");
    }
}
