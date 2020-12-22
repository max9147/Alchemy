using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public GameObject moneySystem;

    public Button[] buyResX1;
    public Button[] buyResX5;
    public Button[] buyResX10;
    public Button buyBottle;
    public Button buyFuel;
    public Button buyCauldron;

    private void Update()
    {
        if (moneySystem.GetComponent<MoneySystem>().GetMoney() < 100)
        {
            foreach (var item in buyResX1)
                item.interactable = false;
        }
        else
        {
            foreach (var item in buyResX1)
                item.interactable = true;
        }

        if (moneySystem.GetComponent<MoneySystem>().GetMoney() < 500)
        {
            foreach (var item in buyResX5)
                item.interactable = false;
        }
        else
        {
            foreach (var item in buyResX5)
                item.interactable = true;
        }

        if (moneySystem.GetComponent<MoneySystem>().GetMoney() < 1000)
        {
            foreach (var item in buyResX10)
                item.interactable = false;
        }
        else
        {
            foreach (var item in buyResX10)
                item.interactable = true;
        }

        if (moneySystem.GetComponent<MoneySystem>().GetMoney() < 150)
            buyFuel.interactable = false;
        else
            buyFuel.interactable = true;

        if (moneySystem.GetComponent<MoneySystem>().GetMoney() < moneySystem.GetComponent<ShopSystem>().bottleCost)
            buyBottle.interactable = false;
        else
            buyBottle.interactable = true;

        if (moneySystem.GetComponent<MoneySystem>().GetMoney() < moneySystem.GetComponent<ShopSystem>().cauldronCost)
            buyCauldron.interactable = false;
        else
            buyCauldron.interactable = true;
    }
}