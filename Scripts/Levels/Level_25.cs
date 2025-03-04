using UnityEngine;

public class Level_25 : MonoBehaviour //кетчуп,сосиски,булки - 3 стрелки - доступный равен 0
{
    private Game game;
    private readonly int levelNum = 24;
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
    public void Go() => game.TheFirstFew(6, 2.9f, 3.6f, 3, levelNum);
}
