using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlowTime : MonoBehaviour
{

    public Sprite noTimeSlowBtn, timeSlowBtn;
    private Image image;
    private int timeSlowCounter;
    public Text timeSlowCounterTxt;
    private float originalFixedDeltaTime;
    public AudioSource audioSource;

    void Start()
    {
        originalFixedDeltaTime = Time.fixedDeltaTime;
        image = GetComponent<Image>();


        timeSlowCounter = PlayerPrefs.GetInt("Time Slow", 3);
        UpdateUI();
    }

    void UpdateUI()
    {
   
        timeSlowCounterTxt.text = timeSlowCounter.ToString();
        image.sprite = (timeSlowCounter == 0) ? noTimeSlowBtn : timeSlowBtn;
    }

    IEnumerator TimeSlow()
    {
        if (timeSlowCounter > 0)
        {

            audioSource.Play();

    
            timeSlowCounter--;
            PlayerPrefs.SetInt("Time Slow", timeSlowCounter);
            PlayerPrefs.Save(); 

            UpdateUI();

         
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;


            yield return new WaitForSecondsRealtime(3f);

            Time.timeScale = 1f;
            Time.fixedDeltaTime = originalFixedDeltaTime;

            
        }
    }

    public void StartTimeSlow()
    {
        StartCoroutine(TimeSlow());
    }


}
