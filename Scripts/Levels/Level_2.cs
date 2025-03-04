using UnityEngine;

public class Level_2 : MonoBehaviour //стакан - 1 стрелка - доступный равен 1
{
    private Game game;
    private readonly int levelNum = 1;
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
    private void Go() => game.TheFirstFew(3, 3.4f, 4.1f, 2, levelNum); //CHECK
}
