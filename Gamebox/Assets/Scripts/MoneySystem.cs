using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public int money = 99999;

    private void Start()
    {
        moneyText.text = "Деньги: " + money.ToString();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = "Деньги: " + money.ToString();
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
        moneyText.text = "Деньги: " + money.ToString();
    }

    public int GetMoney()
    {
        return money;
    }
}