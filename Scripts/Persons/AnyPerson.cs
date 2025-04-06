using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnyPerson : MonoBehaviour, IPointerDownHandler
{
    private Game game;
    private MyData data;
    private DraggingComponent drag;
    private Drag dg;
    private HairList hr;

    private GameObject AllClients;
    private GameObject AtHome, OnScene;
    public int RandomIndex { get; private set; }
    private int sign, index, state; //локальные индексы для методов
    private int price, walkLayer, standLayer, guiltyLayer, runLayer, eyeColor, eyebrowColor;
    public float Timer { get; private set; }
    private float sinusTimer, amp, freq, maxAmp, runSpeed, walkSpeed, startSpeed, limit;
    private readonly Vector2 OffPlace = new(0f, -10f);
    private Vector2 myPlace, PlaceOfMyDeath, moving;
    private bool IsWalking;
    private bool onceTimerBool, stopping, speedUp, IsItBadGuy, timerIsRunning, DidNotStopYet;
    private List<(string name, int code, int number)> myEat;

    private SpriteRenderer bodySR, hairSR, faceSR;
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
        data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        drag = GameObject.FindGameObjectWithTag("Table").GetComponent<DraggingComponent>();
        dg = Camera.main.GetComponent<Drag>();
        AllClients = GameObject.FindGameObjectWithTag("Player");
        AtHome = AllClients.transform.GetChild(0).gameObject;
        OnScene = AllClients.transform.GetChild(1).gameObject;
        hr = AllClients.GetComponent<HairList>();
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        myEat = new(5);
        if (transform.gameObject.CompareTag("Man")) { audioClip = hr.audioClipM; }
        else { audioClip = hr.audioClipW; }
        bodySR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        faceSR = transform.GetChild(1).GetComponent<SpriteRenderer>();
        hairSR = transform.GetChild(2).GetComponent<SpriteRenderer>();
        transform.gameObject.AddComponent(System.Type.GetType($"Pers_{data.ContinueGame + 1}"));
    }

    public void AlwaysAtStart(float factorOfWalkSpeed, float runSpeed, float bonusPayLimit, int procentOfGoodPeople) //при вызове клиента на сцену в методе он энейбл
    {
        transform.SetParent(OnScene.transform);
        transform.localPosition = Random.Range(0, 2) == 0 ? Vector2.zero : new(27, 0);
        RandomIndex = hr.randomIndex.Count == 0 ? 6 : hr.randomIndex[Random.Range(0, hr.randomIndex.Count)];
        DidNotStopYet = true;
        moving = Vector2.zero;
        PlaceOfMyDeath = transform.localPosition.x == 0 ? new Vector2(27, 0) : Vector2.zero;

        if (RandomIndex != 6)
        {
            myPlace = game.PlacePeople[RandomIndex];
            hr.randomIndex.Remove(RandomIndex);
            IsItBadGuy = Random.Range(0, 100) >= procentOfGoodPeople && System.Math.Abs(PlaceOfMyDeath.x - myPlace.x) >= 6;
        }

        sign = transform.localPosition.x == 0 ? 1 : -1;
        walkSpeed = sign * factorOfWalkSpeed;
        startSpeed = sign * (factorOfWalkSpeed + 1f);
        this.runSpeed = sign * runSpeed;
        InitializeMyFace();
        ChangeFace(Emotion.normal);
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
            CatchThief();
            Checking();
        }
    }

    private void SlowStop()
    {
        moving.x = Mathf.Lerp(moving.x, 0, Time.fixedDeltaTime / 0.5f);
        amp = Mathf.Lerp(amp, 0, Time.fixedDeltaTime / 0.5f);

        if (moving.x > -0.3f && moving.x < 0.3f)
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
        float difference = walkSpeed - moving.x;

        if (difference > -0.3f && difference < 0.3f)
        {
            speedUp = false;
            ChangeSpeed(walkSpeed, maxAmp, walkLayer, false);
        }
    }

    private void ChangeSpeed(float newSpeed, float newAmp, int layer, bool box)
    {
        moving.x = newSpeed;
        amp = newAmp;
        if (newSpeed == 0) moving.y = 0f;
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

            if (speedUp && !DidNotStopYet) SlowStart();
            else if (stopping && DidNotStopYet) SlowStop();

            moving.y = amp * Mathf.Sin(sinusTimer * freq);
        }

        rb.velocity = moving;
    }

    public void StoppedOrNot() //только фактические назначения относительно ходьбы (все закрыты)
    {
        if (DidNotStopYet)
        {
            if (!stopping && System.Math.Abs(transform.localPosition.x - myPlace.x) < 1.5f)
            {
                stopping = true;
            }
        }
        else if (timerIsRunning)
        {
            Timer += Time.deltaTime;

            if (onceTimerBool && Timer > limit)
            {
                onceTimerBool = false;
            }
        }
    }

    public void Homecoming()
    {
        if (System.Math.Abs(transform.localPosition.x - 14f) > 14f)
        {
            transform.localPosition = OffPlace;
            transform.SetParent(game.AtHome.transform);
            game.PeopleInCafe -= 1;
            transform.gameObject.SetActive(false);
        }
    }

    public void ForAngryClient()
    {
        if (state != 3)
        {
            CleanWishesForAngryPerson();
            IsItBadGuy = false;
            IsItEnded((int)Emotion.angry, (int)Audio.evel, false); //angry face
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
            ChangeFace((Emotion)face, (Audio)audio);
            state = i;
        }
    }

    private void CleanWishesForAngryPerson()
    {
        myEat.Clear();

        for (int i = 0; i < 5; i++)
        {
            game.WishCloud.transform.GetChild(RandomIndex)
                                    .GetChild(i)
                                    .GetComponent<CleanWishes>()
                                    .OffEatPhase();
        }
    }

    public bool TheClientLeaves()
    {
        return myEat.Count == 0 && !DidNotStopYet;
    }

    private void InitializeMyFace()
    {
        (eyeColor, eyebrowColor) = GetComponent<CreateNewPerson>(); // deconstruct
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
            game.WishCloud.transform.GetChild(RandomIndex)
                                    .GetChild(i)
                                    .GetComponent<SpriteRenderer>()
                                    .sprite = game.ForWish[myEat[i].name];

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
        for (int i = 0, checkMask = 1; i < 3; i++)
        {
            if ((myEat[num].code & checkMask) == checkMask)
            {
                game.WishCloud.transform.GetChild(RandomIndex)
                                        .GetChild(num)
                                        .GetChild(i)
                                        .GetComponent<SpriteRenderer>()
                                        .sprite = sprites[i];
            }

            checkMask <<= 1;
        }
    }

    public void RandomWish(int randomNumber, int maxOprion) //maxOprion = 2(k), 4(k, g), 8(k, g, o)
    {
        if (game.ForWish.ContainsKey(game.FoodForRandom[randomNumber]))
        {
            int foodCode = 0;

            // рандомные топпинги
            switch (game.FoodForRandom[randomNumber])
            {
                case "HotDog": foodCode = Random.Range(0, maxOprion) + RecData.Code_Doneness + RecData.Code_HotDog; break;
                case "Burger": foodCode = Random.Range(0, maxOprion) + RecData.Code_Doneness + RecData.Code_Burger; break;
            }

            myEat.Add((game.FoodForRandom[randomNumber], foodCode, myEat.Count));
        }
    }

    private void CatchThief()
    {
        if (moving.x == runSpeed)
        {
            ChangeFace(Emotion.waiting, Audio.waiting);
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
        if (dg.isDragging) Checking();
    }

    private void Checking()
    {
        // если клиент стоит и выбранный объект имеет фудкод (фудкоды отличаются от нуля в случае если это хотдог или бургер)
        if (!IsWalking && dg.SelectedObject.TryGetComponent(out FoodCode foodCode))
        {
            // есть ли еда в списке желаний
            index = myEat.Exists(tuple => tuple.code == foodCode.Index && foodCode.Index != 0) // что-то конкретно соответствует по фудкоду? (для хотдогов или бургеров)
                  ? myEat.FindIndex(tuple => tuple.code == foodCode.Index) // тогда выбираем по фудкоду
                  : myEat.FindIndex(tuple => tuple.name == dg.SelectedObject.name); // иначе - по имени

            // если клиент это не просил - прерывание
            if (index == -1)
            {
                dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAsSelected();
                return;
            }
            
            // начисление основной суммы
            price += game.priceOfFood[dg.SelectedObject.name];

            // начисление бонусов, если еда соответсвует ожиданиям клиента (только для хотдогов и бургеров)
            switch (dg.SelectedObject.name)
            {
                case "HotDog":
                    price += game.bonusPrice[myEat[index].code & (foodCode.Index - RecData.Code_HotDog)]; // плата за топпинг
                    price += myEat[index].code == foodCode.Index ? 5 : 0; // полное соответствие даёт дополнительно +5
                    break;
                case "Burger":
                    price += game.bonusPrice[myEat[index].code & (foodCode.Index - RecData.Code_Burger)]; // плата за топпинг
                    price += myEat[index].code == foodCode.Index ? 5 : 0; // полное соответствие даёт дополнительно +5
                    break;
            }

            // убираем эту еду из облачка желаний
            game.WishCloud.transform.GetChild(RandomIndex)
                                    .GetChild(myEat[index].number)
                                    .GetComponent<CleanWishes>()
                                    .OffEatPhase();

            myEat.RemoveAt(index); // удаляем еду из списка желаний клиента
            Timer = 0; // обнуляем таймер (чтоб не злился)

            // меняем выражение лица и проверяем, не последнее ли это желание
            if (myEat.Count > 0)
                ChangeFace(Emotion.normal);
            else if (IsItBadGuy)
                IsItEnded(Emotion.thief, Audio.thief, false);
            else
                IsItEnded(Emotion.happy, Audio.happy, true);

            // объект возвращается на место соответствующим способом, вызывая делегат Back()
            dg.SelectedObject.GetComponent<MyStartPlace>().Back();

            // чем будет SelectedObject в определенных случаях
            switch (dg.SelectedObject.name)
            {
                case "DrinkClone": return; // выбор остаётся на соке
                case "ColaClone": return; // выбор остаётся на коле
                case "Free":
                    for (int i = 0; i < 3; i++)
                    {
                        if (game.StoikaOnly.transform.GetChild(14).GetChild(i).gameObject.activeInHierarchy)
                        {
                            // в случае с картошкой - выбранной становится соседняя картошка (если есть хоть одна активная)
                            dg.SelectedObject = game.StoikaOnly.transform.GetChild(14).GetChild(i).gameObject;
                            return;
                        }
                    }
                    break;
            }

            // для всех остальных случаев - обнуление
            dg.SelectedObject = dg.Zero;
        }
        else if (dg.SelectedObject != dg.Zero) // на случай если мы перетащили на человека сосиску/котлетку/лучок
        {
            dg.SelectedObject.GetComponent<MyStartPlace>()
                             .BackHomeAsSelected();
        }
    }

    private void IsItEnded(Emotion emotion, Audio audio, bool money) //change and check
    {
        if (!IsWalking)
        {
            timerIsRunning = false;
            if (onceTimerBool) price += price / 10;

            ChangeFace(emotion, audio);
            game.WishCloud.transform.GetChild(RandomIndex).gameObject.SetActive(false); //облачко выключается
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

    private void ChangeFace(Emotion emotion, Audio audio)
    {
        faceSR.sprite = face[(int)emotion];
        audioSource.clip = audioClip[(int)audio];
        audioSource.Play();
    }

    private void ChangeFace(Emotion emotion)
    {
        faceSR.sprite = face[(int)emotion];
    }
} //397