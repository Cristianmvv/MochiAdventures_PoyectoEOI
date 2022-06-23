using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    public GameObject LoadingPanel;
    public float MinLoadTime;

    public GameObject LoadingMochi;
    public float MochiSpeed;

    private string targetScene;
    private bool isLoading;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        LoadingPanel.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        targetScene = sceneName;
        StartCoroutine(LoadSceneRoutine());

        LoadingPanel.SetActive(true);
        SceneManager.LoadScene(targetScene);
        LoadingPanel.SetActive(false);
    }

    private IEnumerator LoadSceneRoutine()
    {
        isLoading = true;

        LoadingPanel.SetActive(true);
        StartCoroutine(SpinMochiRoutine());

        AsyncOperation op = SceneManager.LoadSceneAsync(targetScene);
        float elapsedLoadTime = 0f;

        while (!op.isDone)
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }
        while(elapsedLoadTime < MinLoadTime) 
        {
            elapsedLoadTime += Time.deltaTime;
            yield return null;
        }

        LoadingPanel.SetActive(false);

        isLoading = false;
    }

    private IEnumerator SpinMochiRoutine()
    {
        while(isLoading)
        {
            LoadingMochi.transform.Rotate(0, 0, -MochiSpeed);
            yield return null;
        }
    }
}
