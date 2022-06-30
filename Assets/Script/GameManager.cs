using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI scoreFruitText;
    int scoreFruit;
    AudioSource audioS;
    public AudioClip appleClip, melonClip;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);    //  Si al inicio del script tiene no ningun valor o si ya lo tiene, lo destruye
        else Instance = this;   //  Le a?ade el valor de este propio script
        audioS = GetComponent<AudioSource>();
    }

    public int GetScoreFruit()
    {
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
}
