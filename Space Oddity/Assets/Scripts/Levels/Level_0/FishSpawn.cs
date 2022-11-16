using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FishSpawn : MonoBehaviour
{

    public GameObject[] fish;
    [SerializeField] private GameObject PanelVictory;


    public float timeSpawn = 1;
    public float repeatSpawn = 3;


    public Transform XRangeLeft;
    public Transform XRangeRight;

    public Transform YRangeUp;
    public Transform YRangeDown;

    public float difficultyTime;

    void Start()
    {
        StartCoroutine("Difficulty");
        PanelVictory.SetActive(true);
    }

    private void Update()
    {
        difficultyTime += Time.deltaTime;

        if (difficultyTime > 5 && difficultyTime < 20)
            repeatSpawn = 2;

        if (difficultyTime > 30 && difficultyTime < 50)
            repeatSpawn = 1;

        if (difficultyTime > 50 && difficultyTime < 60)
            repeatSpawn = 0.50f;

        if (difficultyTime > 60 && difficultyTime < 70)
            repeatSpawn = 0.30f;

        if (difficultyTime >= 80)
        {
            Time.timeScale = 0;
            // pasa escena
            PanelVictory.SetActive(true);
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

        GameObject fishes = Instantiate(fish[Random.Range(0, fish.Length)], spawnPosition, gameObject.transform.rotation);
    }
}