using UnityEngine;

public class Level_33 : MonoBehaviour //горчица - 1 стрелка - доступный равен 8
{
    private Game game;
    private readonly int levelNum = 32;
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
    private void Update() //CHECK
    {
        game.TimerForLevel();
    }
    private void Go() => game.TheFirstFew(8, 2.7f, 3.5f, 4, levelNum); //CHECK
}
