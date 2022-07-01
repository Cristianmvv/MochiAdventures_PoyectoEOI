using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI scoreFruitText;
    int scoreFruit;

    [Header("PausePanel")]
    [SerializeField] GameObject pausePanel;
    bool pausePanelActive;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);    //  Si al inicio del script tiene no ningun valor o si ya lo tiene, lo destruye
        else Instance = this;   //  Le a?ade el valor de este propio script
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseMenu();
    }

    #region Game Metods Collectables
    public int GetScoreFruit()
    {
        return scoreFruit;
    }

    public void ScoreFruit(int _fruitValue = 1)
    {
        scoreFruit += _fruitValue;
        scoreFruitText.text = "x" + scoreFruit;
    }
    #endregion

    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }
}
