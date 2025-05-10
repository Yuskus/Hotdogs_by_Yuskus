using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartKotleta : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private GameObject[] Kotletas = Array.Empty<GameObject>();

    private void Start()
    {
        MyData data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        if (data.ContinueGame < RecData.canCookBurger) { transform.gameObject.SetActive(false); }
        dg = Camera.main.GetComponent<Drag>();
        Kotletas = new GameObject[2]
        {
            transform.parent.GetChild(2).GetChild(0).gameObject,
            transform.parent.GetChild(2).GetChild(1).gameObject
        };
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            if (!Kotletas[0].activeInHierarchy)
            {
                Kotletas[0].SetActive(true);
            }
            else if (!Kotletas[1].activeInHierarchy)
            {
                Kotletas[1].SetActive(true);
            }
        }
    }
}
