using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    const string Skin = "Skin";
    const string Accessorie = "Accessorie";

    const int lvlsTotal = 1; 

    public Skin[] skins;
    private int wasSkin;

    public Accessorie[] accessories;
    private int wasAccessorie;

    void Awake()
    {
        wasSkin = Profil.ChoosenSkin;
        wasAccessorie = Profil.ChoosenAccessorie;
        try
        {
            for (int i = 0; i < Profil.SkinCatalog.Count; i++)
                skins[PlayerPrefs.GetInt(Skin + i)].GetThing();
            ChooseShopThing(Skin + Profil.ChoosenSkin);
        }
        catch (Exception) { }
        try
        {
            for (int i = 0; i < Profil.AccessoriesCatalog.Count; i++)
                accessories[PlayerPrefs.GetInt(Accessorie + i)].GetThing();
            ChooseShopThing(Accessorie + Profil.ChoosenAccessorie);
        }
        catch (Exception) { }
    }

    public void ReturnToMenuFromLevel()
    {
        Profil.lvl--;
        SceneManager.LoadScene(0);
    }
    public void gotoSkinShopFromAccessorieShop()
    {
        Profil.ChoosenAccessorie = wasAccessorie;
        SceneManager.LoadScene(1);
    }

    public void gotoAccessorieShopFromSkinShop()
    {
        Profil.ChoosenSkin = wasSkin;
        SceneManager.LoadScene(2);
    }
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void GoLevel() 
    {
        if (Profil.lvl <= lvlsTotal)
        {
            Profil.lvl++;
            ChangeScene(Profil.lvl + 3);
        }
        else {
            ChangeScene(0);
            Profil.lvl = 1;
        }
        PlayerPrefs.SetInt("Level", Profil.lvl);
    }
    public void ChooseShopThing(string ShopThingN)
    {
        IShopThing[] shopThings = null;
        int h = 0;
        if (ShopThingN.StartsWith(Skin))
        {
            shopThings = skins;
            ShopThingN = ShopThingN.Replace(Skin, "");
            Profil.ChoosenSkin = Convert.ToInt32(ShopThingN);
            h = Profil.ChoosenSkin;
        }
        else if (ShopThingN.StartsWith(Accessorie))
        {
            shopThings = accessories;
            ShopThingN = ShopThingN.Replace(Accessorie, "");
            Profil.ChoosenAccessorie = Convert.ToInt32(ShopThingN);
            h = Profil.ChoosenAccessorie;
        }
        try
        {
            shopThings[h].Info.transform.GetChild(0).GetComponent<Text>().text = shopThings[h].Name;
            if(shopThings[h].Bought)
                shopThings[h].Info.transform.GetChild(1).GetComponent<Text>().text = shopThings[h].Text;
            else
                shopThings[h].Info.transform.GetChild(1).GetComponent<Text>().text = "Цена - " + shopThings[h].Price + " рубинов.";
            shopThings[h].Info.transform.GetChild(2).GetComponent<Image>().sprite = shopThings[h].Skinn;
            if (shopThings[h].Bought)
                shopThings[h].BuyButton.GetComponentInChildren<Text>().text = "Выбрать";
            else
                shopThings[h].BuyButton.GetComponentInChildren<Text>().text = "Купить";
        }
        catch (NullReferenceException)
        {

        }
    }

    public void KeepShopThing(string ShopThing)
    {
        IShopThing[] shopThings = null;
        List<int> shopThingsCatalog = null;
        int h = 0;
        string typeOfShopThing = "";
        try
        {
            if (ShopThing == Skin)
            {
                shopThings = skins;
                h = Profil.ChoosenSkin;
                typeOfShopThing = Skin;
                shopThingsCatalog = Profil.SkinCatalog;
            }
            else if (ShopThing == Accessorie)
            {
                shopThings = accessories;
                h = Profil.ChoosenAccessorie;
                typeOfShopThing = Accessorie;
                shopThingsCatalog = Profil.AccessoriesCatalog;
            }

            if (!shopThings[h].Bought && Profil.Money >= shopThings[h].Price)
            {
                shopThings[h].GetThing();
                Profil.Money -= shopThings[h].Price;
                PlayerPrefs.SetInt(typeOfShopThing + h, h);
                shopThingsCatalog.Add(PlayerPrefs.GetInt(typeOfShopThing + h));
                PlayerPrefs.SetInt("Money", Profil.Money);
            }
            else if (!shopThings[h].Bought && Profil.Money < shopThings[h].Price)
                return;
            else
            {
                PlayerPrefs.SetInt("Choosen" + typeOfShopThing, h);
                ChangeScene(0);
            }
        }
        catch (NullReferenceException)
        {

        }
    }
}
