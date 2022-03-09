using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public PlayerDirection direction;

    [HideInInspector]
    public float stepLength = 0.2f;

    public float movementFrequency = 0.1f;

    private float counter;
    private bool move;

    [SerializeField]
    private GameObject tailPrefab;

    private List<Vector3> deltaPosition; //Eski pozisyonu tutacak liste

    private List<Rigidbody> nodes;

    private Rigidbody mainBody;
    private Rigidbody headBody;
    private Transform tr;

    private bool createNodeAtTail;

    private void Awake()
    {
        Time.timeScale = 1f;
        tr = transform;
        mainBody = GetComponent<Rigidbody>();

        InitSnakeNodes();
        InitPlayer();

        deltaPosition = new List<Vector3>()
        {
            new Vector3(-stepLength,0f,0f),    //-x ..LEFT
            new Vector3(0f,0f,stepLength),     //z ..UP
            new Vector3(stepLength,0f,0f),     //x ..RIGHT
            new Vector3(0f,0f,-stepLength)     //-z ..DOWN

        };
    }

    private void Update()
    {
        CheckMovementFrequency();
    }

    private void FixedUpdate()
    {
        if(move)
        {
            move = false;
            Move();
        }
    }

    void InitSnakeNodes()
    {
        nodes = new List<Rigidbody>();
        nodes.Add(tr.GetChild(0).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(1).GetComponent<Rigidbody>());
        nodes.Add(tr.GetChild(2).GetComponent<Rigidbody>());

        headBody = nodes[0];
    }

    void SetDirectionRandom()
    {
        int dirRandom = Random.Range(0, (int)PlayerDirection.COUNT);
        direction = (PlayerDirection)dirRandom;
    }

    void InitPlayer()
    {
        SetDirectionRandom();

        switch(direction)
        {
            case PlayerDirection.RIGHT:
                nodes[1].position = nodes[0].position - new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position - new Vector3(Metrics.NODE*2f, 0f, 0f);
                break;
            case PlayerDirection.LEFT:
                nodes[1].position = nodes[0].position + new Vector3(Metrics.NODE, 0f, 0f);
                nodes[2].position = nodes[0].position + new Vector3(Metrics.NODE * 2f, 0f, 0f);
                break;
            case PlayerDirection.UP:
                nodes[1].position = nodes[0].position - new Vector3(0f,0f, Metrics.NODE);
                nodes[2].position = nodes[0].position - new Vector3(0f,0f, Metrics.NODE*2f);
                break;
            case PlayerDirection.DOWN:
                nodes[1].position = nodes[0].position + new Vector3(0f,0f, Metrics.NODE);
                nodes[2].position = nodes[0].position + new Vector3(0f,0f, Metrics.NODE * 2f);
                break;
        }
    }
    void Move()
    {
        Vector3 dPosition = deltaPosition[(int)direction];

        Vector3 parentPos = headBody.position;
        Vector3 prevPosition;

        mainBody.position = mainBody.position + dPosition;
        headBody.position = headBody.position + dPosition;

        for (int i = 1; i < nodes.Count; i++)
        {
            prevPosition = nodes[i].position;

            nodes[i].position = parentPos;
            parentPos = prevPosition;
        }

        if(createNodeAtTail)
        {
            createNodeAtTail = false;
            // sondaki eleman 1 birim geri gider onun yerini yenisi doldurur
            GameObject newNode = Instantiate(tailPrefab, nodes[nodes.Count - 1].position, Quaternion.identity);

            newNode.transform.SetParent(transform, true);
            nodes.Add(newNode.GetComponent<Rigidbody>());
        
        
        }
    }

    void CheckMovementFrequency()
    {
        counter += Time.deltaTime;
        if (counter >= movementFrequency)
        {
            counter = 0f;
            move = true;
        }
    }

    public void SetInputDirection(PlayerDirection dir)
    {
        if (dir == PlayerDirection.UP && direction==PlayerDirection.DOWN
            || dir ==PlayerDirection.DOWN && direction==PlayerDirection.UP
            || dir ==PlayerDirection.RIGHT && direction==PlayerDirection.LEFT
            || dir== PlayerDirection.LEFT && direction == PlayerDirection.RIGHT)
        {
            return;
        }
        direction = dir;

        ForceMove();
    }

    void ForceMove()
    {
        counter = 0;
        move = false;
        Move();
    }

    private void OnTriggerEnter(Collider other) //duvarla çarpışma kontrolü
    {
        if(other.tag== Tags.APPLE)
        {
            other.gameObject.SetActive(false);

            createNodeAtTail = true;

            GameplayController.instance.IncreaseScore();
            AudioManager.instance.Play_PickUpSound();
        }
        if(Tags.WALL== other.tag || other.tag==Tags.BOMB || other.tag==Tags.Tail)
        {
            Time.timeScale = 0f;

            AudioManager.instance.Play_DeadSound();
        }
    }
}
