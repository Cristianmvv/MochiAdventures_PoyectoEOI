using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FInalPanel : MonoBehaviour
{
    public GameObject finalPanel;
    public Text textFruit;
    public int totalFruits;
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
        for (int i = 0; i <= GameManager.Instance.GetScoreFruit(); i++)
        {
            textFruit.text = i + " / " + totalFruits;
            yield return new WaitForSeconds((float)5 / (float)GameManager.Instance.GetScoreFruit());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SlimeCenter") || collision.gameObject.CompareTag("MochiSphere"))
            StartCoroutine(OpenfinalPanel());
    }
}
