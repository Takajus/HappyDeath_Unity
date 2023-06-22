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
    public AK.Wwise.Event Menu_Music;
    public void UI_PlayGame()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound(Menu_Music);
    }
    

    
    IEnumerator LoadYourAsyncScene()
    {
        loadScreen.SetActive(true);
        yield return new WaitForSeconds(.6f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");

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
