using UnityEngine;
using UnityEngine.EventSystems;

public class StartBulka : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private GameObject StolHotDog;
    private void Start()
    {
        dg = Camera.main.GetComponent<Drag>();
        StolHotDog = transform.parent.GetChild(3).gameObject;
        if (Game.TimelyContinue == 0 && Game.TimelyAvailable == 0) { transform.gameObject.AddComponent<L_StartBulka>(); }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!StolHotDog.transform.GetChild(i).gameObject.activeInHierarchy) { StolHotDog.transform.GetChild(i).gameObject.SetActive(true); break; }
            }
        }
    }
}
