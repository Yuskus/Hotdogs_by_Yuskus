using UnityEngine;
using UnityEngine.EventSystems;

public class Free : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IBeginDragHandler
{
    private DraggingComponent drag;
    private Drag dg;
    private SpriteRenderer spRen;
    private void Awake()
    {
        drag = transform.parent.parent.GetComponent<DraggingComponent>();
        dg = Camera.main.GetComponent<Drag>();
        spRen = GetComponent<SpriteRenderer>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            dg.SelectedObject = transform.gameObject;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if (eventData.pointerId == 0 && dg.SelectedObject == transform.gameObject && !dg.isDragging)
        {
            drag.TakeObjectInHand(spRen);
            dg.isDragging = true;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId == 0 && dg.isDragging)
        {
            if (dg.SelectedObject == transform.gameObject)
            {
                drag.MousePos(transform.gameObject, eventData.position);
            }
            else
            {
                dg.isDragging = false;
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (dg.SelectedObject == transform.gameObject)
        {
            RaycastHit2D hit = drag.Ray(eventData.position);

            if (hit.collider != null && hit.transform.parent.gameObject.name == "OnScene")
            {
                hit.transform.GetComponent<AnyPerson>().CheckingForDrags();
            }

            BackHome(false);
        }
        else
        {
            BackHome(true);
        }
    }
    private void BackHome(bool drag)
    {
        GetComponent<MyStartPlace>().BackHomeAsSelected();
        dg.isDragging = drag;
    }
}
