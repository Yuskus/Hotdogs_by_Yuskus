using System.Collections.Generic;
using UnityEngine;

public class CreateNewPerson : MonoBehaviour
{
    private HairList hr;
    private SpriteRenderer faceSR, hairSR;
    private int index;
    private bool iAmAMan;
    private int eyeColor;

    private void Awake()
    {
        hr = transform.parent.parent.GetComponent<HairList>();
        faceSR = transform.GetChild(1).GetComponent<SpriteRenderer>();
        hairSR = transform.GetChild(2).GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        SetMyStyle();
    }

    public void Deconstruct(out int eye, out int hair)
    {
        eye = eyeColor;
        hair = hr.hairColor[index];
    }

    private void OnDisable()
    {
        if (iAmAMan) hr.haircutsMan.Add(index);
        else hr.haircutsWoman.Add(index);
    }

    private void SetMyStyle()
    {
        eyeColor = Random.Range(0, 2); //blue or brown
        iAmAMan = transform.gameObject.CompareTag("Man");

        if (iAmAMan)
        {
            index = hr.haircutsMan[Random.Range(0, hr.haircutsMan.Count)];
            hr.haircutsMan.Remove(index);
        }
        else
        {
            index = hr.haircutsWoman[Random.Range(0, hr.haircutsWoman.Count)];
            hr.haircutsWoman.Remove(index);
        }

        hairSR.sprite = hr.hair[index];
        faceSR.sprite = hr.faces[eyeColor, hr.hairColor[index], 1];
    }
}
