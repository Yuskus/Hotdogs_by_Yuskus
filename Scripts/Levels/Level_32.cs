using UnityEngine;

public class Level_32 : MonoBehaviour
{
    private Game game;
    private readonly int levelNum = 31;
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
    private void Go() => game.TheFirstFew(8, 2.7f, 3.5f, 4, levelNum); //CHECK
}
