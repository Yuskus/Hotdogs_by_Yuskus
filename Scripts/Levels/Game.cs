using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour //check
{
    private MyData data;
    private int mySalary, AllTimeSalary, _children, _max, _level;
    private int moneyOnTable, peopleInCafe;
    public int indexOfLevel;
    private float timerDay, oneSec, _intervalMin, _intervalMax;
    private StringBuilder myTimerDisplay, mySalaryDisplay, myRecordDisplay, myPlanDisplay;
    public bool learn;
    private bool makingClients, isLevelEnded, levelIsStarted;
    private GameObject Canvas, ClosePanel, TextMoney, Record, Plan, Timer;
    public GameObject Interactive, WishCloud, Money, TextPref, LearningPointer, Learn;
    public GameObject AllClients, AtHome, OnScene, StoikaOnly, PlitaSosis, StolHotDog, SauceK, SauceG;
    public Text LearnText;
    private Text moneyPan, timePan;
    public Sprite[] forClientsWish = new Sprite[5];
    private WaitForSeconds forSecond, halfSecond;
    public readonly Vector2[] PlacePeople = new Vector2[5] { new(5f, 0f), new(9.5f, 0f), new(14f, 0f), new(18.5f, 0f), new(23f, 0f) }; //€чейки дл€ людей
    public readonly string[] forEatPhase = new string[10] { "Nothing", "HotDog", "Burger", "DrinkClone", "Nothing", "Free", "ColaClone", "Nothing", "DrinkClone", "ColaClone" };
    public readonly Dictionary <string, int> priceOfFood = new() { { "HotDog", 15 }, { "Burger", 20 }, { "DrinkClone", 45 }, { "Free", 50 }, { "ColaClone", 50 } };
    private readonly string[] paths = new string[5] { "Sprites/Drink", "Sprites/DoneFree", "Sprites/HotDog2", "Sprites/Burger2", "Sprites/Cola" };
    private readonly string[] names = new string[5] { "DrinkClone", "Free", "HotDog", "Burger", "ColaClone" };
    public readonly int[] bonusPrice = new int[16] { 0, 10, 10, 20, 15, 25, 25, 35, 30, 40, 40, 50, 45, 55, 55, 65 };
    public Dictionary<string, Sprite> ForWish { get; private set; }
    public static int TimelyContinue { get; private set; }
    public static int TimelyAvailable { get; private set; }
    private float TimerDay
    {
        get { return timerDay; }
        set
        {
            timerDay = value;
            TimeIsChanged();
        }
    }
    public int MySalary
    {
        get { return mySalary; }
        set
        {
            mySalary = value;
            ChangeMySalary();
        }
    }
    public int PeopleInCafe
    {
        get { return peopleInCafe; }
        set
        {
            peopleInCafe = value;
            if (value == 0 && isLevelEnded) { EndLevelChecking(); }
        }
    }
    public int MoneyOnTable
    {
        get { return moneyOnTable; }
        set
        {
            moneyOnTable = value;
            if (value == 0 && isLevelEnded) { EndLevelChecking(); }
        }
    }
    private void Awake()
    {
        data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        TimelyAvailable = data.AvailableLevels;
        TimelyContinue = data.ContinueGame;
        indexOfLevel = data.ContinueGame + 1; //reeading
        transform.gameObject.AddComponent(Type.GetType("Level_" + indexOfLevel)); //MenuButtons 44
    }
    private void Start()
    {
        Camera.main.GetComponent<FocusCamera>().CameraPos(Camera.main);
    }
    public void AwakeAnyLevel()
    {
        TextPref = Resources.Load<GameObject>("Prefabs/TextMoney");
        AllClients = GameObject.FindGameObjectWithTag("Player");
        StoikaOnly = GameObject.FindGameObjectWithTag("Table");
        ClosePanel = GameObject.FindGameObjectWithTag("Finish");
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        PlitaSosis = StoikaOnly.transform.GetChild(1).gameObject;
        StolHotDog = StoikaOnly.transform.GetChild(3).gameObject;
        SauceK = StoikaOnly.transform.GetChild(10).gameObject;
        SauceG = StoikaOnly.transform.GetChild(11).gameObject;
        Interactive = StoikaOnly.transform.GetChild(12).gameObject;
        WishCloud = StoikaOnly.transform.GetChild(12).GetChild(0).gameObject;
        LearningPointer = StoikaOnly.transform.GetChild(17).GetChild(0).gameObject;
        Learn = Canvas.transform.GetChild(4).gameObject;
        LearnText = Canvas.transform.GetChild(4).GetComponent<Text>();
        TextMoney = Canvas.transform.GetChild(0).GetChild(1).gameObject;
        Plan = Canvas.transform.GetChild(0).GetChild(2).gameObject;
        Record = Canvas.transform.GetChild(0).GetChild(3).gameObject;
        Timer = Canvas.transform.GetChild(0).GetChild(4).gameObject;
        AtHome = AllClients.transform.GetChild(0).gameObject;
        OnScene = AllClients.transform.GetChild(1).gameObject;
        Money = Resources.Load<GameObject>("Prefabs/GetMoney");
        ForWish = new(forClientsWish.Length);
        for (int i = 0; i < forClientsWish.Length; i++)
        {
            forClientsWish[i] = Resources.Load<Sprite>(paths[i]);
            ForWish.Add(names[i], forClientsWish[i]);
        }
        myPlanDisplay = new StringBuilder(16, 32);
        mySalaryDisplay = new StringBuilder(16, 32);
        myRecordDisplay = new StringBuilder(16, 32);
        myTimerDisplay = new StringBuilder(16, 32);
    }
    public void StartAnyLevel() //главный скрипт дл€ уровней
    {
        forSecond = new(1f);
        halfSecond = new(0.5f);
        learn = false;
        makingClients = false;
        myPlanDisplay.Append("Plan: " + RecData.plans[data.ContinueGame]);
        Plan.GetComponent<Text>().text = myPlanDisplay.ToString();
        myRecordDisplay.Append("Record: " + data.LvlRec[data.ContinueGame]); //
        Record.GetComponent<Text>().text = myRecordDisplay.ToString();
        data.RecSum(out AllTimeSalary);
        moneyPan = TextMoney.GetComponent<Text>();
        timePan = Timer.GetComponent<Text>();
        TimerDay = 120f;
        moneyPan.text = "Money: 0";
    }
    public void TabloOn() //главный скрипт дл€ уровней 
    {
        Canvas.transform.GetChild(3).gameObject.SetActive(true);
        Canvas.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Level: {indexOfLevel} \nPlan: {RecData.plans[data.ContinueGame]} \nTime: 2:00 \n\nGood luck!";
        Invoke(nameof(TabloOff), 3f);
    }
    private void TabloOff() => Canvas.transform.GetChild(3).gameObject.SetActive(false); //start table
    public void TheFirstFew(int children, float intervalMin, float intervalMax, int max, int level) //start coroutines
    {
        _children = children;
        _intervalMin = intervalMin;
        _intervalMax = intervalMax;
        _max = max;
        _level = level;
        StartCoroutine(PeopleFew());
    }
    private IEnumerator PeopleFew() //first few clients
    {
        levelIsStarted = true;
        for (int i = 0, interval = 1; i < _max; i++)
        {
            Sec();
            interval += 1;
            yield return new WaitForSeconds(UnityEngine.Random.Range(_intervalMin, interval + _intervalMax));
        }
        while (!makingClients)
        {
            if (OnScene.GetComponent<WhoIsOnScene>().TheLastClient()) { makingClients = true; }
            else { yield return halfSecond; }
        }
        StartCoroutine(PeopleRepeat());
        yield break;
    }
    private IEnumerator PeopleRepeat() //flow of clients after first few 
    {
        while (makingClients)
        {
            if (OnScene.transform.childCount < _children)
            {
                Sec();
                yield return new WaitForSeconds(UnityEngine.Random.Range(_intervalMin, _intervalMax));
            }
            else { yield return forSecond; }
        }
        yield break;
    }
    public void Sec() => AtHome.transform.GetChild(UnityEngine.Random.Range(0, AtHome.transform.childCount)).gameObject.SetActive(true); //create person
    public void TimerForLevel() //Update()
    {
        if (levelIsStarted)
        {
            if (TimerDay > 0)
            {
                oneSec += Time.deltaTime;
                if (oneSec > 1f) { TimerDay--; oneSec--; }
            }
            else { TimerIsStopped(); }
        }
    }
    private void ChangeMySalary() //something like event
    {
        mySalaryDisplay.Length = 0;
        mySalaryDisplay.AppendFormat("Money: {0}", MySalary);
        moneyPan.text = mySalaryDisplay.ToString();
    }
    private void TimeIsChanged() //something like event
    {
        myTimerDisplay.Length = 0;
        myTimerDisplay.AppendFormat("Time:   {0:00}:{1:00}", (int)TimerDay / 60, (int)TimerDay % 60);
        timePan.text = myTimerDisplay.ToString();
    }
    private void TimerIsStopped() //level state switcher
    {
        StartCoroutine(GetComponent<ButtonsAndCommands>().FadeOfSound(0f));
        levelIsStarted = false; // block for update
        makingClients = false; // block for coroutine
        isLevelEnded = true; // свойство
    }
    private void EndLevelChecking()
    {
        if (peopleInCafe == 0)
        {
            if (moneyOnTable == 0)
            {
                if (Learn.activeInHierarchy) { Learn.SetActive(false); }
                ClosePanel.GetComponent<Closed>().TheCafeIsClosing();
                SaveGame();
            }
            else
            {
                Learn.SetActive(true);
                LearnText.text = "—оберите все деньги!";
            }
        }
    }
    private void SaveGame()
    {
        if (mySalary >= RecData.plans[_level])
        {
            if (data.AvailableLevels == _level) { data.AvailableLevels++; }
            data.ContinueGame++;
        }
        if (mySalary > data.LvlRec[_level])
        {
            data.LvlRec[_level] = mySalary;
        }
        data.SaveData();
    }
}