using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
#region Вспомогательные структуры данных
[Serializable]
public class MiniGameProgress
{
    public string id;     
    public int stars;     
    public bool completed;

    public MiniGameProgress(string id)
    {
        this.id = id;
        stars = 0;
        completed = false;
    }
}
[Serializable]
public class ProgressWrapper
{
    public List<MiniGameProgress> list = new List<MiniGameProgress>();
}
#endregion
public class GameManager : MonoBehaviour
{
    public int score;
    public static GameManager Instance { get; private set; }

    [Header("Scenes")]
    [Tooltip("MainMenu")]
    public string mainMenuScene = "MainMenu";
    [Tooltip("Play")]
    public string selectScene = "SelectMiniGame";

    private const string PREF_KEY = "EDUQUEST_PROGRESS";
    private ProgressWrapper progress = new ProgressWrapper();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadProgress();
    }
    public void LoadSelectScene() => SceneManager.LoadScene(selectScene);
    public void LoadMiniGame(string sceneName) => SceneManager.LoadScene(sceneName);
    public void LoadMainMenu() => SceneManager.LoadScene(mainMenuScene);
    public void SetStars(string gameId, int starCount)
    {
        if (starCount < 0) starCount = 0;
        if (starCount > 3) starCount = 3;

        MiniGameProgress mg = GetOrCreate(gameId);
        if (starCount > mg.stars) mg.stars = starCount;
        mg.completed = true;

        SaveProgress();
    }
    private string currentGameId;
    public void StartMiniGame(string sceneName, string gameId)
    {
        currentGameId = gameId;
        LoadMiniGame(sceneName);
    }
    public int GetStars(string gameId) => GetOrCreate(gameId).stars;

    public bool IsCompleted(string gameId) => GetOrCreate(gameId).completed;
    public void ResetAllProgress()
    {
        PlayerPrefs.DeleteKey(PREF_KEY);
        progress = new ProgressWrapper();
    }

    private MiniGameProgress GetOrCreate(string id)
    {
        MiniGameProgress mg = progress.list.Find(p => p.id == id);
        if (mg == null)
        {
            mg = new MiniGameProgress(id);
            progress.list.Add(mg);
        }
        return mg;
    }
    public void SaveProgress()
    {
        string json = JsonUtility.ToJson(progress);
        PlayerPrefs.SetString(PREF_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        if (PlayerPrefs.HasKey(PREF_KEY))
        {
            string json = PlayerPrefs.GetString(PREF_KEY);
            progress = JsonUtility.FromJson<ProgressWrapper>(json);
            if (progress == null) progress = new ProgressWrapper();
        }
    }
    
    public void Play()
    {
        SceneManager.LoadScene("Play");
    }
    public void Exit()
    {
        Debug.Log("Вы вышли из игры");
        Application.Quit();
    }
}
