using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartOnion : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private GameObject[] Onion = Array.Empty<GameObject>();
    private void Start()
    {
        MyData data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        if (data.ContinueGame < RecData.canCookOnion) { transform.gameObject.SetActive(false); }
        dg = Camera.main.GetComponent<Drag>();
        Onion = new GameObject[2]
        {
            transform.GetChild(0).GetChild(0).gameObject,
            transform.GetChild(1).GetChild(0).gameObject
        };
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            if (!Onion[0].activeInHierarchy)
            {
                Onion[0].SetActive(true);
            }
            else if (!Onion[1].activeInHierarchy)
            {
                Onion[1].SetActive(true);
            }
        }
    }
}
