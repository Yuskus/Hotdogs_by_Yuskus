using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnyPerson : MonoBehaviour, IPointerDownHandler //RandomIndex, Timer
{
    private Game game;
    private DraggingComponent drag;
    private Drag dg;
    private HairList hr;
    private FoodCode fc;
    public int RandomIndex { get; private set; }
    private int checkMask = 1;
    private int sign, index, state, indexOfLevel; //локальные индексы для методов
    private int price, foodCode, walkLayer, standLayer, guiltyLayer, runLayer, eyeColor, eyebrowColor;
    public float Timer { get; private set; }
    private float sinusTimer, amp, freq, maxAmp, runSpeed, walkSpeed, startSpeed, limit;
    private readonly Vector2 OffPlace = new(0f, -10f);
    private Vector2 myPlace, PlaceOfMyDeath, moving;
    private bool IsWalking;
    private bool onceTimerBool, stopping, speedUp, IsItBadGuy, timerIsRunning, DidNotStopYet;
    public List<(string name, int code, int number)> myEat;
    private SpriteRenderer bodySR, hairSR, faceSR; //заполнить отсюда
    private BoxCollider2D col;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private AudioClip[] audioClip;
    private Sprite[] face;
    private enum Emotion { angry, normal, happy, shocked, thief, waiting };
    private enum Audio { evel, happy, shocked, thief, waiting };
    private void Awake()
    {
        game = Camera.main.GetComponent<Game>();
        drag = game.StoikaOnly.GetComponent<DraggingComponent>();
        dg = Camera.main.GetComponent<Drag>();
        hr = game.AllClients.GetComponent<HairList>();
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        myEat = new(5);
        if (transform.gameObject.CompareTag("Man")) { audioClip = hr.audioClipM; }
        else { audioClip = hr.audioClipW; }
        bodySR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        faceSR = transform.GetChild(1).GetComponent<SpriteRenderer>();
        hairSR = transform.GetChild(2).GetComponent<SpriteRenderer>();
        transform.gameObject.AddComponent(System.Type.GetType("Pers_" + game.indexOfLevel));
    }
    public void AlwaysAtStart(float factorOfWalkSpeed, float runSpeed, float bonusPayLimit, int procentOfGoodPeople) //при вызове клиента на сцену в методе он энейбл
    {
        transform.SetParent(game.OnScene.transform);
        transform.localPosition = Random.Range(0, 2) == 0 ? Vector2.zero : new(27, 0);
        RandomIndex = hr.randomIndex.Count == 0 ? 6 : hr.randomIndex[Random.Range(0, hr.randomIndex.Count)];
        DidNotStopYet = true;
        PlaceOfMyDeath = transform.localPosition.x == 0 ? new Vector2(27, 0) : Vector2.zero;
        if (RandomIndex != 6)
        {
            myPlace = game.PlacePeople[RandomIndex];
            hr.randomIndex.Remove(RandomIndex);
            IsItBadGuy = false;
            if (Random.Range(0, 100) >= procentOfGoodPeople && System.Math.Abs(PlaceOfMyDeath.x - myPlace.x) >= 6) { IsItBadGuy = true; }
        }
        sign = transform.localPosition.x == 0 ? 1 : -1;
        walkSpeed = sign * factorOfWalkSpeed;
        startSpeed = sign * (factorOfWalkSpeed + 1f);
        this.runSpeed = sign * runSpeed;
        InitializeMyFace();
        ForFace((int)Emotion.normal, null, false); //normal face
        audioSource.clip = null;
        Timer = 0;
        price = 0;
        state = 0;
        sinusTimer = 0;
        stopping = false;
        amp = maxAmp = 0.9f;
        freq = 5f;
        limit = bonusPayLimit;
        walkLayer = SortingLayer.NameToID("Walk"); //walkLayer //layer12
        standLayer = SortingLayer.NameToID("Stand"); //standLayer //layer15
        guiltyLayer = SortingLayer.NameToID("Guilty"); //guiltyLayer //layer16
        runLayer = SortingLayer.NameToID("Run"); //runLayer //layer18
        ChangeSpeed(startSpeed, maxAmp, walkLayer, false); //startSpeed, maxAmp, layer12, false
        speedUp = false;
    }
    private void OnEnable()
    {
        game.PeopleInCafe += 1;
    }
    public void TheClientStartedWalking() => IsWalking = true;
    public void OnPointerDown(PointerEventData eventData) //клик
    {
        if (!dg.isDragging)
        {
            ThiefIsRunning();
            Checking();
        }
    }
    private void SlowStop()
    {
        moving.x = Mathf.Lerp(moving.x, 0, Time.fixedDeltaTime / 0.5f);
        amp = Mathf.Lerp(amp, 0, Time.fixedDeltaTime / 0.5f);
        if (moving.x is > -0.3f and < 0.3f)
        {
            stopping = false;
            timerIsRunning = true;
            onceTimerBool = true;
            ChangeSpeed(0, 0, standLayer, true);
            IsWalking = false;
            DidNotStopYet = false;
            WishMaking();
        }
    }
    private void SlowStart()
    {
        moving.x = Mathf.Lerp(moving.x, walkSpeed, Time.fixedDeltaTime / 0.5f);
        amp = Mathf.Lerp(amp, maxAmp, Time.fixedDeltaTime / 0.5f);
        if ((walkSpeed - moving.x) is > -0.3f and < 0.3f) { speedUp = false; ChangeSpeed(walkSpeed, maxAmp, walkLayer, false); }
    }
    private void ChangeSpeed(float newSpeed, float newAmp, int layer, bool box)
    {
        moving.x = newSpeed; //отследить как 0 скорость назначается
        amp = newAmp;
        if (newSpeed == 0) { moving.y = 0f; }
        OnGoing(layer, box);
    }
    private void OnGoing(int layer, bool box)
    {
        bodySR.sortingLayerID = layer;
        faceSR.sortingLayerID = layer;
        hairSR.sortingLayerID = layer;
        col.enabled = box;
    }
    public void Walking() //только хождение и физика
    {
        if (IsWalking)
        {
            sinusTimer += Time.fixedDeltaTime;
            if (speedUp && !DidNotStopYet) { SlowStart(); }
            else if (stopping && DidNotStopYet) { SlowStop(); }
            moving.y = amp * Mathf.Sin(sinusTimer * freq);
        }
        rb.velocity = moving;
    }
    public void StoppedOrNot() //только фактические назначения относительно ходьбы (все закрыты)
    {
        if (DidNotStopYet)
        {
            if (!stopping && System.Math.Abs(transform.localPosition.x - myPlace.x) < 1.5f) { stopping = true; }
        }
        else if (timerIsRunning)
        {
            Timer += Time.deltaTime;
            if (onceTimerBool && Timer > limit) { onceTimerBool = false; }
        }
    }
    public void Homecoming()
    {
        if (System.Math.Abs(transform.localPosition.x - 14f) > 14f)
        {
            transform.localPosition = OffPlace;
            transform.SetParent(game.AtHome.transform);
            transform.gameObject.SetActive(false);
            game.PeopleInCafe -= 1;
        }
    }
    public void ForAngryClient()
    {
        if (state != 3)
        {
            myEat.Clear();
            CleanWishesForAngryPerson();
            if (IsItBadGuy) { IsItBadGuy = false; }
            IsItEnded((int)Emotion.angry,  (int)Audio.evel, false); //angry face
            hr.randomIndex.Add(RandomIndex);
            timerIsRunning = false;
            state = 3;
        }
    }
    public void ForWaitingClient(int face, int audio, int i)
    {
        if (state != i)
        {
            price -= 5;
            ForFace(face, audioClip[audio], true);
            state = i;
        }
    }
    private void CleanWishesForAngryPerson()
    {
        for (int i = 0; i < 5; i++) { game.WishCloud.transform.GetChild(RandomIndex).GetChild(i).GetComponent<CleanWishes>().OffEatPhase(); }
    }
    public bool TheClientLeaves() //protects value
    {
        return myEat.Count == 0 && !DidNotStopYet;
    }
    public void InitializeMyFace() //protects value
    {
        (eyeColor, eyebrowColor) = GetComponent<CreateNewPerson>();
        face = new Sprite[6];
        for (int i = 0; i < face.Length; i++)
        {
            face[i] = hr.faces[eyeColor, eyebrowColor, i];
        }
    }
    private void WishMaking()
    {
        game.WishCloud.transform.GetChild(RandomIndex).gameObject.SetActive(true);
        for (int i = 0; i < myEat.Count; i++)
        {
            game.WishCloud.transform.GetChild(RandomIndex).GetChild(i).GetComponent<SpriteRenderer>().sprite = game.ForWish[myEat[i].name];
            if (myEat[i].code != 0)
            {
                switch (myEat[i].name)
                {
                    case "HotDog": ChildrenOfEatPhase(i, drag.HD_added); break;
                    case "Burger": ChildrenOfEatPhase(i, drag.B_added); break;
                }
            }
        }
    }
    private void ChildrenOfEatPhase(int num, Sprite[] sprites)
    {
        checkMask = 1;
        for (int a = 0; a < 3; a++)
        {
            if ((myEat[num].code & checkMask) == checkMask) { game.WishCloud.transform.GetChild(RandomIndex).GetChild(num).GetChild(a).GetComponent<SpriteRenderer>().sprite = sprites[a]; }
            checkMask <<= 1;
        }
    }
    public void RandomWish(int r, int option) //opt = 2(k), 4(kg), 8(onion) //DONE
    {
        if (game.ForWish.ContainsKey(game.forEatPhase[r]))
        {
            switch (game.forEatPhase[r])
            {
                case "HotDog": myEat.Add((game.forEatPhase[r], Random.Range(0, option) + RecData.Code_Doneness + RecData.Code_HotDog, myEat.Count)); break;
                case "Burger": myEat.Add((game.forEatPhase[r], Random.Range(0, option) + RecData.Code_Doneness + RecData.Code_Burger, myEat.Count)); break;
                default: myEat.Add((game.forEatPhase[r], 0, myEat.Count)); break;
            }
        }
    }
    private void ThiefIsRunning()
    {
        if (moving.x == runSpeed)
        {
            ForFace((int)Emotion.waiting, audioClip[(int)Audio.waiting], true); //waiting face
            GameObject Money = Instantiate(game.Money, game.Interactive.transform.GetChild(2).gameObject.transform);
            Money.transform.localPosition = new(Mathf.Clamp(transform.localPosition.x - 14f, -10f, 10f), 2.75f);
            Money.GetComponent<Money>().WritingInPrice(price + 30);
            Invoke(nameof(OkYouAreFree), 1.5f);
            ChangeSpeed(0, 0, guiltyLayer, false);
            IsWalking = false;
        }
    }
    private void OkYouAreFree()
    {
        speedUp = true;
        OnGoing(walkLayer, false);
        IsWalking = true;
    }
    public void CheckingForDrags()
    {
        if (dg.isDragging) { Checking(); }
    }
    private void Checking()
    {
        if (!IsWalking && dg.SelectedObject.TryGetComponent(out fc))
        {
            foodCode = fc.Code();
            index = myEat.Exists(tuple => tuple.code == foodCode && foodCode != 0) ? myEat.FindIndex(tuple => tuple.code == foodCode) : myEat.FindIndex(tuple => tuple.name == dg.SelectedObject.name);
            if (index == -1) { dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAsSelected(); return; }
            switch (dg.SelectedObject.name)
            {
                case "HotDog": PriceBonus(RecData.Code_HotDog); break;
                case "Burger": PriceBonus(RecData.Code_Burger); break;
            }
            WishListCheck();
        }
        else if (dg.SelectedObject != dg.Zero) { dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAsSelected(); }
    }
    private void PriceBonus(int code)
    {
        price += myEat[index].code == foodCode ? 5 : 0;
        price += game.bonusPrice[myEat[index].code & (foodCode - code)];
    }
    private void WishListCheck()
    {
        game.WishCloud.transform.GetChild(RandomIndex).GetChild(myEat[index].number).GetComponent<CleanWishes>().OffEatPhase(); //CHECK
        price += game.priceOfFood[dg.SelectedObject.name];
        myEat.RemoveAt(index);
        Timer = 0;
        if (myEat.Count == 0)
        {
            if (IsItBadGuy) IsItEnded((int)Emotion.thief, (int)Audio.thief, false);
            else IsItEnded((int)Emotion.happy, (int)Audio.happy, true);
        }
        else { ForFace((int)Emotion.normal, null, false); }
        Back();
    }
    private void Back()
    {
        dg.SelectedObject.GetComponent<MyStartPlace>().Back();
        if (dg.SelectedObject.name == "Free")
        {
            for (int i = 0; i < 3; i++)
            {
                if (game.StoikaOnly.transform.GetChild(14).GetChild(i).gameObject.activeInHierarchy) { dg.SelectedObject = game.StoikaOnly.transform.GetChild(14).GetChild(i).gameObject; return; }
            }
        }
        if (dg.SelectedObject.name is not "DrinkClone" or "ColaClone") dg.SelectedObject = dg.Zero;
    }
    private void IsItEnded(int face, int audio, bool money) //change and check
    {
        if (!IsWalking)
        {
            timerIsRunning = false;
            if (onceTimerBool) { price += price / 10; }
            game.WishCloud.transform.GetChild(RandomIndex).gameObject.SetActive(false); //облачко выключается
            ForFace(face, audioClip[audio], true);
            game.Interactive.transform.GetChild(1).GetChild(RandomIndex).GetChild(0).gameObject.SetActive(money); //денежки либо включаются либо нет
            if (IsItBadGuy)
            {
                ChangeSpeed(runSpeed, maxAmp, runLayer, true);
                hr.randomIndex.Add(RandomIndex);
            }
            else
            {
                game.Interactive.transform.GetChild(1).GetChild(RandomIndex).GetComponent<ParMoney>().WritingInPrice(price);
                speedUp = true;
                OnGoing(walkLayer, false);
            }
            IsWalking = true;
        }
    }
    private void ForFace(int emotion, AudioClip clip, bool play)
    {
        faceSR.sprite = face[emotion];
        audioSource.clip = clip;
        if (play) { audioSource.Play(); }
    }
} //397
