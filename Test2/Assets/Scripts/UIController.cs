using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Ёкраны UI")]
    public GameObject mainMenuUI;
    public GameObject levelSelectUI;
    public GameObject gameplayUI;
    public GameObject pauseUI;

    [Header("ѕлавные переходы")]
    public Animator transitionAnimator;
    void Start()
    {
        ShowMainMenu();
    }


    public void ShowMainMenu()
    {
        CloseAll();
        mainMenuUI.SetActive(true);
    }

    public void ShowLevelSelect()
    {
        CloseAll();
        levelSelectUI.SetActive(true);
    }

    public void ShowGameplay()
    {
        CloseAll();
        gameplayUI.SetActive(true);
    }

    public void ShowPause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    private void CloseAll()
    {
        mainMenuUI.SetActive(false);
        levelSelectUI.SetActive(false);
        gameplayUI.SetActive(false);
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void StartMiniGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void OnPlayButtonPressed()
    {
        ShowLevelSelect();
    }

    public void OnBackToMenuPressed()
    {
        GameManager.Instance.SaveProgress();
        ShowMainMenu();
    }

    public void OnPauseButtonPressed()
    {
        ShowPause();
    }

    public void OnResumeButtonPressed()
    {
        ResumeGame();
    }

    public void OnExitButtonPressed()
    {
        Application.Quit();
    }

    public void PlayTransition(System.Action callback = null)
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Start");
            StartCoroutine(ExecuteAfterDelay(0.5f, callback));
        }
        else
        {
            callback?.Invoke();
        }
    }

    private System.Collections.IEnumerator ExecuteAfterDelay(float delay, System.Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
