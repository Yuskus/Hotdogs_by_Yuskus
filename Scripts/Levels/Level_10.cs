using UnityEngine;

public class Level_10 : MonoBehaviour
{
    private Game game;
    private readonly int levelNum = 9;
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
    private void Update()
    {
        game.TimerForLevel();
    }
    private void Go() => game.TheFirstFew(4, 3.2f, 3.9f,2, levelNum);
}
