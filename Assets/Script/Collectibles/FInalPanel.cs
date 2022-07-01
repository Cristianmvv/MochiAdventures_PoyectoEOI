using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalPanel : MonoBehaviour
{
    public GameObject finalPanel;
    public Text textFruit;
    int totalFruits;
    public int levelNumber;

    private void Start()
    {
        totalFruits = GameObject.FindGameObjectsWithTag("Pickups/Fruit").Length + (GameObject.FindGameObjectsWithTag("Pickups/FruitX5").Length * 5);
        finalPanel.SetActive(false);

    }

    public IEnumerator OpenfinalPanel()
    {
        PlayerPrefs.SetInt("ScoreLevel" + levelNumber, GameManager.Instance.GetScoreFruit());
        finalPanel.SetActive(true);
        if (GameManager.Instance.GetScoreFruit() > 0)
        {
            for (int i = 0; i <= GameManager.Instance.GetScoreFruit(); i++)
            {
                textFruit.text = i + " / " + totalFruits;
                yield return new WaitForSeconds((float)5 / (float)GameManager.Instance.GetScoreFruit());
            }

            GetComponent<ParticleSystem>().Play();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))
        {
            print("mochidentro");
            StartCoroutine(OpenfinalPanel());
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
