using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartBulka : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private GameObject[] HotDogs = Array.Empty<GameObject>();
    private void Start()
    {
        dg = Camera.main.GetComponent<Drag>();
        MyData data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        HotDogs = new GameObject[3] 
        {
            transform.parent.GetChild(3).GetChild(0).gameObject,
            transform.parent.GetChild(3).GetChild(1).gameObject,
            transform.parent.GetChild(3).GetChild(2).gameObject,
        };
        if (data.ContinueGame == 0 && data.AvailableLevels == 0) { transform.gameObject.AddComponent<L_StartBulka>(); }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            if (!HotDogs[0].activeInHierarchy)
            {
                HotDogs[0].SetActive(true);
            }
            else if (!HotDogs[1].activeInHierarchy)
            {
                HotDogs[1].SetActive(true);
            }
            else if (!HotDogs[2].activeInHierarchy)
            {
                HotDogs[2].SetActive(true);
            }
        }
    }
}
