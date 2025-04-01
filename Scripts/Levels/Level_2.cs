using UnityEngine;

public class Level_2 : MonoBehaviour //стакан - 1 стрелка - доступный равен 1
{
    private Game game;

    private readonly FirstFewPeopleInfo levelInfo = new()
    {
        PeopleOnSceneMaxCount = 3,
        IntervalMin = 3.4f,
        IntervalMax = 4.1f,
        FirstFewPeopleCount = 2,
        LevelNumber = 1
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
