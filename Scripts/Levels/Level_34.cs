using UnityEngine;

public class Level_34 : MonoBehaviour
{
    private Game game;
    private readonly int levelNum = 33;
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
    private void Go() => game.TheFirstFew(8, 2.6f, 3.5f, 4, levelNum);
}
