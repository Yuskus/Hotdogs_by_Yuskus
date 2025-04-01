using UnityEngine;

public class Level_21 : MonoBehaviour //горчица - 1 стрелка - доступный равен 8
{
    private Game game;

    private LearningPointer lp;

    private readonly FirstFewPeopleInfo levelInfo = new()
    {
        PeopleOnSceneMaxCount = 6,
        IntervalMin = 3.0f,
        IntervalMax = 3.7f,
        FirstFewPeopleCount = 3,
        LevelNumber = 20
    };

    private void Awake()
    {
        game = Camera.main.GetComponent<Game>();
        game.AwakeAnyLevel();
    }

    private void Start()
    {
        game.StartAnyLevel();
        if (levelInfo.LevelNumber == Game.TimelyAvailable) { Learning(); }
        else
        {
            game.TabloOn();
            Invoke(nameof(Go), 5f);
        }
    }

    private void Update() //CHECK
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
        lp.Press(game.SauceG);
        lp.WriteText("Обратите внимание на новый ингридиент: горчица!");
        Invoke(nameof(AlmostGo), 3.5f);
    }
}
