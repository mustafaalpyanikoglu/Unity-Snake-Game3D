using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playButon : MonoBehaviour
{

    public UnityEngine.UI.Button buton;

    void Awake()
    {

        buton.gameObject.SetActive(false);
    }

    void Update()
    {
        enabledButon();
    }

    public void enabledButon()
    {
        if(Time.timeScale == 0f)
        {
            buton.gameObject.SetActive(true);
        }
    }

    public void playAgain()
    {
        SceneManager.LoadScene("login");
    }
}
