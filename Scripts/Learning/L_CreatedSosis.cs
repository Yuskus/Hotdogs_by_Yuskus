using UnityEngine;

public class L_CreatedSosis : MonoBehaviour
{
    private LearningPointer lp;
    private Game game;
    private L_CreatedSosis learn;
    private SpriteRenderer sR;
    private bool deleteComponent;
    private void Start()
    {
        game = Camera.main.GetComponent<Game>();
        lp = game.LearningPointer.GetComponent<LearningPointer>();
        learn = GetComponent<L_CreatedSosis>();
        sR = GetComponent<SpriteRenderer>();
        deleteComponent = false;
    }
    private void Update()
    {
        CheckSosisUpd();
    }
    private void CheckSosisUpd()
    {
        if (game.learn)
        {
            if (sR.sprite.name is "Sosis2" or "Sosis3")
            {
                if (game.LearningPointer.transform.localPosition != game.StolHotDog.transform.GetChild(0).localPosition)
                {
                    lp.MovingOn();
                    lp.WriteText("Сосиска готова. \nПеретащите её в булочку, получится хотдог.");
                    deleteComponent = true;
                }
                lp.Move(game.PlitaSosis.transform.GetChild(0).gameObject, game.StolHotDog.transform.GetChild(0).gameObject, 5f);
            }
        }
        else if (!learn && deleteComponent) { DestroyImmediate(learn); }
    }
}
