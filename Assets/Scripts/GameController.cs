using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameController : MonoBehaviour
{

    public GameObject[] cars;
    public GameObject canvasLosePanel;
    public float timeToSpawnFrom = 2f, timeToSpawnTo = 4.5f;
    public bool isMainScene;
    private int countCars;
    private Coroutine bottomCars, leftCars, rightCars, upCars;
    private bool isLoseOnce;
    public Text nowScore, topScore, coinsCount;
    public GameObject[] maps;
    public GameObject horn;
    public AudioSource turnSignal;



    void Start()
    {

        if (PlayerPrefs.GetInt("NowMap") == 2)
        {
            Destroy(maps[0]);
            maps[1].SetActive(true);
            Destroy(maps[2]);
        }
        else if (PlayerPrefs.GetInt("NowMap") == 3)
        {
            Destroy(maps[0]);
            Destroy(maps[1]);
            maps[2].SetActive(true);
        }
        else
        {
            maps[0].SetActive(true);
            Destroy(maps[1]);
            Destroy(maps[2]);
        }


        CarController.isLose = false;
        CarController.countCars = 0;

        if (isMainScene)
        {
            timeToSpawnFrom = 4f;
            timeToSpawnTo = 6f;
        }

        bottomCars = StartCoroutine(BottomCars());
        leftCars = StartCoroutine(LeftCars());
        rightCars = StartCoroutine(RightCars());
        upCars = StartCoroutine(UpCars());

        StartCoroutine(CreateHorn());

    }

    private void Update()
    {
        if (CarController.isLose && !isLoseOnce)
        {
            StopCoroutine(bottomCars);
            StopCoroutine(leftCars);
            StopCoroutine(rightCars);
            StopCoroutine(upCars);
            isLoseOnce = true;

            nowScore.text = "<color=#D24545>SCORE:</color>" + CarController.countCars.ToString();
            if (PlayerPrefs.GetInt("Score") < CarController.countCars)
                PlayerPrefs.SetInt("Score", CarController.countCars);

            topScore.text = "<color=#D24545>TOP:</color>" + PlayerPrefs.GetInt("Score").ToString();

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CarController.countCars);
            coinsCount.text = PlayerPrefs.GetInt("Coins").ToString();

            canvasLosePanel.SetActive(true);
        }


    }


    IEnumerator BottomCars()
    {
        while (true)
        {
            SpawnCars(new Vector3(-1.19f, -0.02063537f, -15.4f), 180f);
            float timeToSpawn = UnityEngine.Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    IEnumerator LeftCars()
    {
        while (true)
        {
            SpawnCars(new Vector3(-73.7f, -0.02063537f, 5.6f), 270f);
            float timeToSpawn = UnityEngine.Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    IEnumerator RightCars()
    {
        while (true)
        {
            SpawnCars(new Vector3(20.6f, -0.02063537f, 13.05f), 90f);
            float timeToSpawn = UnityEngine.Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    IEnumerator UpCars()
    {
        while (true)
        {
            SpawnCars(new Vector3(-8.08f, -0.02063537f, 74.6f), 0f, true);
            float timeToSpawn = UnityEngine.Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }



    void SpawnCars(Vector3 pos, float rotY, bool isMoveFromUp = false)
    {
        GameObject newObj = Instantiate(cars[UnityEngine.Random.Range(0, cars.Length)], pos, Quaternion.Euler(0, rotY, 0)) as GameObject;
        newObj.name = "Car - " + ++countCars;

        int random = isMainScene ? 1 : UnityEngine.Random.Range(1, 4);
        if (isMainScene)
            newObj.GetComponent<CarController>().speed = 10f;
        switch (random)
        {
            case 1:
                // Move right
                newObj.GetComponent<CarController>().rightTurn = true;
                if (PlayerPrefs.GetString("Music") != "No" && !turnSignal.isPlaying)
                {
                    turnSignal.Play();
                    Invoke("StopSound", 2f);
                }
                break;

            case 2:
                // Move left
                newObj.GetComponent<CarController>().leftTurn = true;
                if (PlayerPrefs.GetString("Music") != "No" && !turnSignal.isPlaying)
                {
                    turnSignal.Play();
                    Invoke("StopSound", 2f);
                }
                if (isMoveFromUp)
                    newObj.GetComponent<CarController>().moveFromUp = true;
                break;

            case 3:
                // Move forward
                break;
        }

    }

    void StopSound()
    {
        turnSignal.Stop();
    }

    IEnumerator CreateHorn()
    {


        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 9));
            if (PlayerPrefs.GetString("Music") != "No")
                Instantiate(horn, Vector3.zero, Quaternion.identity);

        }
        
    }

}
