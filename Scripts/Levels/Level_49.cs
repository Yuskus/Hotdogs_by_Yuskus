using UnityEngine;

public class Level_49 : MonoBehaviour //ONION //только нарисовать и добавить сам лук
{
    private Game game;
    private readonly int levelNum = 48;
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
    private void Update()
    {
        game.TimerForLevel();
    }
    public void Go() => game.TheFirstFew(10, 2.2f, 3.1f, 5, levelNum);
}
