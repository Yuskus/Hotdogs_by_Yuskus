using UnityEngine;
using UnityEngine.EventSystems;

public class L_StartSosis : MonoBehaviour, IPointerDownHandler
{
    private Game game;
    private LearningPointer lp;
    private L_StartSosis learn;
    private void Start()
    {
        game = Camera.main.GetComponent<Game>();
        lp = game.LearningPointer.GetComponent<LearningPointer>();
        learn = GetComponent<L_StartSosis>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        CheckSosis();
    }
    private void CheckSosis()
    {
        if (game.learn)
        {
            game.StoikaOnly.transform.GetChild(5).GetComponent<BoxCollider2D>().enabled = false;
            lp.WriteText("Подождите, пока сосиска готовится.");
            lp.Press(game.PlitaSosis.transform.GetChild(0).gameObject);
            DestroyImmediate(learn);
        }
    }
}
