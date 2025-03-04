using UnityEngine;

public class Level_40 : MonoBehaviour //фри - 1 стрелка - доступный равен 3
{
    private Game game;
    private readonly int levelNum = 39;
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
    private void Go() => game.TheFirstFew(8, 2.5f, 3.4f, 4, levelNum); //CHECK
}
