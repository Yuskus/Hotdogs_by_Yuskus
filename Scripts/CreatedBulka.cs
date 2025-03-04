using UnityEngine;
using UnityEngine.EventSystems;

public class CreatedBulka : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private DraggingComponent drag;
    private Drag dg;
    private SpriteRenderer spRen;
    private RaycastHit2D hit;
    private bool addedK, addedG, addedO;
    private SpriteRenderer[] son;
    private AudioClip audioClip;
    private AudioSource audioSource;
    private void Awake()
    {
        drag = transform.parent.parent.GetComponent<DraggingComponent>();
        dg = Camera.main.GetComponent<Drag>();
        if (Game.TimelyContinue == 0 && Game.TimelyAvailable == 0 && transform.GetSiblingIndex() == 0) { transform.gameObject.AddComponent<L_CreatedBulka>(); }
        spRen = GetComponent<SpriteRenderer>();
        son = new SpriteRenderer[3];
        for (int i = 0; i < 3; i++) { son[i] = transform.GetChild(i).GetComponent<SpriteRenderer>(); }
        audioClip = Resources.Load<AudioClip>("Sounds/add hotdog");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }
    private void OnEnable()
    {
        spRen.sprite = drag.BulkaHotDogSprite;
        for (int i = 0; i < 3; i++) { transform.GetChild(i).gameObject.SetActive(false); }
        addedK = false;
        addedG = false;
        addedO = false;
        audioSource.Play();
        GetComponent<FoodCode>().index = RecData.Code_HotDog;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            if (spRen.sprite.name == "Bulochka") { drag.MakeFoodDone("Sosis", spRen, drag.sosiska, drag.hotdog); }
            else
            {
                AddSauce();
                dg.SelectedObject = transform.gameObject;
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if (spRen.sprite.name != "Bulochka" && eventData.pointerId == 0 && !dg.isDragging)
        {
            drag.TakeObjectInHand(spRen, son);
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
        if (dg.SelectedObject == transform.gameObject && spRen.sprite.name != "Bulochka")
        {
            hit = drag.Ray(eventData.position);
            if (hit.collider == null) { BackHome(false); return; }
            else if (hit.transform.parent.gameObject.name == "OnScene") { Checking(); }
            else if (hit.transform.gameObject.name == "Trash") { Trash(); }
            else { BackHome(false); }
        }
        else { BackHome(true); }
    }
    private void Checking()
    {
        hit.transform.GetComponent<AnyPerson>().CheckingForDrags();
        dg.isDragging = false;
    }
    private void Trash()
    {
        hit.transform.GetComponent<Trash>().TrashForDrags();
        dg.isDragging = false;
    }
    private void BackHome(bool drag)
    {
        GetComponent<MyStartPlace>().BackHomeSelectedWithSauce();
        dg.isDragging = drag;
    }
    public void AddSauce()
    {
        drag.AddSauce(transform.gameObject, spRen, ref addedK, ref addedG, ref addedO);
    }
}
