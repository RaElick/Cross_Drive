using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class CanvasButtons : MonoBehaviour
{

    public Sprite btn, btnPressed, musicOn, musicOff;
    private Image image;


    void Start()
    {
        image = GetComponent<Image>();
        if (gameObject.name == "MusicBtn")
        {
            if (PlayerPrefs.GetString("Music") == "No")
            {
                transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
            }
        }
    }

    public void ShopScene()
    {
        StartCoroutine(LoadScene("Shop"));
        PlayBtnSound();
    }

    public void ExitShopScene()
    {
        StartCoroutine(LoadScene("Main"));
        PlayBtnSound();
    }


    public void PlayGame()
    {



        if (PlayerPrefs.GetString("First Game") == "No")
        {
            StartCoroutine(LoadScene("Game"));
        }
        else
            StartCoroutine(LoadScene("Study"));

        PlayBtnSound();


    }

    public void RestartGame()
    {
        StartCoroutine(LoadScene("Game"));
        PlayBtnSound();
    }


    public void SetPressedButton()
    {
        image.sprite = btnPressed;
        transform.GetChild(0).localPosition -= new Vector3(0, 5f, 0);
    }


    public void SetDefaultButton()
    {
        image.sprite = btn;
        transform.GetChild(0).localPosition += new Vector3(0, 5f, 0);
    }

    IEnumerator LoadScene(string name)
    {

        float fadeTime = Camera.main.GetComponent<Fading>().Fade(1f);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
        PlayBtnSound();
    }

    public void MusicBtn()
    {
        if (PlayerPrefs.GetString("Music") == "No") // turn on
        {
            PlayerPrefs.SetString("Music", "Yes");
            transform.GetChild(0).GetComponent<Image>().sprite = musicOn;
        }
        else // turn of
        {
            PlayerPrefs.SetString("Music", "No");
            transform.GetChild(0).GetComponent<Image>().sprite = musicOff;
        }
        PlayBtnSound();
    }

    private void PlayBtnSound()
    {
        if (PlayerPrefs.GetString("Music") != "No")
        {
            GetComponent<AudioSource>().Play();
        }
    }











}
 
