using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public GameObject applePickUp, bombPickUp;

    private float minX = 585.73f, maxX = 580.11f , minZ = 65.74f, maxZ = 77.59f;
    private float yPos = 0.113f;

    private Text scoreTxt;
    private int scoreCount;

    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        scoreTxt = GameObject.Find("Score").GetComponent<Text>();

        Invoke("StartSpawning", 0.5f);
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void StartSpawning()
    {
        StartCoroutine(SpawnPickUps());
    }

    public void CancelSpawning()
    {
        CancelInvoke("StartSpawning");
    }

    IEnumerator SpawnPickUps()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));

        if(Random.Range(0,10)>=2)
        {
            Instantiate(applePickUp, new Vector3(Random.Range(minX, maxX), yPos, Random.Range(minZ, maxZ)),Quaternion.identity);
        }
        else
        {
            Instantiate(bombPickUp, new Vector3(Random.Range(minX, maxX), yPos, Random.Range(minZ, maxZ)), Quaternion.identity);
        }
        Invoke("StartSpawning", 0f);
    }

    public void IncreaseScore()
    {
        scoreCount++;
        scoreTxt.text = "Score: " + scoreCount;
    }
}
