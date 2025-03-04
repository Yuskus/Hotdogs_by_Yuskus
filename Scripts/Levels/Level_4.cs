using UnityEngine;

public class Level_4 : MonoBehaviour //фри - 1 стрелка - доступный равен 3
{
    private Game game;
    private readonly int levelNum = 3;
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
    private void Go() => game.TheFirstFew(3, 3.4f, 4.0f,2, levelNum); //CHECK
}
