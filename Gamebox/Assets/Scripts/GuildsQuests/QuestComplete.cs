using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestComplete : MonoBehaviour
{
    public GameObject guildSystem;
    public GameObject potionSystem;
    public GameObject moneySystem;
    public GameObject resourceSystem;
    public GameObject UIControls;
    public TextMeshProUGUI messageText;
    public PotionColor potionColor;
    public PotionEffect potionEffect;
    public bool haveQuest = false;
    public Settings settings;

    public AudioClip completeTask;

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
                if (guildSystem.GetComponent<QuestsSystem>().firstAmount < 5)
                    guildSystem.GetComponent<QuestsSystem>().GiveFirst();
                else if (guildSystem.GetComponent<QuestsSystem>().firstQuest)
                {
                    messageText.text = "*текст об окончании туториала*";
                    UIControls.GetComponent<Tutorial>().ToggleMessage();
                    guildSystem.GetComponent<QuestsSystem>().firstQuest = false;
                }

                if (guildSystem.GetComponent<QuestsSystem>().questTime > settings.questLimit && guildSystem.GetComponent<QuestsSystem>().questStep == settings.questSpeedupStep)
                {
                    guildSystem.GetComponent<QuestsSystem>().questTime -= settings.questSpeedup;
                    guildSystem.GetComponent<QuestsSystem>().questStep = 0;
                }

                GetComponent<AudioSource>().clip = completeTask;
                GetComponent<AudioSource>().Play();

                guildSystem.GetComponent<QuestsSystem>().questStep++;

                collision.GetComponent<BottlePotion>().justGiven = true;
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
                        moneySystem.GetComponent<MoneySystem>().AddMoney(reward);

                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Warriors);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Warriors, settings.repReward);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Priests, settings.repChangeSec);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Bandits, settings.repChangeSec);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Magicians, settings.repChangeSec);
                        if (guildSystem.GetComponent<QuestsSystem>().delayWarriors > 0)
                            guildSystem.GetComponent<QuestsSystem>().delayWarriors--;
                        if (potionEffect != PotionEffect.Normal)
                            guildSystem.GetComponent<QuestsSystem>().delayWarriors = settings.questPenalty;
                        break;

                    case "Bandit":
                        if (Random.Range(0, 100) < 50 + guildSystem.GetComponent<GuildSystem>().GetRep(Guild.Bandits) / 100 * 35)
                            moneySystem.GetComponent<MoneySystem>().AddMoney(reward);

                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Bandits);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Bandits, settings.repReward);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Magicians, settings.repChangeSec);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Warriors, settings.repChangeSec);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Priests, settings.repChangeSec);
                        if (guildSystem.GetComponent<QuestsSystem>().delayBandits > 0)
                            guildSystem.GetComponent<QuestsSystem>().delayBandits--;
                        if (potionEffect != PotionEffect.Normal)
                            guildSystem.GetComponent<QuestsSystem>().delayBandits = settings.questPenalty;
                        break;

                    case "Priest":
                        if (Random.Range(0, 100) < 5 + guildSystem.GetComponent<GuildSystem>().GetRep(Guild.Priests) / 10)
                            moneySystem.GetComponent<MoneySystem>().AddMoney(reward * 2);
                        else
                            moneySystem.GetComponent<MoneySystem>().AddMoney(reward);

                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Priests);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Priests, settings.repReward);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Warriors, settings.repChangeSec);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Magicians, settings.repChangeSec);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Bandits, settings.repChangeSec);
                        if (guildSystem.GetComponent<QuestsSystem>().delayPriests > 0)
                            guildSystem.GetComponent<QuestsSystem>().delayPriests--;
                        if (potionEffect != PotionEffect.Normal)
                            guildSystem.GetComponent<QuestsSystem>().delayPriests = settings.questPenalty;
                        break;

                    case "Magician":
                        if (Random.Range(0, 100) < 5 + guildSystem.GetComponent<GuildSystem>().GetRep(Guild.Magicians) / 5)
                        {
                            switch (Random.Range(0,3))
                            {
                                case 0:
                                    resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Red, 1);
                                    break;
                                case 1:
                                    resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Blue, 1);
                                    break;
                                case 2:
                                    resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Yellow, 1);
                                    break;
                                case 3:
                                    resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.White, 1);
                                    break;
                                default:
                                    break;
                            }
                        }
                        moneySystem.GetComponent<MoneySystem>().AddMoney(reward);

                        guildSystem.GetComponent<QuestsSystem>().StopQuest(Guild.Magicians);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Magicians, settings.repReward);
                        guildSystem.GetComponent<GuildSystem>().addRep(Guild.Bandits, settings.repChangeSec);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Priests, settings.repChangeSec);
                        guildSystem.GetComponent<GuildSystem>().removeRep(Guild.Warriors, settings.repChangeSec);
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