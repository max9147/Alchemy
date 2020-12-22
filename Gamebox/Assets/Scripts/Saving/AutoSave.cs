using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSave : MonoBehaviour
{
    public GameObject moneySystem;
    public GameObject bottles;
    public GameObject resourceSystem;
    public GameObject mixingSystem;
    public GameObject potionSystem;
    public GameObject guildSystem;

    private void Awake()
    {
        StartCoroutine(AutoSaveDelay());

        SaveData save = SaveGameSystem.LoadGame();
        if (save != null)
        {
            moneySystem.GetComponent<MoneySystem>().money = save.money;

            for (int i = 0; i < save.bottleCount - 2; i++)
                bottles.GetComponent<Bottles>().AddBottle();
            if (save.bottleCost != 0)
                moneySystem.GetComponent<ShopSystem>().bottleCost = save.bottleCost;

            resourceSystem.GetComponent<Fuel>().fuelCount = save.fuelCount;

            resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Red, save.amountRed);
            resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Blue, save.amountBlue);
            resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Yellow, save.amountYellow);
            resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.White, save.amountWhite);
            resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Ladan, save.amountLadan);
            resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Eye, save.amountEye);
            resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Stone, save.amountStone);
            resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Sand, save.amountSand);

            mixingSystem.GetComponent<MixingSystem>().ChangeCauldron(save.cauldronId);
            
            potionSystem.GetComponent<PotionSystem>().SetColor(0, (PotionColor)save.bottle1Color);
            potionSystem.GetComponent<PotionSystem>().SetColor(1, (PotionColor)save.bottle2Color);
            potionSystem.GetComponent<PotionSystem>().SetColor(2, (PotionColor)save.bottle3Color);
            potionSystem.GetComponent<PotionSystem>().SetColor(3, (PotionColor)save.bottle4Color);
            potionSystem.GetComponent<PotionSystem>().SetColor(4, (PotionColor)save.bottle5Color);
            potionSystem.GetComponent<PotionSystem>().SetColor(5, (PotionColor)save.bottle6Color);
            potionSystem.GetComponent<PotionSystem>().SetColor(6, (PotionColor)save.bottle7Color);
            potionSystem.GetComponent<PotionSystem>().SetColor(7, (PotionColor)save.bottle8Color);

            potionSystem.GetComponent<PotionSystem>().SetEffect(0, (PotionEffect)save.bottle1Effect);
            potionSystem.GetComponent<PotionSystem>().SetEffect(1, (PotionEffect)save.bottle2Effect);
            potionSystem.GetComponent<PotionSystem>().SetEffect(2, (PotionEffect)save.bottle3Effect);
            potionSystem.GetComponent<PotionSystem>().SetEffect(3, (PotionEffect)save.bottle4Effect);
            potionSystem.GetComponent<PotionSystem>().SetEffect(4, (PotionEffect)save.bottle5Effect);
            potionSystem.GetComponent<PotionSystem>().SetEffect(5, (PotionEffect)save.bottle6Effect);
            potionSystem.GetComponent<PotionSystem>().SetEffect(6, (PotionEffect)save.bottle7Effect);
            potionSystem.GetComponent<PotionSystem>().SetEffect(7, (PotionEffect)save.bottle8Effect);

            guildSystem.GetComponent<GuildSystem>().repWarriors = save.repWarriors;
            guildSystem.GetComponent<GuildSystem>().repBandits = save.repBandits;
            guildSystem.GetComponent<GuildSystem>().repPriests = save.repPriests;
            guildSystem.GetComponent<GuildSystem>().repMagicians = save.repMagicians;
        }
    }

    IEnumerator AutoSaveDelay()
    {
        yield return new WaitForSeconds(1);
        SaveGameSystem.SaveGame(moneySystem.GetComponent<MoneySystem>(), bottles.GetComponent<Bottles>(), moneySystem.GetComponent<ShopSystem>(), resourceSystem.GetComponent<Fuel>(), 
            resourceSystem.GetComponent<ResourceSystem>(), mixingSystem.GetComponent<MixingSystem>(), potionSystem.GetComponent<PotionSystem>(), guildSystem.GetComponent<GuildSystem>());
        StartCoroutine(AutoSaveDelay());
    }
}