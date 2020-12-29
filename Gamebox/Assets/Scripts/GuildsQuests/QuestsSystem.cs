using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Hard
}

public class Quest
{
    public PotionColor color;
    public PotionEffect effect;
    public Difficulty difficulty;
    public Guild guild;
    public int reward;
    public float time;
}

public class QuestsSystem : MonoBehaviour
{
    public TextMeshProUGUI textWarriors;
    public TextMeshProUGUI textBandits;
    public TextMeshProUGUI textPriests;
    public TextMeshProUGUI textMagicians;
    public TextMeshProUGUI rewardWarriors;
    public TextMeshProUGUI rewardBandits;
    public TextMeshProUGUI rewardPriests;
    public TextMeshProUGUI rewardMagicians;
    public TextMeshProUGUI timerWarriors;
    public TextMeshProUGUI timerBandits;
    public TextMeshProUGUI timerPriests;
    public TextMeshProUGUI timerMagicians;

    public GameObject warrior;
    public GameObject bandit;
    public GameObject priest;
    public GameObject magician;

    private float timePassed;
    private float timeWarriors;
    private float timeBandits;
    private float timePriests;
    private float timeMagicians;

    private bool warriorsReady = false;
    private bool banditsReady = false;
    private bool priestsReady = false;
    private bool magiciansReady = false;

    public Quest warriorsQuest;
    public Quest banditsQuest;
    public Quest priestsQuest;
    public Quest magiciansQuest;

    public float questTime = 20;

    public int delayWarriors = 0;
    public int delayBandits = 0;
    public int delayPriests = 0;
    public int delayMagicians = 0;

    public string potionName;

    private void Start()
    {
        timePassed = 0;
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= questTime)
        {
            if (warriorsReady && banditsReady && priestsReady && magiciansReady)
                return;

            int guild = Random.Range(0, 4);

            switch (guild)
            {
                case 0:
                    if (!warriorsReady && GetComponent<GuildSystem>().GetRep(Guild.Warriors) >= 15)
                    {
                        timePassed = 0;
                        warriorsReady = true;

                        if (GetComponent<GuildSystem>().GetRep(Guild.Warriors) >= 75 && delayWarriors == 0)
                            warriorsQuest = CreateQuest(Difficulty.Hard, Guild.Warriors);
                        else
                            warriorsQuest = CreateQuest(Difficulty.Easy, Guild.Warriors);

                        textWarriors.text = GetPotionName(warriorsQuest) + " зелье";
                        rewardWarriors.text = warriorsQuest.reward.ToString();
                        timerWarriors.text = warriorsQuest.time + " секунд";
                        timeWarriors = warriorsQuest.time;
                    }
                    break;

                case 1:
                    if (!banditsReady && GetComponent<GuildSystem>().GetRep(Guild.Bandits) >= 15)
                    {
                        timePassed = 0;
                        banditsReady = true;

                        if (GetComponent<GuildSystem>().GetRep(Guild.Bandits) >= 75 && delayBandits == 0)
                            banditsQuest = CreateQuest(Difficulty.Hard, Guild.Bandits);
                        else
                            banditsQuest = CreateQuest(Difficulty.Easy, Guild.Bandits);

                        textBandits.text = GetPotionName(banditsQuest) + " зелье";
                        rewardBandits.text = banditsQuest.reward.ToString();
                        timerBandits.text = banditsQuest.time + " секунд";
                        timeBandits = banditsQuest.time;
                    }
                    break;

                case 2:
                    if (!priestsReady && GetComponent<GuildSystem>().GetRep(Guild.Priests) >= 15)
                    {
                        timePassed = 0;
                        priestsReady = true;

                        if (GetComponent<GuildSystem>().GetRep(Guild.Priests) >= 75 && delayPriests == 0)
                            priestsQuest = CreateQuest(Difficulty.Hard, Guild.Priests);
                        else
                            priestsQuest = CreateQuest(Difficulty.Easy, Guild.Priests);

                        textPriests.text = GetPotionName(priestsQuest) + " зелье";
                        rewardPriests.text = priestsQuest.reward.ToString();
                        timerPriests.text = priestsQuest.time + " секунд";
                        timePriests = priestsQuest.time;
                    }
                    break;

                case 3:
                    if (!magiciansReady && GetComponent<GuildSystem>().GetRep(Guild.Magicians) >= 15)
                    {
                        timePassed = 0;
                        magiciansReady = true;

                        if (GetComponent<GuildSystem>().GetRep(Guild.Magicians) >= 75 && delayMagicians == 0)
                            magiciansQuest = CreateQuest(Difficulty.Hard, Guild.Magicians);
                        else
                            magiciansQuest = CreateQuest(Difficulty.Easy, Guild.Magicians);

                        textMagicians.text = GetPotionName(magiciansQuest) + " зелье";
                        rewardMagicians.text = magiciansQuest.reward.ToString();
                        timerMagicians.text = magiciansQuest.time + " секунд";
                        timeMagicians = magiciansQuest.time;
                    }
                    break;

                default:
                    break;
            }
        }

        if (warriorsReady)
        {
            if (timeWarriors <= 0)
            {
                GetComponent<GuildSystem>().removeRep(Guild.Warriors, 10);
                warrior.GetComponent<QuestComplete>().EndQuest();
                warriorsReady = false;
                textWarriors.text = "";
                rewardWarriors.text = "";
                timerWarriors.text = "";
                delayWarriors = 3;
                questTime = 20;
            }
            else
            {
                timeWarriors -= Time.deltaTime;
                timerWarriors.text = timeWarriors.ToString("F0") + " секунд";
            }
        }

