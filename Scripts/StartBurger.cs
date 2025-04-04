using UnityEngine;
using UnityEngine.EventSystems;

public class StartBurger : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private MyData data;
    private GameObject[] Burgers;
    private void Start()
    {
        data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        if (data.ContinueGame < RecData.canCookBurger) { transform.gameObject.SetActive(false); }
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
