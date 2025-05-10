using System;
using UnityEngine;

public class CleanWishes : MonoBehaviour
{
    private SpriteRenderer mySR;
    private SpriteRenderer[] childSR = Array.Empty<SpriteRenderer>();

    private void Awake()
    {
        mySR = GetComponent<SpriteRenderer>();
        childSR = new SpriteRenderer[3]
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>(),
            transform.GetChild(1).GetComponent<SpriteRenderer>(),
            transform.GetChild(2).GetComponent<SpriteRenderer>()
        };
    }

    public void OffEatPhase()
    {
        mySR.sprite = null;

        childSR[0].sprite = null;
        childSR[1].sprite = null;
        childSR[2].sprite = null;
    }
}
