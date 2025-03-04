using UnityEngine;

public class Level_9 : MonoBehaviour //горчица - 1 стрелка - доступный равен 8
{
    private Game game;
    private readonly int levelNum = 8;
    private void Awake()
    {
        game = Camera.main.GetComponent<Game>();
        game.AwakeAnyLevel();
    }
    private void Start()
    {
        game.StartAnyLevel();
        game.TabloOn();
        Invoke(nameof(Go), 5f); //CHECK
    }
    private void Update() //CHECK
    {
        game.TimerForLevel();
    }
    private void Go() => game.TheFirstFew(4, 3.3f, 3.9f,2, levelNum); //CHECK
}
