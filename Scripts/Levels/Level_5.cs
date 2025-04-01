using UnityEngine;

public class Level_5 : MonoBehaviour //бургер,котлета - 2 стрелки - доступный равен 4
{
    private Game game;

    private readonly FirstFewPeopleInfo levelInfo = new()
    {
        PeopleOnSceneMaxCount = 3,
        IntervalMin = 3.4f,
        IntervalMax = 4.0f,
        FirstFewPeopleCount = 2,
        LevelNumber = 4
    };

    private void Awake()
    {
        game = Camera.main.GetComponent<Game>();
        game.AwakeAnyLevel();
    }

    private void Start()
    {
        game.StartAnyLevel();
        game.TabloOn();
        Invoke(nameof(Go), 5f);
    }

    private void Update()
    {
        game.TimerForLevel();
    }

    private void Go() => game.TheFirstFew(levelInfo);
}
