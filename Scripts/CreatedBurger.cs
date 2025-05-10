using UnityEngine;
using UnityEngine.EventSystems;

public class CreatedBurger : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IBeginDragHandler
{
    private DraggingComponent drag;
    private Drag dg;
    private SpriteRenderer spRen;
    private bool addedK, addedG, addedO;
    private SpriteRenderer[] son;
    private AudioSource audioSource;
    private void Awake()
    {
        drag = transform.parent.parent.GetComponent<DraggingComponent>();
        dg = Camera.main.GetComponent<Drag>();
        spRen = GetComponent<SpriteRenderer>();
        son = new SpriteRenderer[3]
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>(),
            transform.GetChild(1).GetComponent<SpriteRenderer>(),
            transform.GetChild(2).GetComponent<SpriteRenderer>()
        };
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Sounds/add hotdog");
    }
    private void OnEnable()
    {
        spRen.sprite = drag.BulkaBurgerSprite;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        addedK = false;
        addedG = false;
        addedO = false;
        audioSource.Play();
        GetComponent<FoodCode>().Index = RecData.Code_Burger;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging)
        {
            if (spRen.sprite.name == "BulkaBurger")
            {
                drag.MakeFoodDone("Kotlet", spRen, drag.kotleta, drag.burger);
            }
            else
            {
                AddSauce();
                dg.SelectedObject = transform.gameObject;
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (spRen.sprite.name == "BulkaBurger") return;
        
        if (eventData.pointerId == 0 && dg.SelectedObject == transform.gameObject && !dg.isDragging)
        {
            drag.TakeObjectInHand(spRen, son);
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
        if (dg.SelectedObject == transform.gameObject && spRen.sprite.name != "BulkaBurger")
        {
            RaycastHit2D hit = drag.Ray(eventData.position);

            if (hit.collider == null)
            {
                BackHome(false);
            }
            else
            {
                if (hit.transform.parent.gameObject.name == "OnScene")
                {
                    hit.transform.GetComponent<AnyPerson>().CheckingForDrags();
                }
                else if (hit.transform.gameObject.name == "Trash")
                {
                    hit.transform.GetComponent<Trash>().TrashForDrags();
                }

                dg.isDragging = false;
            }
        }
        else
        {
            BackHome(true);
        }
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
