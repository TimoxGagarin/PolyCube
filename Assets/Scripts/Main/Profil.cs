using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profil : MonoBehaviour{
    public static int ChoosenSkin;
    public Image placeForSkin;
    public static List<int> SkinCatalog;

    public static int ChoosenAccessorie;
    public Image placeForAccessorie;
    public static List<int> AccessoriesCatalog;

    public Text text;
    public static int Money;

    public static int lvl = 1;
    void Awake(){
        SkinCatalog = new List<int>() { PlayerPrefs.GetInt("Skin0"), PlayerPrefs.GetInt("Skin1"), PlayerPrefs.GetInt("Skin2"), PlayerPrefs.GetInt("Skin3"), PlayerPrefs.GetInt("Skin4"), PlayerPrefs.GetInt("Skin5"), PlayerPrefs.GetInt("Skin6") };
        AccessoriesCatalog = new List<int>() { PlayerPrefs.GetInt("Accessorie0"), PlayerPrefs.GetInt("Accessorie1"), PlayerPrefs.GetInt("Accessorie2"), PlayerPrefs.GetInt("Accessorie3") };
        ChoosenSkin = PlayerPrefs.GetInt("ChoosenSkin");
        ChoosenAccessorie = PlayerPrefs.GetInt("ChoosenAccessorie");
        SetSkin();
        SetAccessorie();
        Money = PlayerPrefs.GetInt("Money");
        lvl = PlayerPrefs.GetInt("Lvl");
    }
    private void Update()
    {
        MoneyLoad();
    }
    public void MoneyLoad()
    {
        try
        {
            text.text = Money.ToString();
        }
        catch (NullReferenceException)
        {
            text.text = "0";
        }
    }
    public void SetSkin()
    {
        try
        {
            placeForSkin.sprite = GetComponent<ButtonController>().skins[ChoosenSkin].skin;
        }
        catch (NullReferenceException){}
    }
    public void SetAccessorie()
    {
        try
        {
            placeForAccessorie.sprite = GetComponent<ButtonController>().accessories[ChoosenAccessorie].skin;
        }
        catch (NullReferenceException) { }
    }
}
