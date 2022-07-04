using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI scoreFruitText;
    int scoreFruit;
    AudioSource audioS;
    public AudioClip appleClip, melonClip;

    [Header("PausePanel")]
    [SerializeField] GameObject pausePanel;
    Button resumeButton;
    Button restartButton;
    Button exitButton;

    

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);    //  Si al inicio del script tiene no ningun valor o si ya lo tiene, lo destruye
        else Instance = this;   //  Le a?ade el valor de este propio script


        pausePanel = GameObject.FindGameObjectWithTag("Panel/PausePanel");
        scoreFruitText = GameObject.FindGameObjectWithTag("Panel/CurrentScore").GetComponent<TextMeshProUGUI>();
        audioS = GetComponent<AudioSource>();
    }

    private void Start()
    {
        #region Checkeo de si las cosas estan bien
            #if UNITY_EDITOR

        GameObject DEPRECATED;
        DEPRECATED = GameObject.FindGameObjectWithTag("DEPRECATED"); //  GameObject con este tag son los que no se quieren usar

        if (GameObject.FindGameObjectWithTag("WinZone") == null)
            Debug.Log("<color=red>Error: </color>Te falta poner la zona final");
        if (DEPRECATED != null)
            Debug.Log("<color=red>Error: </color>Debes sustituir el gameobject<"+DEPRECATED.name +">Por el bueno en MochiStartedPack",DEPRECATED);
        if (pausePanel == null)
            Debug.Log("<color=red>Error: </color> Recuerda activar el <PausePanel> antes de iniciar el juego");


            
            #endif
        #endregion

        resumeButton = GameObject.FindGameObjectWithTag("Button/Resume").GetComponent<Button>();
        resumeButton.onClick.AddListener(ResumeButtom);

        restartButton = GameObject.FindGameObjectWithTag("Button/Restart").GetComponent<Button>();
        restartButton.onClick.AddListener(RestartButton);

        exitButton = GameObject.FindGameObjectWithTag("Button/Exit").GetComponent<Button>();
        exitButton.onClick.AddListener(ExitButton);



        pausePanel.SetActive(false);
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseMenu();
    }

    #region Game Metods Collectables
    public int GetScoreFruit()
    {
        print("Cojer Datos de Puntuacion");
        return scoreFruit;
    }

    public void ScoreFruit(int _fruitValue = 1)
    {
        if (_fruitValue == 1)
            audioS.clip = appleClip;
        else
            audioS.clip = melonClip;
        audioS.Play();

        scoreFruit += _fruitValue;
        scoreFruitText.text = "x" + scoreFruit;
    }
    #endregion

    #region Menu de Pausa
    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeInHierarchy)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
            else
            {
                ResumeButtom();
            }
        }
    }

    public void ResumeButtom()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);

    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ExitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void NextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    
    #endregion
}
