using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private Scene scene;
    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }
    public void startLevel()
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
    public void playAgain()
    {
        SceneManager.LoadScene("Login");
    }


}