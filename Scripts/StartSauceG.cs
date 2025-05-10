using UnityEngine;
using UnityEngine.EventSystems;

public class StartSauceG : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IBeginDragHandler
{
    private DraggingComponent drag;
    private Drag dg;
    private SpriteRenderer spRen;
    private void Start()
    {
        MyData data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        if (data.ContinueGame < RecData.canCookSauseG) { transform.gameObject.SetActive(false); }
        drag = transform.parent.GetComponent<DraggingComponent>();
        dg = Camera.main.GetComponent<Drag>();
        spRen = GetComponent<SpriteRenderer>();
        drag.audioSourceG = GetComponent<AudioSource>();
        drag.audioSourceG.clip = Resources.Load<AudioClip>("Sounds/sauce 2");
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

            if (hit.collider != null)
            {
                switch (hit.transform.gameObject.name)
                {
                    case "HotDog":
                        {
                            hit.transform.GetComponent<CreatedBulka>().AddSauce();
                            if (hit.transform.GetComponent<SpriteRenderer>().sprite.name != "Bulochka")
                            {
                                dg.SelectedObject = hit.transform.gameObject;
                            }
                            break;
                        }
                    case "Burger":
                        {
                            hit.transform.GetComponent<CreatedBurger>().AddSauce();
                            if (hit.transform.GetComponent<SpriteRenderer>().sprite.name != "BulkaBurger")
                            {
                                dg.SelectedObject = hit.transform.gameObject;
                            }
                            break;
                        }
                }
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
