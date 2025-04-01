using UnityEngine;

public class Level_13 : MonoBehaviour
{
    private Game game;

    private readonly FirstFewPeopleInfo levelInfo = new()
    {
        PeopleOnSceneMaxCount = 4,
        IntervalMin = 3.2f,
        IntervalMax = 3.8f,
        FirstFewPeopleCount = 3,
        LevelNumber = 12
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
