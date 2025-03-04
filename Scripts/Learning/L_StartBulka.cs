using UnityEngine;
using UnityEngine.EventSystems;

public class L_StartBulka : MonoBehaviour, IPointerDownHandler
{
    private Game game;
    private LearningPointer lp;
    private L_StartBulka learn;
    private void Start()
    {
        game = Camera.main.GetComponent<Game>();
        lp = game.LearningPointer.GetComponent<LearningPointer>();
        learn = GetComponent<L_StartBulka>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        CheckBulka();
    }
    private void CheckBulka()
    {
        if (game.learn)
        {
            game.StoikaOnly.transform.GetChild(6).GetComponent<BoxCollider2D>().enabled = false;
            game.StoikaOnly.transform.GetChild(5).GetComponent<BoxCollider2D>().enabled = true;
            lp.Press(game.StoikaOnly.transform.GetChild(5).gameObject);
            lp.WriteText("Положите сосиску на плиту.");
            DestroyImmediate(learn);
        }
    }
}
