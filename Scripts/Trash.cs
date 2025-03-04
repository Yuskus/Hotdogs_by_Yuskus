using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Trash : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private DraggingComponent drag;
    private Camera cam;
    private GameObject Interactive;
    private AudioClip audioClip;
    private AudioSource audioSource;
    public void Start()
    {
        cam = Camera.main;
        dg = cam.GetComponent<Drag>();
        drag = transform.parent.parent.GetComponent<DraggingComponent>();
        Interactive = GameObject.FindGameObjectWithTag("Table").transform.GetChild(12).gameObject;
        audioClip = Resources.Load<AudioClip>("Sounds/trash");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!dg.isDragging) { TrashVoid(); }
    }
    public void TrashForDrags()
    {
        if (dg.isDragging) { TrashVoid(); }
    }
    private void TrashVoid() //мусорка 0
    {
        if (dg.SelectedObject.CompareTag("FoodForTrash"))
        {
            switch (dg.SelectedObject.name)
            {
                case "Sosis":
                case "Kotlet": ForTrash(10, dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAndOff_Free); break;
                case "HotDog": ForTrash(15, dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAndOff); break;
                case "Burger": ForTrash(20, dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAndOff); break;
                case "Onion": ForTrash(5, dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAndOff_Free); break;
            }
        }
        else { if (dg.SelectedObject.name != dg.Zero.name) { dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAsSelected(); } }
    }
    private delegate void Ho(); //мусорка 1
    private void ForTrash(int price, Ho ho) //мусорка 2
    {
        cam.GetComponent<Game>().MySalary -= price;
        Interactive.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
        Interactive.transform.GetChild(3).GetChild(0).GetComponent<TextMeshPro>().text = "-" + price;
        Invoke(nameof(TextOff), 2f);
        ho();
        dg.SelectedObject = dg.Zero;
        audioSource.Play();
    }
    private void TextOff() => Interactive.transform.GetChild(3).GetChild(0).gameObject.SetActive(false); //мусорка 3
}
