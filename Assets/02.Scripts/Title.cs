using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] GameObject go_Alert = null;

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

    public void CancelAlert()
    {
        go_Alert.SetActive(false);
    }

    public void OpenAlet()
    {
        go_Alert.SetActive(true);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
