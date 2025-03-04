using UnityEngine;
using UnityEngine.EventSystems;

public class StartBurger : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private GameObject[] Burgers;
    private void Start()
    {
        if (Game.TimelyContinue < RecData.canCookBurger) { transform.gameObject.SetActive(false); }
        dg = Camera.main.GetComponent<Drag>();
        Burgers = new GameObject[]
        {
            transform.parent.GetChild(4).GetChild(0).gameObject,
            transform.parent.GetChild(4).GetChild(1).gameObject
        };
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            for (int i = 0; i < 2; i++)
            {
                if (!Burgers[i].activeInHierarchy) { Burgers[i].SetActive(true); break; }
            }
        }
    }
}
