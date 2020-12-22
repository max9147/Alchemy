using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestComplete : MonoBehaviour
{
    public GameObject guildSystem;
    public GameObject potionSystem;
    public GameObject moneySystem;
    public PotionColor potionColor;
    public PotionEffect potionEffect;
    public bool haveQuest = false;
    public Sprite bottleEmpty;

    private int reward;

    public void NewQuest(PotionColor _potionColor, PotionEffect _potionEffect, int _reward)
    {
        potionColor = _potionColor;
        potionEffect = _potionEffect;
        haveQuest = true;
        reward = _reward;
    }

    public void EndQuest()
    {
        haveQuest = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bottle") && haveQuest)
        {
            if (collision.GetComponent<BottlePotion>().potionColor == potionColor && collision.GetComponent<BottlePotion>().potionEffect == potionEffect) 
            {
                moneySystem.GetComponent<MoneySystem>().AddMoney(reward);

                if (guildSystem.GetComponent<QuestsSystem>().questTime > 10)
                    guildSystem.GetComponent<QuestsSystem>().questTime--;

                collision.GetComponent<SpriteRenderer>().sprite = bottleEmpty;
                collision.GetComponent<SpriteRenderer>().color = Color.white;
                collision.GetComponent<BottlePotion>().potionColor = PotionColor.Empty;
                collision.GetComponent<BottlePotion>().potionEffect = PotionEffect.Empty;

                switch (collision.tag)
                {
                    case "Bottle1":
                        potionSystem.GetComponent<PotionSystem>().SetColor(0, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(0, PotionEffect.Empty);
                        break;

                    case "Bottle2":
                        potionSystem.GetComponent<PotionSystem>().SetColor(1, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(1, PotionEffect.Empty);
                        break;

                    case "Bottle3":
                        potionSystem.GetComponent<PotionSystem>().SetColor(2, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(2, PotionEffect.Empty);
                        break;

                    case "Bottle4":
                        potionSystem.GetComponent<PotionSystem>().SetColor(3, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(3, PotionEffect.Empty);
                        break;

                    case "Bottle5":
                        potionSystem.GetComponent<PotionSystem>().SetColor(4, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(4, PotionEffect.Empty);
                        break;

                    case "Bottle6":
                        potionSystem.GetComponent<PotionSystem>().SetColor(5, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(5, PotionEffect.Empty);
                        break;

                    case "Bottle7":
                        potionSystem.GetComponent<PotionSystem>().SetColor(6, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(6, PotionEffect.Empty);
                        break;

                    case "Bottle8":
                        potionSystem.GetComponent<PotionSystem>().SetColor(7, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(7, PotionEffect.Empty);
                        break;

                    default:
                        break;
                }

                haveQuest = false;
                switch (tag)
                {
                    case "Warrior":
                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Warriors);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Warriors, 15);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Priests, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Bandits, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Magicians, 5);
                        break;

                    case "Bandit":
                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Bandits);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Bandits, 15);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Magicians, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Warriors, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Priests, 5);
                        break;

                    case "Priest":
                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Priests);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Priests, 15);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Warriors, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Magicians, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Bandits, 5);
                        break;

                    case "Magician":
                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Magicians);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Magicians, 15);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Bandits, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Priests, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Warriors, 5);
                        break;

                    default:
                        break;
                }                
            }
        }
    }
}