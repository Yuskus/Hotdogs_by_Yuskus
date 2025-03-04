using UnityEngine;

public class CleanWishes : MonoBehaviour
{
    private SpriteRenderer mySR;
    private SpriteRenderer[] childSR;
    private void Awake()
    {
        childSR = new SpriteRenderer[3];
        mySR = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 3; i++) { childSR[i] = transform.GetChild(i).GetComponent<SpriteRenderer>(); }
    }
    public void OffEatPhase()
    {
        mySR.sprite = null;
        for (int i = 0; i < 3; i++) { childSR[i].sprite = null; }
    }
}
