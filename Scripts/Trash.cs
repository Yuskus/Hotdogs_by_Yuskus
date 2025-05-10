using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class Trash : MonoBehaviour, IPointerDownHandler
{
    private Drag dg;
    private DraggingComponent drag;
    private Camera cam;
    private GameObject MoneyTrash;
    private AudioSource audioSource;
    public void Start()
    {
        cam = Camera.main;
        dg = cam.GetComponent<Drag>();
        drag = transform.parent.parent.GetComponent<DraggingComponent>();
        MoneyTrash = GameObject.FindGameObjectWithTag("Table").transform.GetChild(12).GetChild(3).GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Sounds/trash");
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
        else if (dg.SelectedObject.name != dg.Zero.name)
        {
            dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAsSelected();
        }
    }
    private void ForTrash(int price, Action backHome) //мусорка 2
    {
        cam.GetComponent<Game>().MySalary -= price;
        MoneyTrash.SetActive(true);
        MoneyTrash.GetComponent<TextMeshPro>().text = "-" + price;
        Invoke(nameof(TextOff), 2f);
        backHome();
        dg.SelectedObject = dg.Zero;
        audioSource.Play();
    }
    private void TextOff() => MoneyTrash.SetActive(false); //мусорка 3
}
