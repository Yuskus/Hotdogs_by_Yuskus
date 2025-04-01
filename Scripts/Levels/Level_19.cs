using UnityEngine;

public class Level_19 : MonoBehaviour
{
    private Game game;

    private readonly FirstFewPeopleInfo levelInfo = new()
    {
        PeopleOnSceneMaxCount = 5,
        IntervalMin = 3.0f,
        IntervalMax = 3.7f,
        FirstFewPeopleCount = 3,
        LevelNumber = 18
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
