using UnityEngine;

public class Level_11 : MonoBehaviour
{
    private Game game;
    private MyData data;

    private LearningPointer lp;

    private readonly FirstFewPeopleInfo levelInfo = new()
    {
        PeopleOnSceneMaxCount = 4,
        IntervalMin = 3.2f,
        IntervalMax = 3.9f,
        FirstFewPeopleCount = 3,
        LevelNumber = 10
    };

    private void Awake()
    {
        game = Camera.main.GetComponent<Game>();
        game.AwakeAnyLevel();
    }

    private void Start()
    {
        game.StartAnyLevel();
        data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        if (levelInfo.LevelNumber == data.AvailableLevels) { Learning(); }
        else
        {
            game.TabloOn();
            Invoke(nameof(Go), 5f);
        }
    }

    private void Update()
    {
        game.TimerForLevel();
    }

    private void AlmostGo()
    {
        lp.TurnLearnOff();
        game.LearningPointer.SetActive(false);
        game.learn = false;
        game.TabloOn();
        game.StoikaOnly.transform.GetChild(16).GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        Invoke(nameof(Go), 4f);
    }

    private void Go() => game.TheFirstFew(levelInfo);

    private void Learning()
    {
        game.learn = true;
        game.LearningPointer.SetActive(true);
        lp = game.LearningPointer.GetComponent<LearningPointer>();
        game.StoikaOnly.transform.GetChild(16).GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        lp.Press(game.StoikaOnly.transform.GetChild(7).gameObject);
        lp.WriteText("Обратите внимание на новые ингредиенты: \nбулочки и котлеты для бургеров!");
        Invoke(nameof(Learning2), 3f);
    }

    private void Learning2()
    {
        lp.Press(game.StoikaOnly.transform.GetChild(8).gameObject);
        lp.WriteText("Вы уже умеете делать хотдоги, поэтому не будем \nтратить на обучение слишком много времени. \nПринцип такой же. Удачи!");
        Invoke(nameof(AlmostGo), 4f);
    }
}
