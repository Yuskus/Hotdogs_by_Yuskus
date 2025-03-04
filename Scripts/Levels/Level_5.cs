using UnityEngine;

public class Level_5 : MonoBehaviour //бургер,котлета - 2 стрелки - доступный равен 4
{
    private Game game;
    private readonly int levelNum = 4;
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
    private void Go() => game.TheFirstFew(3, 3.4f, 4.0f,2, levelNum); //CHECK
}
