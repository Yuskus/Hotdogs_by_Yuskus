using UnityEngine;

public class Level_17 : MonoBehaviour //бургер,котлета - 2 стрелки - доступный равен 4
{
    private Game game;
    private readonly int levelNum = 16;
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
    private void Go() => game.TheFirstFew(5, 3.1f, 3.7f,3, levelNum); //CHECK
}
