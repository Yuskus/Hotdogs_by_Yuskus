using UnityEngine;
using UnityEngine.EventSystems;

public class StartKotleta : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private GameObject[] Kotletas;
    private void Start()
    {
        if (Game.TimelyContinue < RecData.canCookBurger) { transform.gameObject.SetActive(false); }
        dg = Camera.main.GetComponent<Drag>();
        Kotletas = new GameObject[]
        {
            transform.parent.GetChild(2).GetChild(0).gameObject,
            transform.parent.GetChild(2).GetChild(1).gameObject
        };
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            for (int i = 0; i < 2; i++)
            {
                if (!Kotletas[i].activeInHierarchy) { Kotletas[i].SetActive(true); break; }
            }
        }
    }
}
