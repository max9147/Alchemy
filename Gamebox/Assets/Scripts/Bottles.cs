using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bottles : MonoBehaviour
{
    public Camera cam;

    public Button drainButton;
    public TextMeshProUGUI amountText;

    public Sprite effectFire;
    public Sprite effectSmoke;
    public Sprite effectGlow;
    public Sprite effectBoil;

    public GameObject[] bottlePos;
    public GameObject[] bottle;
    public GameObject cauldron;
    public GameObject UIControls;
    public GameObject water;
    public GameObject potionSystem;
    public GameObject bottleSpawner;

    public bool[] takenSpace = new bool[8];
    public bool[] bottleUsage = new bool[8];

    private Touch touch;
    private Transform toDrag;
    private Rigidbody2D toDragRB;

    public bool canTake = true;
    private bool isDragging = false;
    private bool justTook = false;
    private bool justReturned = false;

    private float time = 1;

    public int bottleCount = 2;
    public int freeBottles;
    private int takenBottleNumber;

    private void Start()
    {
        amountText.text = bottleCount.ToString();
        freeBottles = bottleCount;

        for (int i = 0; i < bottle.Length; i++)
        {
            if (bottleUsage[i])
            {
                for (int j = 0; j < takenSpace.Length; j++)
                {
                    if (!takenSpace[j])
                    {
                        takenSpace[j] = true;
                        bottle[i].transform.position = bottlePos[j].transform.position;
                        bottle[i].SetActive(true);
                        freeBottles--;
                        amountText.text = freeBottles.ToString();
                        break;
                    }
                }
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < bottleCount; i++)
        {
            if (bottle[i].transform.position != bottlePos[bottle[i].GetComponent<BottlePotion>().takenSpace].transform.position && bottle[i].GetComponent<BottlePotion>().potionColor != PotionColor.Empty && !isDragging)
            {
                bottle[i].transform.position = Vector3.MoveTowards(bottle[i].transform.position, bottlePos[bottle[i].GetComponent<BottlePotion>().takenSpace].transform.position, 0.3f);
            }
        }

        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && canTake)
            {
                isDragging = true;

                Vector3 pos = cam.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (Physics2D.Raycast(pos, Vector2.zero) && hit.collider.gameObject.layer == LayerMask.NameToLayer("Bottle"))
                {
                    time = 0;
                    canTake = false;
                    toDrag = hit.collider.gameObject.transform;

                    if (toDrag != null)
                        toDragRB = toDrag.GetComponent<Rigidbody2D>();

                    takenSpace[toDrag.GetComponent<BottlePotion>().takenSpace] = false;
                }

                if (Physics2D.Raycast(pos, Vector2.zero) && hit.transform.CompareTag("BottleSpawner") && freeBottles > 0)
                {
                    freeBottles--;
                    amountText.text = freeBottles.ToString();

                    time = 0;
                    canTake = false;

                    for (int i = 0; i < bottleCount; i++)
                    {
                        if (!bottleUsage[i])
                        {
                            toDrag = bottle[i].transform;
                            toDrag.gameObject.SetActive(true);
                            bottleUsage[i] = true;
                            break;
                        }
                    }

                    if (toDrag != null)
                        toDragRB = toDrag.GetComponent<Rigidbody2D>();
                }
            }
            time += Time.deltaTime;
            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                if (toDrag != null)
                {
                    toDragRB.simulated = false;
                    toDrag.position = cam.ScreenToWorldPoint(touch.position);
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (time < 0.1f)
                    UIControls.GetComponent<CameraMovement>().MoveCam();
                time = 1;

                isDragging = false;
                if (toDrag != null)
                {
                    StartCoroutine(Wait());
                    toDragRB.simulated = true;
                }
            }
        }

        if (Input.touchCount == 0 && toDrag)
        {
            if (cauldron.GetComponent<MixingSystem>().isReady && cauldron.GetComponent<MixingSystem>().bottleIn && !justReturned)
            {
                justTook = true;
                for (int i = 0; i < takenSpace.Length; i++)
                {
                    if (!takenSpace[i])
                    {
                        toDrag.GetComponent<BottlePotion>().AddPotion(cauldron.GetComponent<MixingSystem>().inCauldron,
                            cauldron.GetComponent<MixingSystem>().inCauldronColored, cauldron.GetComponent<MixingSystem>().isRare, i);
                        takenSpace[i] = true;
                        break;
                    }
                }

                toDrag.Find("Water").gameObject.SetActive(true);
                toDrag.Find("Water").GetComponent<SpriteRenderer>().color = water.GetComponent<SpriteRenderer>().color;

                switch (cauldron.GetComponent<MixingSystem>().GetBrewEffect())
                {
                    case PotionEffect.Glowing:
                        toDrag.Find("Effect").GetComponent<SpriteRenderer>().sprite = effectGlow;
                        break;
                    case PotionEffect.Boiling:
                        toDrag.Find("Effect").GetComponent<SpriteRenderer>().sprite = effectBoil;
                        break;
                    case PotionEffect.Burning:
                        toDrag.Find("Effect").GetComponent<SpriteRenderer>().sprite = effectFire;
                        break;
                    case PotionEffect.Smoking:
                        toDrag.Find("Effect").GetComponent<SpriteRenderer>().sprite = effectSmoke;
                        break;
                    default:
                        break;
                }

                cauldron.GetComponent<MixingSystem>().TakePotion();
            }

            if (cauldron.GetComponent<MixingSystem>().bottleIn && cauldron.GetComponent<MixingSystem>().inCauldron.Count == 0 && toDrag.GetComponent<BottlePotion>().potionColor != PotionColor.Empty && !justTook)
            {
                justReturned = true;
                toDrag.GetComponent<BottlePotion>().justDrained = true;
                cauldron.GetComponent<MixingSystem>().isReady = true;
                switch (toDrag.GetComponent<BottlePotion>().potionColor)
                {
                    case PotionColor.Black:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[4];
                        break;
                    case PotionColor.Gray:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[5];
                        break;
                    case PotionColor.Purple:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[6];
                        break;
                    case PotionColor.Orange:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[7];
                        break;
                    case PotionColor.Green:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[8];
                        break;
                    case PotionColor.Violet:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[9];
                        break;
                    case PotionColor.LightOrange:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[10];
                        break;
                    case PotionColor.Lime:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[11];
                        break;
                    case PotionColor.Pink:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[12];
                        break;
                    case PotionColor.LightBlue:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[13];
                        break;
                    case PotionColor.Gold:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[14];
                        break;
                    default:
                        break;
                }

                switch (toDrag.GetComponent<BottlePotion>().potionEffect)
                {
                    case PotionEffect.Glowing:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Ladan);
                        cauldron.GetComponent<MixingSystem>().isRare = true;
                        break;
                    case PotionEffect.Boiling:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Eye);
                        cauldron.GetComponent<MixingSystem>().isRare = true;
                        break;
                    case PotionEffect.Burning:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Stone);
                        cauldron.GetComponent<MixingSystem>().isRare = true;
                        break;
                    case PotionEffect.Smoking:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Sand);
                        cauldron.GetComponent<MixingSystem>().isRare = true;
                        break;
                    default:
                        break;
                }

                toDrag.Find("Effect").GetComponent<SpriteRenderer>().sprite = null;
                toDrag.Find("Water").GetComponent<SpriteRenderer>().color = Color.white;
                toDrag.Find("Water").gameObject.SetActive(false);
                toDrag.GetComponent<BottlePotion>().potionColor = PotionColor.Empty;
                toDrag.GetComponent<BottlePotion>().potionEffect = PotionEffect.Empty;
                drainButton.interactable = true;

                switch (toDrag.tag)
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
            }
        }

        if (toDrag && !isDragging)
        {
            if (toDrag.GetComponent<BottlePotion>().potionColor == PotionColor.Empty)
            {
                takenBottleNumber = toDrag.GetComponent<BottlePotion>().takenSpace;
                toDrag.position = Vector3.MoveTowards(toDrag.position, bottleSpawner.transform.position, 0.3f);
                if (toDrag.position == bottleSpawner.transform.position)
                {
                    switch (toDrag.tag)
                    {
                        case "Bottle1":
                            bottleUsage[0] = false;
                            break;
                        case "Bottle2":
                            bottleUsage[1] = false;
                            break;
                        case "Bottle3":
                            bottleUsage[2] = false;
                            break;
                        case "Bottle4":
                            bottleUsage[3] = false;
                            break;
                        case "Bottle5":
                            bottleUsage[4] = false;
                            break;
                        case "Bottle6":
                            bottleUsage[5] = false;
                            break;
                        case "Bottle7":
                            bottleUsage[6] = false;
                            break;
                        case "Bottle8":
                            bottleUsage[7] = false;
                            break;
                        default:
                            break;
                    }

                    int max = 0;
                    for (int i = 0; i < bottleCount; i++)
                    {
                        if (bottle[i].GetComponent<BottlePotion>().potionColor != PotionColor.Empty && bottle[i].GetComponent<BottlePotion>().takenSpace > takenBottleNumber && toDrag.GetComponent<BottlePotion>().justDrained)
                        {
                            if (max < bottle[i].GetComponent<BottlePotion>().takenSpace)
                                max = bottle[i].GetComponent<BottlePotion>().takenSpace;

                            bottle[i].GetComponent<BottlePotion>().takenSpace--;
                            takenSpace[bottle[i].GetComponent<BottlePotion>().takenSpace] = true;
                        }
                    }
                    if (max > 0)
                        takenSpace[max] = false;

                    toDrag.GetComponent<BottlePotion>().justDrained = false;
                    toDrag.gameObject.SetActive(false);
                    canTake = true;
                    toDrag = null;
                    toDragRB.simulated = true;
                    toDragRB = null;
                    justTook = false;
                    justReturned = false;
                    freeBottles++;
                    amountText.text = freeBottles.ToString();
                }
            }
            else
            {
                toDrag.position = Vector3.MoveTowards(toDrag.position, bottlePos[toDrag.GetComponent<BottlePotion>().takenSpace].transform.position, 0.3f);
                if (toDrag.position == bottlePos[toDrag.GetComponent<BottlePotion>().takenSpace].transform.position)
                {
                    takenSpace[toDrag.GetComponent<BottlePotion>().takenSpace] = true;
                    canTake = true;
                    toDrag = null;
                    toDragRB.simulated = true;
                    toDragRB = null;
                    justTook = false;
                    justReturned = false;
                }
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.02f);

        if (toDragRB)
            toDragRB.simulated = false;
    }

    public void AddBottle()
    {
        bottleCount++;
        freeBottles++;
        amountText.text = freeBottles.ToString();
    }

    public int GetBottleCount()
    {
        return bottleCount;
    }
}