using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public int[] scoreLevel;
    public Button[] buttonLevel;

    private void Start()
    {
        LoadData();
        for (int i = 0; i < buttonLevel.Length; i++)
        {
            if (i > 0 && scoreLevel[i - 1] <= 0)
                buttonLevel[i].interactable = false;
            else
                buttonLevel[i].interactable = true;
        }
        transform.parent.gameObject.SetActive(false);
    }

    void LoadData()
    {
        scoreLevel = new int[buttonLevel.Length];
        for (int i = 0; i < buttonLevel.Length; i++)
        {
            scoreLevel[i] = PlayerPrefs.GetInt("ScoreLevel" + i);
        }
    }
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
