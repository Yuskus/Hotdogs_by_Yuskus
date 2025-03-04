using UnityEngine;

public class Level_38 : MonoBehaviour //стакан - 1 стрелка - доступный равен 1
{
    private Game game;
    private readonly int levelNum = 37;
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
    private void Go() => game.TheFirstFew(8, 2.5f, 3.4f,4, levelNum); //CHECK
}
