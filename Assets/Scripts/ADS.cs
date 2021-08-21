using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class ADS : MonoBehaviour
{
    void Start()
    {
        if (Advertisement.isSupported)
            Advertisement.Initialize("3952179", false);
    }
    public void ADSButton()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video");
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 10);
            Profil.Money = PlayerPrefs.GetInt("Money");
        }
    }
}
