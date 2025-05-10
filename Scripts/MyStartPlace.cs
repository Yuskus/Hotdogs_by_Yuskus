using System;
using UnityEngine;

public class MyStartPlace : MonoBehaviour
{
    private Vector2 myStartPlace;
    private SpriteRenderer spRen;
    private SpriteRenderer HOsr;
    private int table, onTable;
    private SpriteRenderer[] children = Array.Empty<SpriteRenderer>();
    private DraggingComponent drag;
    public Action Back;
    private void Awake()
    {
        myStartPlace = transform.localPosition;
        
        GameObject Table = GameObject.FindGameObjectWithTag("Table");
        HOsr = Table.transform.GetChild(15).GetComponent<SpriteRenderer>();
        drag = Table.GetComponent<DraggingComponent>();

        table = SortingLayer.NameToID("Table");
        onTable = SortingLayer.NameToID("OnTable");

        spRen = transform.GetComponent<SpriteRenderer>();

        if (transform.gameObject.name == "HotDog" || transform.gameObject.name == "Burger")
        {
            children = new SpriteRenderer[3]
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>(),
                transform.GetChild(1).GetComponent<SpriteRenderer>(),
                transform.GetChild(2).GetComponent<SpriteRenderer>()
            };
        }
    }
    private void OnEnable()
    {
        transform.localPosition = myStartPlace;
        spRen.sortingLayerID = onTable;
        Back = transform.gameObject.name switch
        {
            "HotDog" => BackHomeAndOff,
            "Burger" => BackHomeAndOff,
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
        children[0].sortingLayerID = onTable;
        children[1].sortingLayerID = onTable;
        children[2].sortingLayerID = onTable;
        HOsr.sortingLayerID = table;
        drag.ForSelecting();
    }
}
