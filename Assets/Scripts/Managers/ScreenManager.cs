using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// from unity docs https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html
public class ScreenManager : Singleton<ScreenManager> 
{
    public void TransitionToMainGame()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainScene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}