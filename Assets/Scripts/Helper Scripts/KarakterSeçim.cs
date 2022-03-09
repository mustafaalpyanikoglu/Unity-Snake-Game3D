using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterSeçim : MonoBehaviour
{
    public GameObject turquoiseSnake;
    public GameObject graySnake;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void chooseTurquoiseSnake()
    {
        turquoiseSnake.SetActive(true);
        graySnake.SetActive(false);
    }

    public void chooseGraySnake()
    {
        turquoiseSnake.SetActive(false);
        graySnake.SetActive(true);
    }
}
