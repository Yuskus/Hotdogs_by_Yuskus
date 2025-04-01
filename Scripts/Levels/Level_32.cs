using UnityEngine;

public class Level_32 : MonoBehaviour
{
    private Game game;

    private readonly FirstFewPeopleInfo levelInfo = new()
    {
        PeopleOnSceneMaxCount = 8,
        IntervalMin = 2.7f,
        IntervalMax = 3.5f,
        FirstFewPeopleCount = 4,
        LevelNumber = 31
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
