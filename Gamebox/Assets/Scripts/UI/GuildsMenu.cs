using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuildsMenu : MonoBehaviour
{
    public GameObject guildSystem;
    public GameObject moneySystem;
    public GameObject resourceSystem;

    public Slider repWarriors;
    public Slider repBandits;
    public Slider repPriests;
    public Slider repMagicians;
    public Slider repWarriorsGuilds;
    public Slider repBanditsGuilds;
    public Slider repPriestsGuilds;
    public Slider repMagiciansGuilds;

    public Button buyLadan;
    public Button buyEye;
    public Button buySand;
    public Button buyStone;

    public Button buyRepWarriors;
    public Button buyRepBandits;
    public Button buyRepPriests;
    public Button buyRepMagicians;

    private void OnEnable()
    {
        repWarriorsGuilds.value = repWarriors.value;
        repBanditsGuilds.value = repBandits.value;
        repPriestsGuilds.value = repPriests.value;
        repMagiciansGuilds.value = repMagicians.value;

        if (repWarriors.value >= 70 && moneySystem.GetComponent<MoneySystem>().GetMoney() > 300 && resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Eye) < 2)
            buyEye.interactable = true;
        else
            buyEye.interactable = false;

        if (repBandits.value >= 70 && moneySystem.GetComponent<MoneySystem>().GetMoney() > 300 && resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Sand) < 2)
            buySand.interactable = true;
        else
            buySand.interactable = false;

        if (repPriests.value >= 70 && moneySystem.GetComponent<MoneySystem>().GetMoney() > 300 && resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Ladan) < 2)
            buyLadan.interactable = true;
        else
            buyLadan.interactable = false;

        if (repMagicians.value >= 70 && moneySystem.GetComponent<MoneySystem>().GetMoney() > 300 && resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Stone) < 2)
            buyStone.interactable = true;
        else
            buyStone.interactable = false;
    }

    public void BuyRep(int guildID)
    {
        if (moneySystem.GetComponent<MoneySystem>().GetMoney() >= 500)
        {
            moneySystem.GetComponent<MoneySystem>().SpendMoney(500);

            switch (guildID)
            {
                case 0:
                    guildSystem.GetComponent<GuildSystem>().addRep(Guild.Warriors, 5);
                    repWarriorsGuilds.value = repWarriors.value;
                    if (repWarriors.value >= 25)
                        buyRepWarriors.interactable = false;
                    break;

                case 1:
                    guildSystem.GetComponent<GuildSystem>().addRep(Guild.Bandits, 5);
                    repBanditsGuilds.value = repBandits.value;
                    if (repBandits.value >= 25)
                        buyRepBandits.interactable = false;
                    break;

                case 2:
                    guildSystem.GetComponent<GuildSystem>().addRep(Guild.Priests, 5);
                    repPriestsGuilds.value = repPriests.value;
                    if (repPriests.value >= 25)
                        buyRepPriests.interactable = false;
                    break;

                case 3:
                    guildSystem.GetComponent<GuildSystem>().addRep(Guild.Magicians, 5);
                    repMagiciansGuilds.value = repMagicians.value;
                    if (repMagicians.value >= 25)
                        buyRepMagicians.interactable = false;
                    break;

                default:
                    break;
            }
        }
    }

    public void BuyRareResource(int resID)
    {
        if (moneySystem.GetComponent<MoneySystem>().GetMoney() >= 300)
        {
            switch (resID)
            {
                case 0:
                    if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Eye) < 2)
                    {
                        moneySystem.GetComponent<MoneySystem>().SpendMoney(300);
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Eye, 1);
                        if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Eye) == 2)
                            buyEye.interactable = false;
                    }
                    break;

                case 1:
                    if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Sand) < 2)
                    {
                        moneySystem.GetComponent<MoneySystem>().SpendMoney(300);
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Sand, 1);
                        if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Sand) == 2)
                            buySand.interactable = false;
                    }
                    break;

                case 2:
                    if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Ladan) < 2)
                    {
                        moneySystem.GetComponent<MoneySystem>().SpendMoney(300);
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Ladan, 1);
                        if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Ladan) == 2)
                            buyLadan.interactable = false;
                    }
                    break;

                case 3:
                    if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Stone) < 2)
                    {
                        moneySystem.GetComponent<MoneySystem>().SpendMoney(300);
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Stone, 1);
                        if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Stone) == 2)
                            buyStone.interactable = false;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}