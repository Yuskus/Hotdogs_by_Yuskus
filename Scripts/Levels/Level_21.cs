using UnityEngine;

public class Level_21 : MonoBehaviour //горчица - 1 стрелка - доступный равен 8
{
    private Game game;
    private LearningPointer lp;
    private readonly int levelNum = 20;
    private void Awake()
    {
        game = Camera.main.GetComponent<Game>();
        game.AwakeAnyLevel();
    }
    private void Start()
    {
        game.StartAnyLevel();
        if (levelNum == Game.TimelyAvailable) { Learning(); }
        else
        {
            game.TabloOn();
            Invoke(nameof(Go), 5f);
        }
    }
    private void Update() //CHECK
    {
        game.TimerForLevel();
    }
    private void AlmostGo()
    {
        lp.TurnLearnOff();
        game.LearningPointer.SetActive(false);
        game.learn = false;
        game.TabloOn();
        game.StoikaOnly.transform.GetChild(16).GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        Invoke(nameof(Go), 4f);
    }
    private void Go() => game.TheFirstFew(6, 3f, 3.7f,3, levelNum); //CHECK
    private void Learning()
    {
        game.learn = true;
        game.LearningPointer.SetActive(true);
        lp = game.LearningPointer.GetComponent<LearningPointer>();
        game.StoikaOnly.transform.GetChild(16).GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        lp.Press(game.SauceG);
        lp.WriteText("Обратите внимание на новый ингридиент: горчица!");
        Invoke(nameof(AlmostGo), 3.5f);
    }
}
