using UnityEngine;

public class Level_31 : MonoBehaviour
{
    private Game game;
    private MyData data;

    private LearningPointer lp;

    private readonly FirstFewPeopleInfo levelInfo = new()
    {
        PeopleOnSceneMaxCount = 8,
        IntervalMin = 2.7f,
        IntervalMax = 3.5f,
        FirstFewPeopleCount = 4,
        LevelNumber = 30
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

    private void AlmostGo()
    {
        lp.TurnLearnOff();
        game.LearningPointer.SetActive(false);
        game.learn = false;
        game.TabloOn();
        game.StoikaOnly.transform.GetChild(16).GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        Invoke(nameof(Go), 4f);
    }


    private void Learning()
    {
        game.learn = true;
        game.LearningPointer.SetActive(true);
        lp = game.LearningPointer.GetComponent<LearningPointer>();
        game.StoikaOnly.transform.GetChild(16).GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        lp.Press(game.StoikaOnly.transform.GetChild(19).gameObject);
        lp.WriteText("Обратите внимание на новый ингридиент: лук!");
        Invoke(nameof(AlmostGo), 4f);
    }

    private void Update()
    {
        game.TimerForLevel();
    }

    private void Go() => game.TheFirstFew(levelInfo);
}
