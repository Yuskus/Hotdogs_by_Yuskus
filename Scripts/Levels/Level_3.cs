using UnityEngine;

public class Level_3 : MonoBehaviour
{
    private Game game;

    private LearningPointer lp;

    private readonly FirstFewPeopleInfo levelInfo = new()
    {
        PeopleOnSceneMaxCount = 3,
        IntervalMin = 3.4f,
        IntervalMax = 4.0f,
        FirstFewPeopleCount = 2,
        LevelNumber = 2
    };

    private void Awake()
    {
        game = Camera.main.GetComponent<Game>();
        game.AwakeAnyLevel();
    }

    private void Start()
    {
        game.StartAnyLevel();
        MyData data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        if (levelInfo.LevelNumber == data.AvailableLevels) { Learning(); }
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

    public void AlmostGo()
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
        lp.Press(game.StoikaOnly.transform.GetChild(9).gameObject);
        lp.WriteText("�������� �������� �� ����� ����������: ���!");
        Invoke(nameof(AlmostGo), 4f);
    }
}
