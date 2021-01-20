using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    [Header("Время на выполнение легкого квеста, сек.")]
    public float questTimeEasy;

    [Header("Время на выполнение сложного квеста, сек.")]
    public float questTimeHard;

    [Header("Интервал выдачи квеста, сек.")]
    public float questDelay;

    [Header("Ускорение выдачи квестов при выполнении, сек.")]
    public float questSpeedup;

    [Header("Кол-во выполненных квестов для ускорения выдачи квестов")]
    public int questSpeedupStep;

    [Header("Минимальное время выдачи квеста, сек.")]
    public float questLimit;

    [Header("Время до выдачи первого квеста, сек.")]
    public float questFirst;

    [Header("Штраф за провал квеста (минимальное кол-во квестов до следующего квеста данной гильдии)")]
    public int questPenalty;

    [Header("Вознаграждение за легкий квест")]
    public int questRewardEasy;

    [Header("Вознаграждение за сложный квест")]
    public int questRewardHard;

    [Header("Начальная репутация")]
    public int rep;

    [Header("Начальные деньги")]
    public int money;

    [Header("Время варки зелья из 2 ингредиентов, сек.")]
    public int timeBrew2;

    [Header("Время варки зелья из 3 ингредиентов, сек.")]
    public int timeBrew3;

    [Header("Время варки зелья из 4 ингредиентов, сек.")]
    public int timeBrew4;

    [Header("Время варки редкого зелья, сек.")]
    public int timeBrewRare;

    [Header("Время действия дров, сек.")]
    public int timeWood;

    [Header("Во сколько раз дрова ускоряют варку")]
    public int woodSpeedup;
}
