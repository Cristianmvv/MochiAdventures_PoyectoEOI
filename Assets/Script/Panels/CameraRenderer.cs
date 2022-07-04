using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRenderer : MonoBehaviour
{
    Canvas canvas;

    public Image[] estrellitas;

    int totalFrutas;
    int frutasRecogidas;
    float porcentajeFrutasRecogidas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();

        canvas.worldCamera = Camera.main;

        totalFrutas = GameObject.FindGameObjectsWithTag("Pickups/Fruit").Length + (GameObject.FindGameObjectsWithTag("Pickups/FruitX5").Length * 5);
        frutasRecogidas = GameManager.Instance.GetScoreFruit();
        porcentajeFrutasRecogidas = frutasRecogidas / totalFrutas;
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator ActivarEstrellitas()
    {
        if (porcentajeFrutasRecogidas > 0.66f)
        {
            for (int i = 0; i < estrellitas.Length; i++)
            {
                while (estrellitas[i].fillAmount<1)
                {
                    estrellitas[i].fillAmount += 0.01f;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }else if (porcentajeFrutasRecogidas > 0.33f)
        {
            for (int i = 0; i < 2; i++)
            {
                while (estrellitas[i].fillAmount < 1)
                {
                    estrellitas[i].fillAmount += 0.01f;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        else
        {
            while (estrellitas[0].fillAmount < 1)
            {
                estrellitas[0].fillAmount += 0.01f;
                yield return new WaitForSeconds(0.1f);
            }
        }
        
    }
}
