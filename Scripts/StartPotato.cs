using UnityEngine;
using UnityEngine.EventSystems;

public class StartPotato : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private SpriteRenderer childSR;
    private BoxCollider2D col;
    private Sprite FreeBoxEmpty, FreeBoxFull;
    private GameObject ParentPotato;
    private AudioClip audioClip, doneFreeClip;
    private AudioSource audioSource;
    private void Start()
    {
        MyData data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        if (data.ContinueGame < RecData.canCookFree || (data.ContinueGame < 15 && data.ContinueGame > 9))
        {
            transform.gameObject.SetActive(false);
        }
        dg = Camera.main.GetComponent<Drag>();
        childSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        FreeBoxEmpty = Resources.Load<Sprite>("Sprites/EmptyFree");
        FreeBoxFull = Resources.Load<Sprite>("Sprites/CookingFree");
        ParentPotato = GameObject.FindGameObjectWithTag("Table").transform.GetChild(14).gameObject;
        audioClip = Resources.Load<AudioClip>("Sounds/fries");
        doneFreeClip = Resources.Load<AudioClip>("Sounds/fries box");
        audioSource = GetComponent<AudioSource>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging) { Cook(); }
    }
    private void Cook()
    {
        if (FreeBoxFull != childSR.sprite)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        childSR.sprite = FreeBoxFull;
        col.enabled = false;
        Invoke(nameof(Done), 3f);
    }
    private void Done()
    {
        ParentPotato.transform.GetChild(0).gameObject.SetActive(true);
        ParentPotato.transform.GetChild(1).gameObject.SetActive(true);
        ParentPotato.transform.GetChild(2).gameObject.SetActive(true);

        audioSource.clip = doneFreeClip;
        audioSource.Play();
        col.enabled = true;
        childSR.sprite = FreeBoxEmpty;
    }
}
