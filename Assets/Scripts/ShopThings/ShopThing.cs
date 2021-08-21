using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopThing : MonoBehaviour{}

public interface IShopThing
{
    bool Bought { get; set; }
    int Price { get; set; }
    Sprite Skinn { get; set; }
    GameObject Info { get; set; }
    Button BuyButton { get; set; }
    string Text { get; set; }
    string Name { get; set; }
    void GetThing();
}
