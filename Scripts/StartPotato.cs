using UnityEngine;
using UnityEngine.EventSystems;

public class StartPotato : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private SpriteRenderer childSR;
    private BoxCollider2D col;
    private Sprite FreeBoxEmpty, FreeBoxFull;
    private GameObject Table;
    private AudioClip audioClip, doneFreeClip;
    private AudioSource audioSource;
    private void Start()
    {
        if (Game.TimelyContinue < RecData.canCookFree || (Game.TimelyContinue < 15 && Game.TimelyContinue > 9))
        {
            transform.gameObject.SetActive(false);
        }
        dg = Camera.main.GetComponent<Drag>();
        childSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        FreeBoxEmpty = Resources.Load<Sprite>("Sprites/EmptyFree");
        FreeBoxFull = Resources.Load<Sprite>("Sprites/CookingFree");
        Table = GameObject.FindGameObjectWithTag("Table");
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
        for (int a = 0; a < 3; a++)
        {
            Table.transform.GetChild(14).GetChild(a).gameObject.SetActive(true);
        }
        audioSource.clip = doneFreeClip;
        audioSource.Play();
        col.enabled = true;
        childSR.sprite = FreeBoxEmpty;
    }
}
