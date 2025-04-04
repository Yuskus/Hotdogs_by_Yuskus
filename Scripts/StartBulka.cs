using UnityEngine;
using UnityEngine.EventSystems;

public class StartBulka : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private MyData data;
    private GameObject StolHotDog;
    private void Start()
    {
        dg = Camera.main.GetComponent<Drag>();
        data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        StolHotDog = transform.parent.GetChild(3).gameObject;
        if (data.ContinueGame == 0 && data.AvailableLevels == 0) { transform.gameObject.AddComponent<L_StartBulka>(); }
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
