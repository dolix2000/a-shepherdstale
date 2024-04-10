using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public enum LevelName
    {
        MainMenu,
        LevelOne
    }

    [SerializeField]
    private Animator transition;
    [SerializeField]
    private GameObject fadeInCanvas;

    private void Start()
    {
        fadeInCanvas.SetActive(true);
        InvokeRepeating("FadeOutDisable", 3f, 3f);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
        fadeInCanvas.SetActive(true);
    }

    public void RestartLevel(int sceneIndex)
    {
        StartCoroutine(UnLoadAsync(sceneIndex));
        StartCoroutine(LoadAsync(sceneIndex));
        fadeInCanvas.SetActive(true);
    }

    public void UnloadLevel(int sceneIndex)
    {
        StartCoroutine(UnLoadAsync(sceneIndex));
    }

    public void SwitchScene(Scene scene)
    {
        SceneManager.SetActiveScene(scene);
        fadeInCanvas.SetActive(true);
    }

    // Disable Fading canvas after triggering it to arm it again
    private void FadeOutDisable()
    {
        fadeInCanvas.SetActive(false);
    }

    IEnumerator LoadAsync (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
           // transition.SetBool("FadeOut",true);
            //transition.SetTrigger("Start");
            yield return new WaitForSeconds(5f); 

        }
    }

    IEnumerator ReloadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            yield return new WaitForSeconds(5f);
        }
    }


    IEnumerator UnLoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            // transition.SetBool("FadeOut",true);
            //transition.SetTrigger("Start");
            yield return new WaitForSeconds(5f);

        }
    }


}
