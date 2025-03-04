using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_CreatedBulka : MonoBehaviour
{
    private Game game;
    private LearningPointer lp;
    private L_CreatedBulka learn;
    private SpriteRenderer spRen;
    private void Start()
    {
        game = Camera.main.GetComponent<Game>();
        lp = game.LearningPointer.GetComponent<LearningPointer>();
        learn = GetComponent<L_CreatedBulka>();
        spRen = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        CheckBulkaUpd();
    }
    private void CheckBulkaUpd()
    {
        if (game.learn)
        {
            if (transform.GetChild(0).gameObject.activeInHierarchy)
            {
                game.StoikaOnly.transform.GetChild(5).GetComponent<BoxCollider2D>().enabled = true;
                game.StoikaOnly.transform.GetChild(6).GetComponent<BoxCollider2D>().enabled = true;
                lp.WriteText("Готово! Теперь вы всё умеете. \nА теперь за работу! :)");
                lp.Press(transform.gameObject);
                Invoke(nameof(GoGame), 3.5f);
                game.learn = false;
            }
            else if (spRen.sprite.name is "HotDog0" or "HotDog1" or "HotDog2" or "HotDog3")
            {
                if (game.SauceK.GetComponent<BoxCollider2D>().enabled == false)
                {
                    game.SauceK.GetComponent<BoxCollider2D>().enabled = true;
                    lp.OnlyPress(game.SauceK);
                    lp.WriteText("Налейте на хотдог кетчуп.");
                }
                lp.Move(game.SauceK, transform.gameObject, 3.5f);
            }
        }
    }
    private void GoGame()
    {
        Camera.main.GetComponent<Level_1>().AlmostGo();
        DestroyImmediate(learn);
    }
}
