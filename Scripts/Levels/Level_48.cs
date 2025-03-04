using UnityEngine;

public class Level_48 : MonoBehaviour //COLA  //вроде готово
{
    private Game game;
    private readonly int levelNum = 47;
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
    public void Go() => game.TheFirstFew(10, 2.3f, 3.2f, 5, levelNum);
}
