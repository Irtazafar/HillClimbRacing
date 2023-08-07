using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void LoadScene()
    {
        StartCoroutine(LoadYourAsyncScene("GameScene"));
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);

        while (!op.isDone)
        {
            yield return null;
        }
    }
}
