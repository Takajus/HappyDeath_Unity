using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fungus;

public class Menu : MonoBehaviour
{
    [SerializeField] Image imageToFill;
    [SerializeField] GameObject loadScreen;
    public void UI_PlayGame()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(.6f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");
        loadScreen.SetActive(true);

        while (!asyncLoad.isDone)
        {
            float progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            imageToFill.fillAmount = progressValue;
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
