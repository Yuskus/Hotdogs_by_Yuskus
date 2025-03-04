using UnityEngine;

public class Level_22 : MonoBehaviour
{
    private Game game;
    private readonly int levelNum = 21;
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
    private void Go() => game.TheFirstFew(6, 2.9f, 3.6f,3, levelNum);
}
