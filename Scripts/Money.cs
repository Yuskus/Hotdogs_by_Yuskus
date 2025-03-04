using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Money : MonoBehaviour, IPointerDownHandler
{
    private Game game;
    private Drag dg;
    private MoneySound sound;
    private int number;
    private int price;
    private GameObject Parent, TextPref;
    private void Awake()
    {
        game = Camera.main.GetComponent<Game>();
        dg = Camera.main.GetComponent<Drag>();
        number = transform.parent.GetSiblingIndex();
        Parent = game.Interactive.transform.GetChild(2).gameObject;
        sound = game.Interactive.GetComponent<MoneySound>();
    }
    private void OnEnable()
    {
        game.MoneyOnTable += 1;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (dg.isDragging) return;
        if (transform.parent.gameObject == Parent) { MakeTextPref(); }
        else
        {
            game.AllClients.GetComponent<HairList>().randomIndex.Add(number);
            transform.parent.GetComponent<ParMoney>().WritingOutPrice(out price);
            game.MySalary += price;
            SetAct(number, true);
            TextIs(number, price, "+");
            Invoke(nameof(Vanish), 2f);
            transform.gameObject.SetActive(false);
        }
        sound.Play();
        game.MoneyOnTable -= 1;
    }
    public void WritingInPrice(int value)
    {
        price = value;
    }
    private void MakeTextPref()
    {
        game.MySalary += price;
        TextPref = CreatingReturns(game.TextPref, Parent, new(transform.localPosition.x, 3.75f));
        TextPref.transform.GetComponent<TextMeshPro>().text = $"+{price}";
        Destroy(TextPref, 2f);
        Destroy(transform.gameObject);
    }
    private void Vanish()
    {
        TextIs(number, 0, "");
        SetAct(number, false);
    }
    private GameObject CreatingReturns(GameObject clone, GameObject parent, Vector2 place)
    {
        GameObject copy = Instantiate(clone, parent.transform, false);
        copy.transform.localPosition = place;
        return copy;
    }
    private void SetAct(int num, bool active) => game.Interactive.transform.GetChild(1).GetChild(num).GetChild(1).gameObject.SetActive(active); //money
    private void TextIs(int num, int price, string text) => game.Interactive.transform.GetChild(1).GetChild(num).GetChild(1).GetComponent<TextMeshPro>().text = text + price; //money
}
