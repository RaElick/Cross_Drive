
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject canvasFirst, canvasSecond, secondCar;
    private bool isFirst;
    private CarController carController;

    void Start()
    {
        carController = GetComponent<CarController>();
    }


    void Update()
    {
        if (transform.position.x < 8 && !isFirst)
        {
            carController.speed = 0;
            canvasFirst.SetActive(true);
            isFirst = true;

        }
    }

    void OnMouseDown()
    {
        if (!isFirst || transform.position.x > 9f)
            return;

        carController.speed = 15f;
        canvasFirst.SetActive(false);
        canvasSecond.SetActive(true);
        secondCar.GetComponent<CarController>().speed = 12f;
    }






}
