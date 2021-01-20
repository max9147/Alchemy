using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Guild
{
    Warriors,
    Bandits,
    Priests,
    Magicians
}

public class GuildSystem : MonoBehaviour
{
    public Slider sliderWarriors;
    public Slider sliderBandits;
    public Slider sliderPriests;
    public Slider sliderMagicians;

    public Button buyRepWarriors;
    public Button buyRepBandits;
    public Button buyRepPriests;
    public Button buyRepMagicians;

    public Settings settings;

    public int repWarriors;
    public int repBandits;
    public int repPriests;
    public int repMagicians;

    private void Start()
    {
        sliderWarriors.value = repWarriors;
        sliderBandits.value = repBandits;
        sliderPriests.value = repPriests;
        sliderMagicians.value = repMagicians;

        if (repWarriors <= 15)
            buyRepWarriors.interactable = true;
        if (repBandits <= 15)
            buyRepBandits.interactable = true;
        if (repPriests <= 15)
            buyRepPriests.interactable = true;
        if (repMagicians <= 15)
            buyRepMagicians.interactable = true;
    }

    public void addRep(Guild guild, int amount)
    {
        switch (guild)
        {
            case Guild.Warriors:
                repWarriors += amount;
                if (repWarriors > 100)
                    repWarriors = 100;
                sliderWarriors.value = repWarriors;
                break;

            case Guild.Bandits:
                repBandits += amount;
                if (repBandits > 100)
                    repBandits = 100;
                sliderBandits.value = repBandits;
                break;

            case Guild.Priests:
                repPriests += amount;
                if (repPriests > 100)
                    repPriests = 100;
                sliderPriests.value = repPriests;
                break;

            case Guild.Magicians:
                repMagicians += amount;
                if (repMagicians > 100)
                    repMagicians = 100;
                sliderMagicians.value = repMagicians;
                break;

            default:
                break;
        }
    }

    public void removeRep(Guild guild, int amount)
    {
        switch (guild)
        {
            case Guild.Warriors:
                repWarriors -= amount;
                if (repWarriors < 0)
                    repWarriors = 0;
                sliderWarriors.value = repWarriors;
                if (repWarriors <= 15)
                    buyRepWarriors.interactable = true;
                break;

            case Guild.Bandits:
                repBandits -= amount;
                if (repBandits < 0)
                    repBandits = 0;
                sliderBandits.value = repBandits;
                if (repBandits <= 15)
                    buyRepBandits.interactable = true;
                break;

            case Guild.Priests:
                repPriests -= amount;
                if (repPriests < 0)
                    repPriests = 0;
                sliderPriests.value = repPriests;
                if (repPriests <= 15)
                    buyRepPriests.interactable = true;
                break;

            case Guild.Magicians:
                repMagicians -= amount;
                if (repMagicians < 0)
                    repMagicians = 0;
                sliderMagicians.value = repMagicians;
                if (repMagicians <= 15)
                    buyRepMagicians.interactable = true;
                break;

            default:
                break;
        }
    }

    public int GetRep(Guild guild)
    {
        switch (guild)
        {
            case Guild.Warriors:
                return repWarriors;

            case Guild.Bandits:
                return repBandits;

            case Guild.Priests:
                return repPriests;

            case Guild.Magicians:
                return repMagicians;

            default:
                return 0;
        }
    }

    public int CalcExtraTime()
    {
        int time = 0;

        if (repWarriors < 40)
            time += (40 - repWarriors) / 5;

        if (repBandits < 40)
            time += (40 - repBandits) / 5;

        if (repMagicians < 40)
            time += (40 - repMagicians) / 5;

        if (repPriests < 40)
            time += (40 - repPriests) / 5;

        return time;
    }
}