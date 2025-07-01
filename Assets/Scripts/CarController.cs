using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour
{
    public AudioClip crash;
    public AudioClip[] accelerates;
    private Rigidbody carRb;
    public float speed = 15f, rotateMultRight = 6f, rotateMultLeft = 5f, force = 50f;
    public bool rightTurn, leftTurn, moveFromUp;
    private float originRotationY;
    private Camera mainCam;
    public LayerMask carsLayer;
    private bool isMovingFast, carCrashed;
    [NonSerialized] public bool carPassed;
    [NonSerialized] public static bool isLose;
    public GameObject turnLeftSignal, turnRightSignal, explosion, exhaust;
    [NonSerialized] public static int countCars;

    void Start()
    {
        mainCam = Camera.main;
        originRotationY = transform.eulerAngles.y;
        carRb = GetComponent<Rigidbody>();

        if (rightTurn)
            StartCoroutine(TurnSignals(turnRightSignal));
        else if (leftTurn)
            StartCoroutine(TurnSignals(turnLeftSignal));
    }

    IEnumerator TurnSignals(GameObject turnSignal)
    {
        while (!carPassed)
        {
            turnSignal.SetActive(!turnSignal.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void FixedUpdate()
    {
        carRb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            HandleInput(ray);
        }
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.GetTouch(0).position);
            HandleInput(ray);
        }
#endif
    }

    void HandleInput(Ray ray)
    {
        if (isMovingFast) return;

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, carsLayer))
        {
            if (hit.transform.gameObject.name == gameObject.name)
            {
                GameObject vfx = Instantiate(exhaust, 
                    new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), 
                    Quaternion.Euler(90, 0, 0));
                Destroy(vfx, 2f);
                speed *= 2f;
                isMovingFast = true;

                if (PlayerPrefs.GetString("Music") != "No")
                {
                    GetComponent<AudioSource>().clip = accelerates[Random.Range(0, accelerates.Length)];
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") && other.GetComponent<CarController>().carPassed)
        {
            other.GetComponent<CarController>().speed = speed + 5f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car") && !carCrashed)
        {
            isLose = true;
            carCrashed = true;
            speed = 0f;
            collision.gameObject.GetComponent<CarController>().speed = 0f;

            GameObject vfx = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(vfx, 5f);

            if (isMovingFast)
                force *= 1.5f;
            carRb.AddRelativeForce(Vector3.back * force);

            if (PlayerPrefs.GetString("Music") != "No")
            {
                GetComponent<AudioSource>().clip = crash;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (carCrashed) return;

        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
        {
            RotateCar(rotateMultRight);
        }
        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
        {
            RotateCar(rotateMultLeft, -1);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (carCrashed) return;

        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
        {
            carRb.rotation = Quaternion.Euler(0, originRotationY + 90, 0);
        }

        if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
        {
            carRb.rotation = Quaternion.Euler(0, originRotationY - 90, 0);
        }

        if (other.transform.CompareTag("Delete Trigger"))
        {
            Destroy(gameObject);
        }

        if (other.transform.CompareTag("Pass"))
        {
            if (carPassed) return;
            countCars++;
            carPassed = true;
            Collider[] colliders = GetComponents<BoxCollider>();
            foreach (Collider col in colliders)
            {
                col.enabled = true;
            }
        }
    }

    void RotateCar(float speedRotate, int dir = 1)
    {
        if (carCrashed) return;

        if (dir == -1 && transform.localRotation.eulerAngles.y < originRotationY - 90f)
            return;
            
        if (dir == -1 && moveFromUp && transform.localRotation.eulerAngles.y > 250f && transform.localRotation.eulerAngles.y < 270f)
            return;

        float rotateSpeed = speed * speedRotate * dir;
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
        carRb.MoveRotation(carRb.rotation * deltaRotation);
    }
}