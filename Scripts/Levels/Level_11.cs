using UnityEngine;

public class Level_11 : MonoBehaviour //ONION //только нарисовать и добавить сам лук
{
    private Game game;
    private LearningPointer lp;
    private readonly int levelNum = 10;
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
    private void Update()
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
    public void Go() => game.TheFirstFew(4, 3.2f, 3.9f, 3, levelNum);
    private void Learning()
    {
        game.learn = true;
        game.LearningPointer.SetActive(true);
        lp = game.LearningPointer.GetComponent<LearningPointer>();
        game.StoikaOnly.transform.GetChild(16).GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        lp.Press(game.StoikaOnly.transform.GetChild(7).gameObject);
        lp.WriteText("Обратите внимание на новые ингридиенты: \nбулочки и котлеты для бургеров!");
        Invoke(nameof(Learning2), 3f);
    }
    private void Learning2()
    {
        lp.Press(game.StoikaOnly.transform.GetChild(8).gameObject);
        lp.WriteText("Вы уже умеете делать хотдоги, поэтому не будем \nтратить на обучение слишком много времени. \nПринцип такой же. Удачи!");
        Invoke(nameof(AlmostGo), 4f);
    }
}
