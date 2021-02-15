using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MixingSystem : MonoBehaviour
{
    public TextMeshProUGUI brewButtonText;
    public TextMeshProUGUI wrongBrewText;
    public Button brewButton;
    public Button drainButton;
    public Button fuelButton;
    public Button buyCauldron;
    public Slider progressBar;

    public GameObject resourceSystem;
    public GameObject fire;
    public GameObject water;
    public GameObject recipes;
    public GameObject UIControls;

    public GameObject platformRed;
    public GameObject platformBlue;
    public GameObject platformYellow;
    public GameObject platformWhite;
    public GameObject platformLadan;
    public GameObject platformEye;
    public GameObject platformStone;
    public GameObject platformSand;

    public Sprite red;
    public Sprite redEmpty;
    public Sprite blue;
    public Sprite blueEmpty;
    public Sprite yellow;
    public Sprite yellowEmpty;
    public Sprite white;
    public Sprite whiteEmpty;
    public Sprite ladan;
    public Sprite ladanEmpty;
    public Sprite eye;
    public Sprite eyeEmpty;
    public Sprite stone;
    public Sprite stoneEmpty;
    public Sprite sand;
    public Sprite sandEmpty;

    public Potions[] potions;

    public List<Resource> inCauldron;
    public List<Resource> inCauldronColored;

    public Cauldrons[] cauldrons;

    public Settings settings;

    public AudioClip addResource;
    public AudioClip brewSound;
    public AudioClip takePotionSound;

    public Sprite waterStill;
    public Sprite waterBoil;

    public int cauldronId;

    public bool isRare = false;
    public bool isWrong = false;
    public bool isReady = false;
    public bool bottleIn = false;

    private bool showTimer = false;
    private bool isFueled = false;
    private bool slowCooldown = false;
    private bool cauldronSpawned = false;

    public float time = 0f;
    public float startTime;
    private float speed = 1;
    private float curSpeed = 0;
    public float cooldownTime;
    public float speedMul;
    public float chance;

    private string showTime = null;

    private void Start()
    {
        if (!cauldronSpawned)
        {
            cauldronId = 0;
            GetComponent<SpriteRenderer>().sprite = cauldrons[cauldronId].image;
            speedMul = cauldrons[cauldronId].speedMul;
            chance = cauldrons[cauldronId].chance;
        }
    }

    public void ChangeCauldron(int id)
    {
        cauldronSpawned = true;
        cauldronId = id;
        GetComponent<SpriteRenderer>().sprite = cauldrons[cauldronId].image;
        speedMul = cauldrons[cauldronId].speedMul;
        chance = cauldrons[cauldronId].chance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Bottle"))
        {
            GetComponent<AudioSource>().clip = addResource;
            GetComponent<AudioSource>().Play();

            isReady = false;
            drainButton.interactable = true;
            buyCauldron.interactable = false;
            Destroy(collision.gameObject);
            brewButton.interactable = true;
        }

        switch (collision.transform.tag)
        {
            case "ResourceRed":
                if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Red) > 0)
                    platformRed.GetComponent<SpriteRenderer>().sprite = red;
                else platformRed.GetComponent<SpriteRenderer>().sprite = redEmpty;
                if (inCauldron.Contains(Resource.Red))
                    isWrong = true;
                inCauldron.Add(Resource.Red);
                inCauldronColored.Add(Resource.Red);
                break;

            case "ResourceBlue":
                if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Blue) > 0)
                    platformBlue.GetComponent<SpriteRenderer>().sprite = blue;
                else platformBlue.GetComponent<SpriteRenderer>().sprite = blueEmpty;
                if (inCauldron.Contains(Resource.Blue))
                    isWrong = true;
                inCauldron.Add(Resource.Blue);
                inCauldronColored.Add(Resource.Blue);
                break;

            case "ResourceYellow":
                if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Yellow) > 0)
                    platformYellow.GetComponent<SpriteRenderer>().sprite = yellow;
                else platformYellow.GetComponent<SpriteRenderer>().sprite = yellowEmpty;
                if (inCauldron.Contains(Resource.Yellow))
                    isWrong = true;
                inCauldron.Add(Resource.Yellow);
                inCauldronColored.Add(Resource.Yellow);
                break;

            case "ResourceWhite":
                if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.White) > 0)
                    platformWhite.GetComponent<SpriteRenderer>().sprite = white;
                else platformWhite.GetComponent<SpriteRenderer>().sprite = whiteEmpty;
                if (inCauldron.Contains(Resource.White))
                    isWrong = true;
                inCauldron.Add(Resource.White);
                inCauldronColored.Add(Resource.White);
                break;

            case "ResourceLadan":
                if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Ladan) > 0)
                    platformLadan.GetComponent<SpriteRenderer>().sprite = ladan;
                else platformLadan.GetComponent<SpriteRenderer>().sprite = ladanEmpty;
                if (isRare)
                    isWrong = true;
                inCauldron.Add(Resource.Ladan);
                isRare = true;
                break;

            case "ResourceEye":
                if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Eye) > 0)
                    platformEye.GetComponent<SpriteRenderer>().sprite = eye;
                else platformEye.GetComponent<SpriteRenderer>().sprite = eyeEmpty;
                if (isRare)
                    isWrong = true;
                inCauldron.Add(Resource.Eye);
                isRare = true;
                break;

            case "ResourceStone":
                if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Stone) > 0)
                    platformStone.GetComponent<SpriteRenderer>().sprite = stone;
                else platformStone.GetComponent<SpriteRenderer>().sprite = stoneEmpty;
                if (isRare)
                    isWrong = true;
                inCauldron.Add(Resource.Stone);
                isRare = true;
                break;

            case "ResourceSand":
                if (resourceSystem.GetComponent<ResourceSystem>().GetAmount(Resource.Sand) > 0)
                    platformSand.GetComponent<SpriteRenderer>().sprite = sand;
                else platformSand.GetComponent<SpriteRenderer>().sprite = sandEmpty;
                if (isRare)
                    isWrong = true;
                inCauldron.Add(Resource.Sand);
                isRare = true;
                break;

            default:
                break;
        }

        water.GetComponent<WaterColor>().ChangeColor(inCauldronColored, isWrong);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bottle"))
            bottleIn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bottle"))
            bottleIn = false;
    }

    public void Brew()
    {
        cooldownTime = cauldrons[cauldronId].cooldown;
        drainButton.interactable = false;
        resourceSystem.GetComponent<DragResources>().StartMixing();

        if (!isRare && inCauldron.Count < 2 || isRare && inCauldron.Count < 3)
            isWrong = true;

        time = TimeCalc();
        startTime = time;
        brewButton.interactable = false;
        showTimer = true;
        curSpeed = speed;
        StartCoroutine(Brewing());
    }

    public float TimeCalc()
    {
        float time = 0;     

        if (isRare)
            time = settings.timeBrewRare;

        else
        {
            switch (inCauldron.Count)
            {
                case 2:
                    time = settings.timeBrew2;
                    break;

                case 3:
                    time = settings.timeBrew3;
                    break;

                case 4:
                    time = settings.timeBrew4;
                    break;

                default:
                    break;
            }
        }

        time *= speedMul;

        return time;
    }

    IEnumerator Brewing()
    {
        if (isWrong)
        {
            water.GetComponent<WaterColor>().ClearWater();
            wrongBrewText.gameObject.SetActive(true);

            if (isFueled)
            {
                slowCooldown = true;
                yield return new WaitForSeconds(cooldownTime / (speed / 2));
                slowCooldown = false;
            }
            else
                yield return new WaitForSeconds(cooldownTime / speed);

            isReady = false;
            inCauldron.Clear();
            inCauldronColored.Clear();
        }

        else
        {
            GetComponent<AudioSource>().clip = brewSound;
            GetComponent<AudioSource>().Play();
            water.GetComponent<SpriteRenderer>().sprite = waterBoil;

            yield return new WaitForSeconds(time / speed);

            drainButton.interactable = true;
            isReady = true;
            water.GetComponent<SpriteRenderer>().sprite = waterStill;
        }

        resourceSystem.GetComponent<DragResources>().StopMixing();

        isWrong = false;
        showTimer = false;
        brewButtonText.text = "Смешать";
        wrongBrewText.gameObject.SetActive(false);

        switch (GetBrewEffect())
        {
            case PotionEffect.Normal:
                switch (GetBrewColor())
                {
                    case PotionColor.Black:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[3]);
                        break;
                    case PotionColor.Gray:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[13]);
                        break;
                    case PotionColor.Purple:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[48]);
                        break;
                    case PotionColor.Orange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[38]);
                        break;
                    case PotionColor.Green:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[18]);
                        break;
                    case PotionColor.Violet:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[53]);
                        break;
                    case PotionColor.LightOrange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[28]);
                        break;
                    case PotionColor.Lime:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[33]);
                        break;
                    case PotionColor.Pink:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[43]);
                        break;
                    case PotionColor.LightBlue:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[23]);
                        break;
                    case PotionColor.Gold:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[8]);
                        break;
                    default:
                        break;
                }
                break;

            case PotionEffect.Glowing:
                switch (GetBrewColor())
                {
                    case PotionColor.Black:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[2]);
                        break;
                    case PotionColor.Gray:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[12]);
                        break;
                    case PotionColor.Purple:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[47]);
                        break;
                    case PotionColor.Orange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[37]);
                        break;
                    case PotionColor.Green:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[17]);
                        break;
                    case PotionColor.Violet:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[52]);
                        break;
                    case PotionColor.LightOrange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[27]);
                        break;
                    case PotionColor.Lime:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[32]);
                        break;
                    case PotionColor.Pink:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[42]);
                        break;
                    case PotionColor.LightBlue:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[22]);
                        break;
                    case PotionColor.Gold:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[7]);
                        break;
                    default:
                        break;
                }
                break;

            case PotionEffect.Boiling:
                switch (GetBrewColor())
                {
                    case PotionColor.Black:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[0]);
                        break;
                    case PotionColor.Gray:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[10]);
                        break;
                    case PotionColor.Purple:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[45]);
                        break;
                    case PotionColor.Orange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[35]);
                        break;
                    case PotionColor.Green:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[15]);
                        break;
                    case PotionColor.Violet:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[50]);
                        break;
                    case PotionColor.LightOrange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[25]);
                        break;
                    case PotionColor.Lime:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[30]);
                        break;
                    case PotionColor.Pink:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[40]);
                        break;
                    case PotionColor.LightBlue:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[20]);
                        break;
                    case PotionColor.Gold:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[5]);
                        break;
                    default:
                        break;
                }
                break;

            case PotionEffect.Burning:
                switch (GetBrewColor())
                {
                    case PotionColor.Black:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[1]);
                        break;
                    case PotionColor.Gray:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[11]);
                        break;
                    case PotionColor.Purple:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[46]);
                        break;
                    case PotionColor.Orange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[36]);
                        break;
                    case PotionColor.Green:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[16]);
                        break;
                    case PotionColor.Violet:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[51]);
                        break;
                    case PotionColor.LightOrange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[26]);
                        break;
                    case PotionColor.Lime:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[31]);
                        break;
                    case PotionColor.Pink:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[41]);
                        break;
                    case PotionColor.LightBlue:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[21]);
                        break;
                    case PotionColor.Gold:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[6]);
                        break;
                    default:
                        break;
                }
                break;

            case PotionEffect.Smoking:
                switch (GetBrewColor())
                {
                    case PotionColor.Black:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[4]);
                        break;
                    case PotionColor.Gray:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[14]);
                        break;
                    case PotionColor.Purple:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[49]);
                        break;
                    case PotionColor.Orange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[39]);
                        break;
                    case PotionColor.Green:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[19]);
                        break;
                    case PotionColor.Violet:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[54]);
                        break;
                    case PotionColor.LightOrange:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[29]);
                        break;
                    case PotionColor.Lime:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[34]);
                        break;
                    case PotionColor.Pink:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[44]);
                        break;
                    case PotionColor.LightBlue:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[24]);
                        break;
                    case PotionColor.Gold:
                        recipes.GetComponent<RecipesMenu>().AddPotionData(potions[9]);
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }

    public void TakePotion()
    {
        GetComponent<AudioSource>().clip = takePotionSound;
        GetComponent<AudioSource>().Play();

        isReady = false;
        isRare = false;
        inCauldron.Clear();
        inCauldronColored.Clear();
        buyCauldron.interactable = true;
        progressBar.value = 0;
        drainButton.interactable = false;
        water.GetComponent<WaterColor>().ClearWater();
    }

    public void DrainCauldron()
    {
        drainButton.interactable = false;

        foreach (var item in inCauldron)
        {
            if (Random.value <= chance)
            {
                switch (item)
                {
                    case Resource.Red:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Red, 1);
                        break;

                    case Resource.Blue:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Blue, 1);
                        break;

                    case Resource.Yellow:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Yellow, 1);
                        break;

                    case Resource.White:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.White, 1);
                        break;

                    case Resource.Ladan:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Ladan, 1);
                        break;

                    case Resource.Eye:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Eye, 1);
                        break;

                    case Resource.Stone:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Stone, 1);
                        break;

                    case Resource.Sand:
                        resourceSystem.GetComponent<ResourceSystem>().AddResource(Resource.Sand, 1);
                        break;

                    default:
                        break;
                }
            }
        }
        buyCauldron.interactable = true;;
        inCauldron.Clear();
        inCauldronColored.Clear();
        isWrong = false;
        isRare = false;
        isReady = false;
        brewButton.interactable = false;
        progressBar.value = 0;
        water.GetComponent<WaterColor>().ClearWater();
    }

    private void Update()
    {
        if (showTimer)
        {
            time -= Time.deltaTime * curSpeed;
            if (slowCooldown)
                cooldownTime -= Time.deltaTime * (curSpeed / 2);
            else
                cooldownTime -= Time.deltaTime * curSpeed;

            if (isWrong)
                showTime = cooldownTime.ToString("F0");
            else
            {
                showTime = time.ToString("F0");
                progressBar.value = (startTime - time) / startTime;
            }

            brewButtonText.text = showTime + " секунд";
        }
    }

    public void fuelSpeedUp()
    {
        fuelButton.interactable = false;
        speed *= settings.woodSpeedup;
        fire.SetActive(true);
        isFueled = true;
        StartCoroutine(FuelCombustion());
    }

    IEnumerator FuelCombustion()
    {
        yield return new WaitForSeconds(settings.timeWood / (speed / settings.woodSpeedup));
        if (resourceSystem.GetComponent<Fuel>().GetFuelCount() > 0)
            fuelButton.interactable = true;
        speed /= settings.woodSpeedup;
        fire.SetActive(false);
        isFueled = false;
    }

    public int GetCauldron()
    {
        return cauldronId;
    }

    public PotionEffect GetBrewEffect()
    {
        foreach (var item in inCauldron)
        {
            if (item==Resource.Ladan)
                return PotionEffect.Glowing;
            if (item == Resource.Eye)
                return PotionEffect.Boiling;
            if (item == Resource.Stone)
                return PotionEffect.Burning;
            if (item == Resource.Sand)
                return PotionEffect.Smoking;
        }
        return PotionEffect.Normal;
    }

    public PotionColor GetBrewColor()
    {
        switch (inCauldronColored.Count)
        {
            case 2:
                if (inCauldronColored.Contains(Resource.Red) && inCauldronColored.Contains(Resource.Blue))
                    return PotionColor.Purple;

                if (inCauldronColored.Contains(Resource.Red) && inCauldronColored.Contains(Resource.Yellow))
                    return PotionColor.Orange;

                if (inCauldronColored.Contains(Resource.Blue) && inCauldronColored.Contains(Resource.Yellow))
                    return PotionColor.Green;

                if (inCauldronColored.Contains(Resource.Red) && inCauldronColored.Contains(Resource.White))
                    return PotionColor.Pink;

                if (inCauldronColored.Contains(Resource.Blue) && inCauldronColored.Contains(Resource.White))
                    return PotionColor.LightBlue;

                if (inCauldronColored.Contains(Resource.Yellow) && inCauldronColored.Contains(Resource.White))
                    return PotionColor.Gold;
                break;

            case 3:
                if (inCauldronColored.Contains(Resource.Red) && inCauldronColored.Contains(Resource.Blue) && inCauldronColored.Contains(Resource.White))
                    return PotionColor.Violet;

                if (inCauldronColored.Contains(Resource.Red) && inCauldronColored.Contains(Resource.Yellow) && inCauldronColored.Contains(Resource.White))
                    return PotionColor.LightOrange;

                if (inCauldronColored.Contains(Resource.Blue) && inCauldronColored.Contains(Resource.Yellow) && inCauldronColored.Contains(Resource.White))
                    return PotionColor.Lime;

                if (inCauldronColored.Contains(Resource.Red) && inCauldronColored.Contains(Resource.Blue) && inCauldronColored.Contains(Resource.Yellow))
                    return PotionColor.Black;
                break;

            case 4:
                return PotionColor.Gray;

            default:
                break;
        }
        return PotionColor.Empty;
    }
}