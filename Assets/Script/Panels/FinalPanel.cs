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
    Button restartButton;


    public Image[] estrellitas;
    int frutasRecogidas;
    float porcentajeFrutasRecogidas;


    private void Awake()
    {
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Estrellitas");
        estrellitas = new Image[stars.Length];
        for (int i = 0; i < stars.Length; ++i)
            estrellitas[i] = stars[i].GetComponent<Image>();

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

        restartButton = GameObject.FindGameObjectWithTag("Button/RestartFinal").GetComponent<Button>();
        restartButton.onClick.AddListener(GameManager.Instance.RestartButton);

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
        StartCoroutine(ActivarEstrellitas());
    }

    public IEnumerator OpenfinalPanel()
    {
        PlayerPrefs.SetInt("ScoreLevel" + levelNumber, GameManager.Instance.GetScoreFruit());
        finalPanel.SetActive(true);
        if (GameManager.Instance.GetScoreFruit() >= 0 )
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

    IEnumerator ActivarEstrellitas()
    {

        frutasRecogidas = GameManager.Instance.GetScoreFruit();
        porcentajeFrutasRecogidas = (float)frutasRecogidas / (float)totalFruits;

        if (porcentajeFrutasRecogidas > 0.66f)
        {
            for (int i = 0; i < estrellitas.Length; i++)
            {
                while (estrellitas[i].fillAmount < 1)
                {
                    estrellitas[i].fillAmount += 0.01f;
                    yield return new WaitForSeconds(0.005f);
                }
            }
        }
        else if (porcentajeFrutasRecogidas > 0.33f)
        {
            for (int i = 0; i < 2; i++)
            {
                while (estrellitas[i].fillAmount < 1)
                {
                    estrellitas[i].fillAmount += 0.01f;
                    yield return new WaitForSeconds(0.050f);
                }
            }
        }
        else
        {
            while (estrellitas[0].fillAmount < 1)
            {
                estrellitas[0].fillAmount += 0.01f;
                yield return new WaitForSeconds(0.005f);
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
