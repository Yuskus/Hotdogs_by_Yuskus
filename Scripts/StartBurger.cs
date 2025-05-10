using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartBurger : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private GameObject[] Burgers = Array.Empty<GameObject>();
    private void Start()
    {
        MyData data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        if (data.ContinueGame < RecData.canCookBurger) { transform.gameObject.SetActive(false); }
        dg = Camera.main.GetComponent<Drag>();
        Burgers = new GameObject[2]
        {
            transform.parent.GetChild(4).GetChild(0).gameObject,
            transform.parent.GetChild(4).GetChild(1).gameObject
        };
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            if (!Burgers[0].activeInHierarchy)
            {
                Burgers[0].SetActive(true);
            }
            else if (!Burgers[1].activeInHierarchy)
            {
                Burgers[1].SetActive(true);
            }
        }
    }
}
