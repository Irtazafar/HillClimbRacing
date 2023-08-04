using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingBar;
    public Text loadingText;

    private void Start()
    {
        StartCoroutine(LoadYourAsyncScene("MainMenu"));
    }
    public void LoadScene(string scene)
    {
        StartCoroutine(LoadYourAsyncScene(scene));
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        loadingPanel.SetActive(true);
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);

        while (!op.isDone)
        {
            // Calculate the loading progress based on both scene load progress and slider value
            float progress = Mathf.Clamp01(op.progress / .9f);
            float displayProgress = Mathf.Lerp(loadingBar.value, progress, Time.deltaTime * 2f);

            // Update the loading progress smoothly
            loadingBar.value = displayProgress;
            loadingText.text = (displayProgress * 100f).ToString("F0") + "%";

            yield return null;
        }
    }
}
