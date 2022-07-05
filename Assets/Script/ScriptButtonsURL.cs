using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptButtonsURL : MonoBehaviour
{
    public string url;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenPage);
    }

    public void OpenPage()
    {
        Application.OpenURL(url);
    }
}
