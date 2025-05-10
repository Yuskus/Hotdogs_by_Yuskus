using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartSosis : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private GameObject[] Sosiski = Array.Empty<GameObject>();
    private void Start()
    {
        MyData data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        dg = Camera.main.GetComponent<Drag>();
        Sosiski = new GameObject[3]
        {
            transform.parent.GetChild(1).GetChild(0).gameObject,
            transform.parent.GetChild(1).GetChild(1).gameObject,
            transform.parent.GetChild(1).GetChild(2).gameObject
        };
        if (data.ContinueGame == 0 && data.AvailableLevels == 0) { transform.gameObject.AddComponent<L_StartSosis>(); }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            if (!Sosiski[0].activeInHierarchy)
            {
                Sosiski[0].SetActive(true);
            }
            else if (!Sosiski[1].activeInHierarchy)
            {
                Sosiski[1].SetActive(true);
            }
            else if(!Sosiski[2].activeInHierarchy)
            {
                Sosiski[2].SetActive(true);
            }
        }
    }
}
