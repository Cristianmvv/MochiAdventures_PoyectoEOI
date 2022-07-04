using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalPanel : MonoBehaviour
{
    public GameObject finalPanel;
    public Text textFruit;
    public int totalFruits;
    public int levelNumber;

    Button nextButton;
    Button menuButton;

    private void Awake()
    {
        finalPanel = GameObject.FindGameObjectWithTag("Panel/FinalPanel");
        textFruit = GameObject.FindGameObjectWithTag("Panel/FinalPanelScore").GetComponent<Text>();
    }

    private void Start()
    {
        #region Checkeo de si las cosas estan bien
            #if UNITY_EDITOR

        if (finalPanel == null)
            Debug.Log("<color=red>Error: </color> Recuerda activar el <FinalPanel> antes de iniciar el juego");

            #endif
        #endregion

        nextButton = GameObject.FindGameObjectWithTag("Button/Next").GetComponent<Button>();
        nextButton.onClick.AddListener(GameManager.Instance.NextButton);

        menuButton = GameObject.FindGameObjectWithTag("Button/Menu").GetComponent<Button>();
        menuButton.onClick.AddListener(GameManager.Instance.MenuButton);

        totalFruits = GameObject.FindGameObjectsWithTag("Pickups/Fruit").Length + (GameObject.FindGameObjectsWithTag("Pickups/FruitX5").Length * 5);
        finalPanel.SetActive(false);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))
        {
            print("mochidentro");
            Invoke("CallCoroutine", 1);
        }
    }

    void CallCoroutine()
    {
        StartCoroutine(OpenfinalPanel());
    }

    public IEnumerator OpenfinalPanel()
    {
        PlayerPrefs.SetInt("ScoreLevel" + levelNumber, GameManager.Instance.GetScoreFruit());
        finalPanel.SetActive(true);
        if (GameManager.Instance.GetScoreFruit() > (25 * GameManager.Instance.GetScoreFruit()/100))
        {
            GetComponent<ParticleSystem>().Play();

            for (int i = 0; i <= GameManager.Instance.GetScoreFruit(); i++)
            {
                print("Conteo panel final");
                textFruit.text = i + " / " + totalFruits;
                yield return new WaitForSeconds((float)5 / (float)GameManager.Instance.GetScoreFruit());
            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
