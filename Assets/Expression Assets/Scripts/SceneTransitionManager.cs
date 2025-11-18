using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeIn fadeInScript;
    public FadeOut fadeOutScript;

    public float delayBeforeFadeOut = 0.5f;

    int SceneIndex;// Adjust this if needed
    private string difficultyKey = "Difficulty";

    public void LoadScene(int sceneIndex)
    {
        //Set the scene name
        SceneIndex = sceneIndex;
        // Call fade-out script before loading the new scene
        fadeOutScript.gameObject.SetActive(true);
        Invoke("LoadSceneAfterFadeOut", delayBeforeFadeOut);
    }

    private void LoadSceneAfterFadeOut()
    {
        // Load the new scene
        SceneManager.LoadSceneAsync(SceneIndex);
    }

    public void LoadSceneWithDifficulty(string difficulty)
    {
        PlayerPrefs.SetString(difficultyKey, difficulty);
        LoadScene(2);
    }
    public void Quit()
    { 
       Application.Quit();
    }
}
