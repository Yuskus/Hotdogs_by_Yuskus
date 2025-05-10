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
    private GameObject CurrentParent, ParentOfThiefsMoney;

    private void Awake()
    {
        game = Camera.main.GetComponent<Game>();
        dg = Camera.main.GetComponent<Drag>();
        number = transform.parent.GetSiblingIndex();
        CurrentParent = transform.parent.gameObject;
        ParentOfThiefsMoney = game.Interactive.transform.GetChild(2).gameObject;
        sound = game.Interactive.GetComponent<MoneySound>();
    }

    private void OnEnable()
    {
        game.MoneyOnTable += 1;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (dg.isDragging) return;

        if (CurrentParent != ParentOfThiefsMoney)
        {
            GetClientsMoney();
        }
        else
        {
            GetThiefsMoney();
        }
    }

    private void GetClientsMoney()
    {
        game.AllClients.GetComponent<HairList>().randomIndex.Add(number);
        transform.parent.GetComponent<ParMoney>().WritingOutPrice(out price);
        game.MySalary += price;
        SetAct(number, true);
        TextIs(number, price, "+");
        game.MoneyOnTable -= 1;
        sound.Play();
        Invoke(nameof(Vanish), 2f);
        transform.gameObject.SetActive(false);
    }

    private void GetThiefsMoney()
    {
        game.MySalary += price;
        GameObject TextPref = Instantiate(game.TextPref, ParentOfThiefsMoney.transform, false);
        TextPref.transform.localPosition = new Vector2(transform.localPosition.x, 3.75f);
        TextPref.transform.GetComponent<TextMeshPro>().text = $"+{price}";
        game.MoneyOnTable -= 1;
        sound.Play();
        Destroy(TextPref, 2f);
        Destroy(transform.gameObject);
    }

    public void WritingInPrice(int value)
    {
        price = value;
    }

    private void Vanish()
    {
        TextIs(number, 0, "");
        SetAct(number, false);
    }

    private void SetAct(int num, bool active) => game.Interactive.transform.GetChild(1).GetChild(num).GetChild(1).gameObject.SetActive(active); //money
    private void TextIs(int num, int price, string text) => game.Interactive.transform.GetChild(1).GetChild(num).GetChild(1).GetComponent<TextMeshPro>().text = text + price; //money
}
