using UnityEngine;

public class Level_48 : MonoBehaviour
{
    private Game game;

    private readonly FirstFewPeopleInfo levelInfo = new()
    {
        PeopleOnSceneMaxCount = 10,
        IntervalMin = 2.3f,
        IntervalMax = 3.2f,
        FirstFewPeopleCount = 5,
        LevelNumber = 47
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

    public void Go() => game.TheFirstFew(levelInfo);
}
