using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports;


public class AsteroidSpawn : MonoBehaviour
{
    //SerialPort puerto1 = new SerialPort("COM5", 9600); //display
    //SerialPort puerto2 = new SerialPort("COM6", 9600); //demás

    [SerializeField] private GameObject VictoryPanel;

    SerialPort puertoDisplay;

    //public PlayerMov playerM;

    public GameObject[] enemies;

    public float timeSpawn = 1;
    public float repeatSpawn = 3;

    public Transform XRangeLeft;
    public Transform XRangeRight;

    public Transform YRangeUp;
    public Transform YRangeDown;

    public float difficultyTime;

    void Awake()
    {
        //puerto2.ReadTimeout = 140;
        //puerto2.Open();
        
        StartCoroutine(Difficulty());
    }

    void Start()
    {
        //puertoDisplay = FindObjectOfType<PlayerMov>().puerto1;

        //puertoDisplay.ReadTimeout = 40;
        //puertoDisplay.Open();

        //playerM = GetComponent<PlayerMov>();

        VictoryPanel.SetActive(false);
    }

    private void Update()
    {
        difficultyTime += Time.deltaTime;

        if (difficultyTime > 5 && difficultyTime < 20)
            repeatSpawn = 2;

        if (difficultyTime > 20 && difficultyTime < 30)
            repeatSpawn = 1;

        if (difficultyTime > 30 && difficultyTime < 50)
            repeatSpawn = 0.50f;

        if (difficultyTime > 50 && difficultyTime < 60)
            repeatSpawn = 0.30f;

        if (difficultyTime > 60 && difficultyTime < 70)
            repeatSpawn = 0.20f;
        
        if (difficultyTime >= 80)
        {
            Time.timeScale = 0;
            //playerM.Arduino("victory");
            VictoryPanel.SetActive(true);
        }

    }

    IEnumerator Difficulty()
    {
        yield return new WaitForSeconds(repeatSpawn);
        SpawnEnemies();
        if (difficultyTime < 75)
            StartCoroutine("Difficulty");
    }

    public void SpawnEnemies()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);

        spawnPosition = new Vector3(Random.Range(XRangeLeft.position.x, XRangeRight.position.x), Random.Range(YRangeDown.position.y, YRangeUp.position.y), 0);

        GameObject enemie = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPosition, gameObject.transform.rotation);
    }
}