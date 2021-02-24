using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyTextShop;
    public TextMeshProUGUI moneyTextGuilds;

    public GameObject UIControls;

    public int money = 99999;

    private void Start()
    {
        moneyTextShop.text = "Деньги: " + money.ToString();
        moneyTextGuilds.text = "Деньги: " + money.ToString();
        moneyText.text = money.ToString();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyTextShop.text = "Деньги: " + money.ToString();
        moneyTextGuilds.text = "Деньги: " + money.ToString();
        moneyText.text = money.ToString();
        if (money > 4000)
        {
            UIControls.GetComponent<Tutorial>().helpStep = 3;
            UIControls.GetComponent<Tutorial>().GetHelp();
        }
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
        moneyTextShop.text = "Деньги: " + money.ToString();
        moneyTextGuilds.text = "Деньги: " + money.ToString();
        moneyText.text = money.ToString();
    }

    public int GetMoney()
    {
        return money;
    }
}