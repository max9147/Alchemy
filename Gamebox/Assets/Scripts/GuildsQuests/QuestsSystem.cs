using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public Button helpButton;

    public GameObject warrior;
    public GameObject bandit;
    public GameObject priest;
    public GameObject magician;

    public GameObject warriorBubble;
    public GameObject banditBubble;
    public GameObject priestBubble;
    public GameObject magicianBubble;

    public GameObject UIControls;
    public GameObject resourceSystem;
    public GameObject moneySystem;
    public GameObject cam;
    public GameObject orders;

    public Settings settings;

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

    public AudioClip newTask;
    public AudioClip failTask;

    public Image warriorsIngredient1;
    public Image warriorsIngredient2;
    public Image banditsIngredient1;
    public Image banditsIngredient2;
    public Image priestsIngredient1;
    public Image priestsIngredient2;
    public Image magiciansIngredient1;
    public Image magiciansIngredient2;

    public Sprite red;
    public Sprite blue;
    public Sprite yellow;
    public Sprite white;

    public float questTime;
    public int questStep = 0;

    public int delayWarriors = 0;
    public int delayBandits = 0;
    public int delayPriests = 0;
    public int delayMagicians = 0;

    public string potionName;

    private void Awake()
    {
        resourceSystem.GetComponent<ResourceSystem>().RemoveResource(Resource.Red, resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Red));
        resourceSystem.GetComponent<ResourceSystem>().RemoveResource(Resource.Blue, resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Blue));
        resourceSystem.GetComponent<ResourceSystem>().RemoveResource(Resource.Yellow, resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Yellow));
        resourceSystem.GetComponent<ResourceSystem>().RemoveResource(Resource.White, resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.White));
        moneySystem.GetComponent<MoneySystem>().SpendMoney(moneySystem.GetComponent<MoneySystem>().GetMoney() - 200);
    }

    private void Start()
    {
        questTime = settings.questDelay;
        timePassed = questTime - settings.questFirst + GetComponent<GuildSystem>().CalcExtraTime();
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= questTime + GetComponent<GuildSystem>().CalcExtraTime() && !(warriorsReady && banditsReady && priestsReady && magiciansReady))
        {
            int guild = Random.Range(0, 4);
            switch (guild)
            {
                case 0:
                    if (!warriorsReady && GetComponent<GuildSystem>().GetRep(Guild.Warriors) >= settings.repMin)
                    {
                        timePassed = 0;
                        warriorsReady = true;

                        GetComponent<AudioSource>().clip = newTask;
                        GetComponent<AudioSource>().Play();

                        if (GetComponent<GuildSystem>().GetRep(Guild.Warriors) >= settings.repMax && delayWarriors == 0)
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
                    if (!banditsReady && GetComponent<GuildSystem>().GetRep(Guild.Bandits) >= settings.repMin)
                    {
                        timePassed = 0;
                        banditsReady = true;

                        GetComponent<AudioSource>().clip = newTask;
                        GetComponent<AudioSource>().Play();

                        if (GetComponent<GuildSystem>().GetRep(Guild.Bandits) >= settings.repMax && delayBandits == 0)
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
                    if (!priestsReady && GetComponent<GuildSystem>().GetRep(Guild.Priests) >= settings.repMin)
                    {
                        timePassed = 0;
                        priestsReady = true;

                        GetComponent<AudioSource>().clip = newTask;
                        GetComponent<AudioSource>().Play();

                        if (GetComponent<GuildSystem>().GetRep(Guild.Priests) >= settings.repMax && delayPriests == 0)
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
                    if (!magiciansReady && GetComponent<GuildSystem>().GetRep(Guild.Magicians) >= settings.repMin)
                    {
                        timePassed = 0;
                        magiciansReady = true;

                        GetComponent<AudioSource>().clip = newTask;
                        GetComponent<AudioSource>().Play();

                        if (GetComponent<GuildSystem>().GetRep(Guild.Magicians) >= settings.repMax && delayMagicians == 0)
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
                warrior.GetComponent<SpriteRenderer>().enabled = false;
                warriorBubble.SetActive(false);

                GetComponent<AudioSource>().clip = failTask;
                GetComponent<AudioSource>().Play();

                GetComponent<GuildSystem>().removeRep(Guild.Warriors, settings.repPenalty);

                warrior.GetComponent<QuestComplete>().EndQuest();
                warriorsReady = false;
                textWarriors.text = "";
                rewardWarriors.text = "";
                timerWarriors.text = "";
                delayWarriors = settings.questPenalty;
                questTime = settings.questDelay;
                questStep = 0;
                if (!helpButton.interactable)
                {
                    UIControls.GetComponent<Tutorial>().helpStep = 5;
                    UIControls.GetComponent<Tutorial>().GetHelp();
                }
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
                bandit.GetComponent<SpriteRenderer>().enabled = false;
                banditBubble.SetActive(false);

                GetComponent<AudioSource>().clip = failTask;
                GetComponent<AudioSource>().Play();

                GetComponent<GuildSystem>().removeRep(Guild.Bandits, settings.repPenalty);

                bandit.GetComponent<QuestComplete>().EndQuest();
                banditsReady = false;
                textBandits.text = "";
                rewardBandits.text = "";
                timerBandits.text = "";
                delayBandits = settings.questPenalty;
                questTime = settings.questDelay;
                questStep = 0;
                if (!helpButton.interactable)
                {
                    UIControls.GetComponent<Tutorial>().helpStep = 5;
                    UIControls.GetComponent<Tutorial>().GetHelp();
                }
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
                priest.GetComponent<SpriteRenderer>().enabled = false;
                priestBubble.SetActive(false);

                GetComponent<AudioSource>().clip = failTask;
                GetComponent<AudioSource>().Play();

                GetComponent<GuildSystem>().removeRep(Guild.Priests, settings.repPenalty);

                priest.GetComponent<QuestComplete>().EndQuest();
                priestsReady = false;
                textPriests.text = "";
                rewardPriests.text = "";
                timerPriests.text = "";
                delayPriests = settings.questPenalty;
                questTime = settings.questDelay;
                questStep = 0;
                if (!helpButton.interactable)
                {
                    UIControls.GetComponent<Tutorial>().helpStep = 5;
                    UIControls.GetComponent<Tutorial>().GetHelp();
                }
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
                magician.GetComponent<SpriteRenderer>().enabled = false;
                magicianBubble.SetActive(false);

                GetComponent<AudioSource>().clip = failTask;
                GetComponent<AudioSource>().Play();

                GetComponent<GuildSystem>().removeRep(Guild.Magicians, settings.repPenalty);

                magician.GetComponent<QuestComplete>().EndQuest();
                magiciansReady = false;
                textMagicians.text = "";
                rewardMagicians.text = "";
                timerMagicians.text = "";
                delayMagicians = settings.questPenalty;
                questTime = settings.questDelay;
                questStep = 0;
                if (!helpButton.interactable)
                {
                    UIControls.GetComponent<Tutorial>().helpStep = 5;
                    UIControls.GetComponent<Tutorial>().GetHelp();
                }
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
                quest.reward = settings.questRewardEasy;
                quest.time = settings.questTimeEasy;
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
                quest.reward = settings.questRewardHard;
                quest.time = settings.questTimeHard;
                break;

            default:
                break;
        }

        switch (guild)
        {
            case Guild.Warriors:
                warrior.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                warrior.GetComponent<SpriteRenderer>().enabled = true;
                warriorBubble.SetActive(true);
                break;

            case Guild.Bandits:
                bandit.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                bandit.GetComponent<SpriteRenderer>().enabled = true;
                banditBubble.SetActive(true);
                break;

            case Guild.Priests:
                priest.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                priest.GetComponent<SpriteRenderer>().enabled = true;
                priestBubble.SetActive(true);
                break;

            case Guild.Magicians:
                magician.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                magician.GetComponent<SpriteRenderer>().enabled = true;
                magicianBubble.SetActive(true);
                break;

            default:
                break;
        }

        return quest;
    }

    public Quest CreateFirstQuest(Guild guild)
    {
        Quest quest = new Quest();

        quest.difficulty = Difficulty.Easy;
        quest.guild = guild;

        quest.effect = PotionEffect.Normal;
        quest.reward = settings.questRewardEasy;
        quest.time = settings.questTimeEasy;

        switch (guild)
        {
            case Guild.Warriors:
                warrior.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                warrior.GetComponent<SpriteRenderer>().enabled = true;
                warriorBubble.SetActive(true);
                break;

            case Guild.Bandits:
                bandit.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                bandit.GetComponent<SpriteRenderer>().enabled = true;
                banditBubble.SetActive(true);
                break;

            case Guild.Priests:
                priest.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                priest.GetComponent<SpriteRenderer>().enabled = true;
                priestBubble.SetActive(true);
                break;

            case Guild.Magicians:
                magician.GetComponent<QuestComplete>().NewQuest(quest.color, quest.effect, quest.reward);
                magician.GetComponent<SpriteRenderer>().enabled = true;
                magicianBubble.SetActive(true);
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
                warriorBubble.SetActive(false);
                warriorsReady = false;
                textWarriors.text = "";
                rewardWarriors.text = "";
                timerWarriors.text = "";
                break;

            case Guild.Bandits:
                banditBubble.SetActive(false);
                banditsReady = false;
                textBandits.text = "";
                rewardBandits.text = "";
                timerBandits.text = "";
                break;

            case Guild.Priests:
                priestBubble.SetActive(false);
                priestsReady = false;
                textPriests.text = "";
                rewardPriests.text = "";
                timerPriests.text = "";
                break;

            case Guild.Magicians:
                magicianBubble.SetActive(false);
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
                potionName = settings.colorNames[6];
                break;
            case PotionColor.Gray:
                potionName = settings.colorNames[7];
                break;
            case PotionColor.Purple:
                potionName = settings.colorNames[0];
                break;
            case PotionColor.Orange:
                potionName = settings.colorNames[1];
                break;
            case PotionColor.Green:
                potionName = settings.colorNames[2];
                break;
            case PotionColor.Violet:
                potionName = settings.colorNames[9];
                break;
            case PotionColor.Peach:
                potionName = settings.colorNames[8];
                break;
            case PotionColor.Lime:
                potionName = settings.colorNames[10];
                break;
            case PotionColor.Pink:
                potionName = settings.colorNames[3];
                break;
            case PotionColor.LightBlue:
                potionName = settings.colorNames[5];
                break;
            case PotionColor.Gold:
                potionName = settings.colorNames[4];
                break;
            default:
                break;
        }

        switch (quest.effect)
        {
            case PotionEffect.Glowing:
                potionName += " " + settings.effectNames[0];
                break;
            case PotionEffect.Boiling:
                potionName += " " + settings.effectNames[1];
                break;
            case PotionEffect.Burning:
                potionName += " " + settings.effectNames[2];
                break;
            case PotionEffect.Smoking:
                potionName += " " + settings.effectNames[3];
                break;
            default:
                break;
        }

        return potionName;
    }
}
