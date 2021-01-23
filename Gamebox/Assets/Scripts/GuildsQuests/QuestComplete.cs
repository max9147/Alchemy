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
    public Settings settings;

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

                if (guildSystem.GetComponent<QuestsSystem>().questTime > settings.questLimit && guildSystem.GetComponent<QuestsSystem>().questStep == settings.questSpeedupStep)
                {
                    guildSystem.GetComponent<QuestsSystem>().questTime -= settings.questSpeedup;
                    guildSystem.GetComponent<QuestsSystem>().questStep = 0;
                }

                guildSystem.GetComponent<QuestsSystem>().questStep++;

                collision.transform.Find("Water").GetComponent<SpriteRenderer>().color = Color.white;
                collision.transform.Find("Water").gameObject.SetActive(false);
                collision.GetComponent<BottlePotion>().potionColor = PotionColor.Empty;
                collision.GetComponent<BottlePotion>().potionEffect = PotionEffect.Empty;
                collision.transform.Find("Effect").GetComponent<SpriteRenderer>().sprite = null;

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
                        if (guildSystem.GetComponent<QuestsSystem>().delayWarriors > 0)
                            guildSystem.GetComponent<QuestsSystem>().delayWarriors--;
                        if (potionEffect != PotionEffect.Normal)
                            guildSystem.GetComponent<QuestsSystem>().delayWarriors = settings.questPenalty;
                        break;

                    case "Bandit":
                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Bandits);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Bandits, 15);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Magicians, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Warriors, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Priests, 5);
                        if (guildSystem.GetComponent<QuestsSystem>().delayBandits > 0)
                            guildSystem.GetComponent<QuestsSystem>().delayBandits--;
                        if (potionEffect != PotionEffect.Normal)
                            guildSystem.GetComponent<QuestsSystem>().delayBandits = settings.questPenalty;
                        break;

                    case "Priest":
                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Priests);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Priests, 15);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Warriors, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Magicians, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Bandits, 5);
                        if (guildSystem.GetComponent<QuestsSystem>().delayPriests > 0)
                            guildSystem.GetComponent<QuestsSystem>().delayPriests--;
                        if (potionEffect != PotionEffect.Normal)
                            guildSystem.GetComponent<QuestsSystem>().delayPriests = settings.questPenalty;
                        break;

                    case "Magician":
                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Magicians);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Magicians, 15);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Bandits, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Priests, 5);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Warriors, 5);
                        if (guildSystem.GetComponent<QuestsSystem>().delayMagicians > 0)
                            guildSystem.GetComponent<QuestsSystem>().delayMagicians--;
                        if (potionEffect != PotionEffect.Normal)
                            guildSystem.GetComponent<QuestsSystem>().delayMagicians = settings.questPenalty;
                        break;

                    default:
                        break;
                }                
            }
        }
    }
}