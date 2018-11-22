    /**
 * Fade between scenes 
 * Modified from https://gist.github.com/leMaur/d99fd93a9812b76c535f319cb6b5478d 
 * which in turn is based on https://youtu.be/0HwZQt94uHQ
 * Terms can be found at http://devassets.com/guidelines/
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class FadeScenes : MonoBehaviour
{
    public Texture2D ProgressBar;
    public Texture2D ProgressBarBackground;
    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.8f;
    public bool fadeAudio = true;

    private bool isInProgress = true;
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;
    private AsyncOperation Async;

    public void Update()
    {
        if(this.isInProgress && this.fadeAudio)
        {
            AudioListener.volume = 1f - alpha;
        }
    }

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.fixedDeltaTime;
        alpha = Mathf.Clamp01(alpha);
        if(fadeDir == 1)
        {
            alpha = 1f;
        }
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        isInProgress = true;
        return fadeSpeed;
    }

    void OnLevelWasLoaded()
    {
        BeginFade(-1);
    }


    /**
	 * Load scene with fade in/out effect
	 * 
	 * @param string SceneName
	 * @param float WaitFor = 0.6f
	 * @return void
	 */
    public void LoadScene(string SceneName, float WaitFor = 0.6f)
    {

        StartCoroutine(ChangeScene(SceneName, WaitFor));
    }


    /**
	 * Load scene with fade in/out effect asynchronously
	 * 
	 * @param string SceneName
	 * @param float WaitFor = 0.6f
	 * @return void
	 */
    public void LoadSceneAsync(string SceneName, float WaitFor = 0.6f)
    {

        StartCoroutine(ChangeSceneAsync(SceneName, WaitFor));
    }


    /**
	 * Change scene
	 * 
	 * @param string SceneName
	 * @param float WaitFor = 0.6f
	 * @return void
	 */
    IEnumerator ChangeScene(string SceneName, float WaitFor = 0.6f)
    {
        yield return StartCoroutine(WaitForRealSeconds(WaitFor));
        float fadeTime = BeginFade(1);
        yield return StartCoroutine(WaitForRealSeconds(fadeTime));
        isInProgress = false;
        SceneManager.LoadScene(SceneName);
    }


    /**
	 * Change scene asynchronously
	 * 
	 * @param string SceneName
	 * @param float WaitFor = 0.6f
	 * @return void
	 */
    IEnumerator ChangeSceneAsync(string SceneName, float WaitFor = 0.6f)
    {
        //yield return StartCoroutine(WaitForRealSeconds(WaitFor));
        BeginFade(1);
        Async = SceneManager.LoadSceneAsync(SceneName);
        Async.allowSceneActivation = true;
        isInProgress = false;
        yield return Async;
    }


    /**
	 * Works when timeScale=0
	 * 
	 * @param float time
	 * @return yield
	 */
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
}