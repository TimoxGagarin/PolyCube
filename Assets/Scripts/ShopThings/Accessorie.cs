using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accessorie : MonoBehaviour, IShopThing
{
    public bool Bought { get; set; }
    public int Price { get; set; }
    public Sprite Skinn { get; set; }
    public GameObject Info { get; set; }
    public Button BuyButton { get; set; }
    public string Text { get; set; }
    public string Name { get; set; }

    public bool bought;
    public int price;
    public Sprite skin;
    public GameObject info;
    public Button buyButton;
    public string text;
    public string name;

    void Start()
    {
        Updatee();
    }
    void Updatee()
    {
        Bought = bought;
        Price = price;
        Skinn = skin;
        Info = info;
        BuyButton = buyButton;
        Text = text;
        Name = name;
    }
    public void GetThing()
    {
        bought = true;
        GetComponent<Image>().color = Color.white;
        buyButton.GetComponentInChildren<Text>().text = "Выбрать";
        info.transform.GetChild(1).GetComponent<Text>().text = Text;
        Updatee();
    }
}