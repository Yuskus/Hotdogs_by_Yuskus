using UnityEngine;
using UnityEngine.EventSystems;

public class Cola : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IBeginDragHandler
{
    private DraggingComponent drag;
    private Drag dg;
    private SpriteRenderer spRen;
    private RaycastHit2D hit;
    private AudioClip audioClip;
    private AudioSource audioSource;
    private void Awake()
    {
        if (Game.TimelyContinue < RecData.canCookCola) { transform.parent.gameObject.SetActive(false); }
    }
    private void Start()
    {
        drag = transform.parent.parent.GetComponent<DraggingComponent>();
        dg = Camera.main.GetComponent<Drag>();
        spRen = GetComponent<SpriteRenderer>();
        audioClip = Resources.Load<AudioClip>("Sounds/drink"); //CHANGE
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
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
        if (eventData.pointerId == 0 && !dg.isDragging)
        {
            drag.TakeObjectInHand(spRen);
            dg.isDragging = true;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId == 0 && dg.isDragging)
        {
            if (dg.SelectedObject == transform.gameObject) { drag.MousePos(transform.gameObject, eventData.position); }
            else { dg.isDragging = false; }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (dg.SelectedObject == transform.gameObject)
        {
            hit = drag.Ray(eventData.position);
            if (hit.collider == null) { BackHome(false); return; }
            else if (hit.transform.parent.gameObject.name == "OnScene") { Checking(); }
            else { BackHome(false); }
        }
        else { BackHome(true); }
    }
    private void Checking()
    {
        hit.transform.GetComponent<AnyPerson>().CheckingForDrags();
        dg.isDragging = false;
    }
    private void BackHome(bool drag)
    {
        GetComponent<MyStartPlace>().BackHomeAsSelected();
        dg.isDragging = drag;
    }
}
