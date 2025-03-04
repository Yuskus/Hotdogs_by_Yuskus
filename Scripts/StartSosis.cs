using UnityEngine;
using UnityEngine.EventSystems;

public class StartSosis : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private GameObject PlitaSosis;
    private void Start()
    {
        dg = Camera.main.GetComponent<Drag>();
        PlitaSosis = transform.parent.GetChild(1).gameObject;
        if (Game.TimelyContinue == 0 && Game.TimelyAvailable == 0) { transform.gameObject.AddComponent<L_StartSosis>(); }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!PlitaSosis.transform.GetChild(i).gameObject.activeInHierarchy) { PlitaSosis.transform.GetChild(i).gameObject.SetActive(true); break; }
            }
        }
    }
}
