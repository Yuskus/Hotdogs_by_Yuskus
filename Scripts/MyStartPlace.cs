using UnityEngine;

public class MyStartPlace : MonoBehaviour
{
    private Vector2 myStartPlace;
    private SpriteRenderer spRen;
    private SpriteRenderer HOsr;
    private int table, onTable;
    private SpriteRenderer[] children;
    private GameObject Table;
    private DraggingComponent drag;
    public delegate void AtPlace();
    public AtPlace Back;
    private void Awake()
    {
        myStartPlace = transform.localPosition;
        spRen = transform.GetComponent<SpriteRenderer>();
        HOsr = GameObject.FindGameObjectWithTag("Table").transform.GetChild(15).GetComponent<SpriteRenderer>();
        table = SortingLayer.NameToID("Table");
        onTable = SortingLayer.NameToID("OnTable");
        children = new SpriteRenderer[3];
        if (transform.gameObject.name is "HotDog" or "Burger")
        {
            for (int i = 0; i < 3; i++) { children[i] = transform.GetChild(i).GetComponent<SpriteRenderer>(); }
        }
        Table = GameObject.FindGameObjectWithTag("Table");
        drag = Table.GetComponent<DraggingComponent>();
    }
    private void OnEnable()
    {
        transform.localPosition = myStartPlace;
        spRen.sortingLayerID = onTable;
        Back = transform.gameObject.name switch
        {
            "HotDog" or "Burger" => BackHomeAndOff,
            "Free" => BackHomeAndOff_Free,
            _ => BackHomeAsSelected
        };
    }
    public void BackHomeAsSelected()
    {
        transform.localPosition = myStartPlace;
        spRen.sortingLayerID = onTable;
        HOsr.sortingLayerID = table;
        drag.ForSelecting();
    }
    public void BackHomeSelectedWithSauce()
    {
        transform.localPosition = myStartPlace;
        ChangeLayers_Meet();
    }
    public void BackHomeAndOff() //_children
    {
        transform.localPosition = myStartPlace;
        ChangeLayers_Meet();
        transform.gameObject.SetActive(false);
    }
    public void BackHomeAndOff_Free() //_no
    {
        transform.localPosition = myStartPlace;
        spRen.sortingLayerID = onTable;
        HOsr.sortingLayerID = table;
        transform.gameObject.SetActive(false);
        drag.ForSelecting();
    }
    private void ChangeLayers_Meet()
    {
        spRen.sortingLayerID = onTable;
        for (int i = 0; i < 3; i++) { children[i].sortingLayerID = onTable; }
        HOsr.sortingLayerID = table;
        drag.ForSelecting();
    }
}
