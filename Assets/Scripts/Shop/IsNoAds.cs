using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsNoAds : MonoBehaviour
{
    void Awake()
    {
        if (PlayerPrefs.GetString("NoAds") == "Yes")
        {
            Destroy(gameObject);
        }
    }
}
