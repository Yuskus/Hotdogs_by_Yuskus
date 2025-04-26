using UnityEngine;
using UnityEngine.EventSystems;

public class Drink : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IBeginDragHandler
{
    private DraggingComponent drag;
    private Drag dg;
    private MyStartPlace myStartPlace;
    private SpriteRenderer spRen;
    private AudioSource audioSource;
    private void Awake()
    {
        MyData data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        transform.parent.gameObject.SetActive(data.ContinueGame >= RecData.canCookDrink);
    }
    private void Start()
    {
        drag = transform.parent.parent.GetComponent<DraggingComponent>();
        dg = Camera.main.GetComponent<Drag>();
        myStartPlace = transform.GetComponent<MyStartPlace>();
        spRen = transform.GetComponent<SpriteRenderer>();
        audioSource = transform.GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Sounds/drink");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            dg.SelectedObject = transform.gameObject;
            audioSource.Play();
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
            if (drag.Ray(eventData.position) is RaycastHit2D hit && hit.transform.parent.gameObject.name == "OnScene")
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
        myStartPlace.BackHomeAsSelected();
        dg.isDragging = drag;
    }
}
