using UnityEngine;

public class Level_28 : MonoBehaviour
{
    private Game game;

    private readonly FirstFewPeopleInfo levelInfo = new()
    {
        PeopleOnSceneMaxCount = 7,
        IntervalMin = 2.8f,
        IntervalMax = 3.6f,
        FirstFewPeopleCount = 3,
        LevelNumber = 27
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