        if (banditsReady)
        {
            if (timeBandits <= 0)
            {
                GetComponent<GuildSystem>().removeRep(Guild.Bandits, 10);
                bandit.GetComponent<QuestComplete>().EndQuest();
                banditsReady = false;
                textBandits.text = "";
                rewardBandits.text = "";
                timerBandits.text = "";
                delayBandits = 3;
                questTime = 20;
            }
            else
            {
                timeBandits -= Time.deltaTime;
                timerBandits.text = timeBandits.ToString("F0") + " секунд";
            }
        }

        if (priestsReady)
        {
            if (timePriests <= 0)
            {
                GetComponent<GuildSystem>().removeRep(Guild.Priests, 10);
                priest.GetComponent<QuestComplete>().EndQuest();
                priestsReady = false;
                textPriests.text = "";
                rewardPriests.text = "";
                timerPriests.text = "";
                delayPriests = 3;
                questTime = 20;
            }
            else
            {
                timePriests -= Time.deltaTime;
                timerPriests.text = timePriests.ToString("F0") + " секунд";
            }
        }

        if (magiciansReady)
        {
            if (timeMagicians <= 0)
            {
                GetComponent<GuildSystem>().removeRep(Guild.Magicians, 10);
                magician.GetComponent<QuestComplete>().EndQuest();
                magiciansReady = false;
                textMagicians.text = "";
                rewardMagicians.text = "";
                timerMagicians.text = "";
                delayMagicians = 3;
                questTime = 20;
            }
            else
            {
                timeMagicians -= Time.deltaTime;
                timerMagicians.text = timeMagicians.ToString("F0") + " секунд";
            }
        }
    }

    public Quest CreateQuest(Difficulty difficulty, Guild guild)
    {
        Quest quest = new Quest();

        quest.difficulty = difficulty;
        quest.guild = guild;

        quest.color = (PotionColor)Random.Range(1, 12);

        switch (difficulty)
        {
            case Difficulty.Easy:
                quest.effect = PotionEffect.Normal;
                quest.reward = 600;
                quest.time = 20;
                break;

            case Difficulty.Hard:
                var temp = Random.value;
                switch (guild)
                {
                    case Guild.Warriors:
                        if (temp > 0.5)
                            quest.effect = PotionEffect.Glowing;
                        else
                            quest.effect = PotionEffect.Smoking;
                        break;

                    case Guild.Bandits:
                        if (temp > 0.5)
                            quest.effect = PotionEffect.Glowing;
                        else
                            quest.effect = PotionEffect.Burning;
                        break;

                    case Guild.Priests:
                        if (temp > 0.5)
                            quest.effect = PotionEffect.Boiling;
                        else
                            quest.effect = PotionEffect.Burning;
                        break;

                    case Guild.Magicians:
                        if (temp > 0.5)
                            quest.effect = PotionEffect.Smoking;
                        else
                            quest.effect = PotionEffect.Boiling;
                        break;

                    default:
                        break;
                }
                quest.reward = 1800;
                quest.time = 40;
                break;

            default:
                break;
        }

        switch (guild)
        {
            case Guild.Warriors:
                warrior.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                break;

            case Guild.Bandits:
                bandit.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                break;

            case Guild.Priests:
                priest.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                break;

            case Guild.Magicians:
                magician.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                break;

            default:
                break;
        }

        return quest;
    }

    public void StopQuest(Guild guild)
    {
        switch (guild)
        {
            case Guild.Warriors:
                warriorsReady = false;
                textWarriors.text = "";
                rewardWarriors.text = "";
                timerWarriors.text = "";
                break;

            case Guild.Bandits:
                banditsReady = false;
                textBandits.text = "";
                rewardBandits.text = "";
                timerBandits.text = "";
                break;

            case Guild.Priests:
                priestsReady = false;
                textPriests.text = "";
                rewardPriests.text = "";
                timerPriests.text = "";
                break;

            case Guild.Magicians:
                magiciansReady = false;
                textMagicians.text = "";
                rewardMagicians.text = "";
                timerMagicians.text = "";
                break;

            default:
                break;
        }
    }

    public string GetPotionName(Quest quest)
    {
        switch (quest.color)
        {
            case PotionColor.Black:
                potionName = "Черное";
                break;
            case PotionColor.Gray:
                potionName = "Серое";
                break;
            case PotionColor.Purple:
                potionName = "Пурпурное";
                break;
            case PotionColor.Orange:
                potionName = "Оранжевое";
                break;
            case PotionColor.Green:
                potionName = "Зеленое";
                break;
            case PotionColor.Violet:
                potionName = "Фиолетовое";
                break;
            case PotionColor.LightOrange:
                potionName = "Светло-оранжевое";
                break;
            case PotionColor.Lime:
                potionName = "Лаймовое";
                break;
            case PotionColor.Pink:
                potionName = "Розовое";
                break;
            case PotionColor.LightBlue:
                potionName = "Голубое";
                break;
            case PotionColor.Gold:
                potionName = "Золотое";
                break;
            default:
                break;
        }

        switch (quest.effect)
        {
            case PotionEffect.Glowing:
                potionName += " светящееся";
                break;
            case PotionEffect.Boiling:
                potionName += " бурлящее";
                break;
            case PotionEffect.Burning:
                potionName += " горящее";
                break;
            case PotionEffect.Smoking:
                potionName += " дымящееся";
                break;
            default:
                break;
        }

        return potionName;
    }
}
