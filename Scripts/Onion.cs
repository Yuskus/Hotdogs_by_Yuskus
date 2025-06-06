using UnityEngine;
using UnityEngine.EventSystems;

public class Onion : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    private DraggingComponent drag;
    private Drag dg;
    private int i;
    private float time;
    private bool timer;
    private SpriteRenderer sR, childSR;
    private AudioClip audioClip, dzinn, burntOut;
    private AudioSource audioSource;
    private Animation anim;
    private void Awake()
    {
        drag = GameObject.FindGameObjectWithTag("Table").GetComponent<DraggingComponent>();
        dg = Camera.main.GetComponent<Drag>();
        anim = transform.GetChild(0).GetComponent<Animation>();
        childSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sR = GetComponent<SpriteRenderer>();
        audioClip = Resources.Load<AudioClip>("Sounds/add sosis or kotlet");
        dzinn = Resources.Load<AudioClip>("Sounds/din");
        burntOut = Resources.Load<AudioClip>("Sounds/burnt out");
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        sR.sprite = drag.onion[0];
        i = 0;
        time = 0f;
        timer = true;
        audioSource.clip = audioClip;
        audioSource.Play();
        anim.enabled = false;
        childSR.enabled = false;
    }
    private void Update()
    {
        if (timer)
        {
            time += Time.deltaTime;
            switch (time)
            {
                case > 7.0f: ChangeSpriteAndAnim(2, burntOut, false); break;
                case > 3.0f: ChangeSpriteAndAnim(1, dzinn, true); break;
            }
        }
    }
    private void ChangeSpriteAndAnim(int ind, AudioClip clip, bool isAnimAndTimerPlay)
    {
        if (i == ind) return;
        i = ind;
        sR.sprite = drag.onion[i];
        timer = isAnimAndTimerPlay;
        audioSource.clip = clip;
        audioSource.Play();
        anim.enabled = isAnimAndTimerPlay;
        childSR.enabled = isAnimAndTimerPlay;
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
            drag.TakeObjectInHand(sR);
            timer = false;
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

                            dg.isDragging = false;
                            return;
                        }
                    case "Burger":
                        {
                            hit.transform.GetComponent<CreatedBurger>().AddSauce();

                            if (hit.transform.GetComponent<SpriteRenderer>().sprite.name != "BulkaBurger")
                            {
                                dg.SelectedObject = hit.transform.gameObject;
                            }

                            dg.isDragging = false;
                            return;
                        }
                    case "Trash":
                        {
                            hit.transform.GetComponent<Trash>().TrashForDrags();
                            dg.isDragging = false;
                            return;
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
        timer = true;
    }
}
