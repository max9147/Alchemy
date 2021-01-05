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

    public List<Resource> inCauldron;
    public List<Resource> inCauldronColored;

    public Cauldrons[] cauldrons;

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
            if (isReady)
                return;
            drainButton.interactable = true;
            buyCauldron.interactable = false;
            Destroy(collision.gameObject);
        }

        switch (collision.transform.tag)
        {
            case "ResourceRed":
                if (inCauldron.Contains(Resource.Red))
                    isWrong = true;
                inCauldron.Add(Resource.Red);
                inCauldronColored.Add(Resource.Red);
                break;

            case "ResourceBlue":
                if (inCauldron.Contains(Resource.Blue))
                    isWrong = true;
                inCauldron.Add(Resource.Blue);
                inCauldronColored.Add(Resource.Blue);
                break;

            case "ResourceYellow":
                if (inCauldron.Contains(Resource.Yellow))
                    isWrong = true;
                inCauldron.Add(Resource.Yellow);
                inCauldronColored.Add(Resource.Yellow);
                break;

            case "ResourceWhite":
                if (inCauldron.Contains(Resource.White))
                    isWrong = true;
                inCauldron.Add(Resource.White);
                inCauldronColored.Add(Resource.White);
                break;

            case "ResourceLadan":
                if (isRare)
                    isWrong = true;
                inCauldron.Add(Resource.Ladan);
                isRare = true;
                break;

            case "ResourceEye":
                if (isRare)
                    isWrong = true;
                inCauldron.Add(Resource.Eye);
                isRare = true;
                break;

            case "ResourceStone":
                if (isRare)
                    isWrong = true;
                inCauldron.Add(Resource.Stone);
                isRare = true;
                break;

            case "ResourceSand":
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
            time = 8f;

        else
        {
            switch (inCauldron.Count)
            {
                case 2:
                    time = 4f;
                    break;

                case 3:
                    time = 5f;
                    break;

                case 4:
                    time = 6f;
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
            brewButton.interactable = true;
            inCauldron.Clear();
            inCauldronColored.Clear();
        }

        else
        {
            yield return new WaitForSeconds(time / speed);
            drainButton.interactable = true;
            isReady = true;
        }

        resourceSystem.GetComponent<DragResources>().StopMixing();

        isWrong = false;
        showTimer = false;
        brewButtonText.text = "Смешать";
        wrongBrewText.gameObject.SetActive(false);

    }

    public void TakePotion()
    {
        isReady = false;
        isRare = false;
        inCauldron.Clear();
        inCauldronColored.Clear();
        buyCauldron.interactable = true;
        progressBar.value = 0;
        brewButton.interactable = true;
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
        brewButton.interactable = true;
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
        speed *= 2;
        fire.SetActive(true);
        isFueled = true;
        StartCoroutine(FuelCombustion());
    }

    IEnumerator FuelCombustion()
    {
        yield return new WaitForSeconds(30 / (speed / 2));
        if (resourceSystem.GetComponent<Fuel>().GetFuelCount() > 0)
            fuelButton.interactable = true;
        speed /= 2;
        fire.SetActive(false);
        isFueled = false;
    }

    public int GetCauldron()
    {
        return cauldronId;
    }
}