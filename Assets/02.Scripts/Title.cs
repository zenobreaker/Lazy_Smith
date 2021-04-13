using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    void Update()
    {
        if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    IEnumerator LoadCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("GameScene");
        while (!operation.isDone) 
        {
            yield return null;
        }
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
