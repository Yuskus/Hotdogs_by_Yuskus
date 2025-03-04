using System.Collections.Generic;
using UnityEngine;

public class DraggingComponent : MonoBehaviour
{
    private Drag dg;
    private Transform HOtr;
    private SpriteRenderer HOsr;
    private Vector2 m2d;
    private Camera cam;
    private Sprite[] SO_sprites;
    private Dictionary<string, Sprite> selection;
    private int inHand, onTable;
    public Sprite BulkaHotDogSprite, BulkaBurgerSprite;
    public Sprite[] sosiska;
    public Sprite[] hotdog;
    public Sprite[] kotleta;
    public Sprite[] burger;
    public Sprite[] onion;
    public Sprite[] HD_added;
    public Sprite[] B_added;
    private Sprite[] O_B_added;
    private Sprite[] O_HD_added;
    private AudioClip audioClipBulka;
    private AudioSource audioSourceBulka;
    public AudioSource audioSourceK, audioSourceG;
    private void Awake()
    {
        cam = Camera.main;
        dg = cam.GetComponent<Drag>();
        HOsr = transform.GetChild(15).GetComponent<SpriteRenderer>();
        HOtr = transform.GetChild(15).GetComponent<Transform>();
    }
    private void Start()
    {
        BulkaHotDogSprite = Resources.Load<Sprite>("Sprites/Bulochka");
        BulkaBurgerSprite = Resources.Load<Sprite>("Sprites/BulkaBurger");
        audioClipBulka = Resources.Load<AudioClip>("Sounds/put in bulka");
        audioSourceBulka = GetComponent<AudioSource>();
        sosiska = new Sprite[4];
        hotdog = new Sprite[4];
        kotleta = new Sprite[4];
        burger = new Sprite[4];
        onion = new Sprite[3];
        HD_added = new Sprite[3];
        B_added = new Sprite[3];
        O_B_added = new Sprite[3];
        O_HD_added = new Sprite[3];
        selection = new();
        SO_sprites = new Sprite[7];
        audioSourceBulka.clip = audioClipBulka;
        for (int i = 0; i < 4; i++)
        {
            sosiska[i] = Resources.Load<Sprite>("Sprites/Sosis" + i);
            kotleta[i] = Resources.Load<Sprite>("Sprites/Kotleta" + i);
            hotdog[i] = Resources.Load<Sprite>("Sprites/HotDog" + i);
            burger[i] = Resources.Load<Sprite>("Sprites/Burger" + i);
            if (i != 3) { onion[i] = Resources.Load<Sprite>("Sprites/PanOnion" + (i+1)); }
        }
        SO_sprites = Resources.LoadAll<Sprite>("Sprites/So");
        ForSelectingList();
        ForAddingSprites();
        inHand = SortingLayer.NameToID("InHand");
        onTable = SortingLayer.NameToID("OnTable");
    }
    public void OnEnable()
    {
        dg.Choised += ForSelecting;
    }
    public void ForSelecting() //компонент перетаскивания
    {
        if (dg.SelectedObject == dg.Zero)
        {
            HOsr.sprite = null;
            return;
        }
        HOsr.sprite = selection[dg.SelectedObject.name];
        HOtr.SetPositionAndRotation(dg.SelectedObject.transform.position, dg.SelectedObject.transform.rotation);
    }
    public void MousePos(GameObject thisObj, Vector2 pos) //компонент перетаскивания, перетаскивание объекта-ссылки мышью, пальцем
    {
        m2d = cam.ScreenToWorldPoint(pos);
        thisObj.transform.position = m2d;
        HOtr.position = dg.SelectedObject.transform.position;
    }
    public void AddSauce(GameObject obj, SpriteRenderer sR, ref bool addedK, ref bool addedG, ref bool addedO) //компонент перетаскивания, добавляет соус на хот дог/бургер
    {
        if (sR.sprite.name is "Bulochka" or "BulkaBurger") { return; }
        switch (dg.SelectedObject.name)
        {
            case "StartSauceK": if (!addedK) { ForAddingSauces(obj, 0, sR, 1, audioSourceK); addedK = true; } break;
            case "StartSauceG": if (!addedG) { ForAddingSauces(obj, 1, sR, 2, audioSourceG); addedG = true; } break;
            case "Onion": if (!addedO)
                {
                    int i = System.Array.IndexOf(onion, dg.SelectedObject.GetComponent<SpriteRenderer>().sprite);
                    obj.transform.GetChild(2).gameObject.SetActive(true);
                    if (sR.gameObject.name == "HotDog") { obj.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = O_HD_added[i]; }
                    else if (sR.gameObject.name == "Burger") { obj.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = O_B_added[i]; }
                    dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAndOff_Free();
                    if (i == 1) { sR.gameObject.GetComponent<FoodCode>().index += RecData.Code_Onion; }
                    addedO = true;
                }
                break;
        }
    }
    private void ForAddingSauces(GameObject obj, int index, SpriteRenderer sR, int foodCode, AudioSource audio)
    {
        obj.transform.GetChild(index).gameObject.SetActive(true);
        sR.gameObject.GetComponent<FoodCode>().index += foodCode;
        audio.Play();
    }
    public void MakeFoodDone(string name, SpriteRenderer sR, Sprite[] notyet, Sprite[] done) //компонент перетаскивания, проверяет прожарку (сосиски/котлеты), добавляет в булку
    {
        if (dg.SelectedObject.name == name & (sR.sprite.name is "Bulochka" or "BulkaBurger"))
        {
            int i = System.Array.IndexOf(notyet, dg.SelectedObject.GetComponent<SpriteRenderer>().sprite);
            sR.sprite = done[i];
            audioSourceBulka.Play();
            dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAndOff_Free();
            dg.SelectedObject = sR.transform.gameObject;
            if (i == 2) { dg.SelectedObject.GetComponent<FoodCode>().index += RecData.Code_Doneness; }
        }
        else { if (dg.SelectedObject != dg.Zero) { dg.SelectedObject.GetComponent<MyStartPlace>().BackHomeAsSelected(); } }
    }
    public RaycastHit2D Ray(Vector2 pos) //компонент перетаскивания, пускает луч
    {
        dg.SelectedObject.transform.GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(pos));
        dg.SelectedObject.transform.GetComponent<BoxCollider2D>().enabled = true;
        return hit;
    }
    public void TakeObjectInHand(SpriteRenderer sR)
    {
        sR.sortingLayerID = inHand;
        HOsr.sortingLayerID = onTable;
    }
    public void TakeObjectInHand(SpriteRenderer sR, SpriteRenderer[] sons)
    {
        sR.sortingLayerID = inHand;
        for (int i = 0; i < 3; i++) { sons[i].sortingLayerID = inHand; }
        HOsr.sortingLayerID = onTable;
    }
    private void ForSelectingList()
    {
        selection.Add("Burger", SO_sprites[0]);
        selection.Add("ColaClone", SO_sprites[1]);
        selection.Add("DrinkClone", SO_sprites[2]);
        selection.Add("Free", SO_sprites[3]);
        selection.Add("HotDog", SO_sprites[4]);
        selection.Add("Kotlet", SO_sprites[5]);
        selection.Add("Onion", SO_sprites[6]);
        selection.Add("StartSauceK", SO_sprites[7]);
        selection.Add("StartSauceG", SO_sprites[7]);
        selection.Add("Sosis", SO_sprites[8]);
    }
    private void ForAddingSprites()
    {
        O_B_added[0] = Resources.Load<Sprite>("Sprites/AddedO_B_1");
        O_B_added[1] = Resources.Load<Sprite>("Sprites/AddedO_B_2");
        O_B_added[2] = Resources.Load<Sprite>("Sprites/AddedO_B_3");
        O_HD_added[0] = Resources.Load<Sprite>("Sprites/AddedO_HD_1");
        O_HD_added[1] = Resources.Load<Sprite>("Sprites/AddedO_HD_2");
        O_HD_added[2] = Resources.Load<Sprite>("Sprites/AddedO_HD_3");
        HD_added[0] = Resources.Load<Sprite>("Sprites/AddedK_HD");
        HD_added[1] = Resources.Load<Sprite>("Sprites/AddedG_HD");
        HD_added[2] = O_HD_added[1];
        B_added[0] = Resources.Load<Sprite>("Sprites/AddedK_B");
        B_added[1] = Resources.Load<Sprite>("Sprites/AddedG_B");
        B_added[2] = O_B_added[1];
    }
    public void OnDisable()
    {
        dg.Choised -= ForSelecting;
    }
}
