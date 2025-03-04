using UnityEngine;

public class Level_6 : MonoBehaviour
{
    private Game game;
    private LearningPointer lp;
    private readonly int levelNum = 5;
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
    private void Go() => game.TheFirstFew(4, 3.3f, 4.0f,2, levelNum); //CHECK
    private void Learning()
    {
        game.learn = true;
        game.LearningPointer.SetActive(true);
        lp = game.LearningPointer.GetComponent<LearningPointer>();
        game.StoikaOnly.transform.GetChild(16).GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        lp.Press(game.StoikaOnly.transform.GetChild(13).gameObject);
        lp.WriteText("Обратите внимание на новый ингридиент: картошка! \nТеперь вы можете готовить картошку фри.");
        Invoke(nameof(AlmostGo), 4f);
    }
}
