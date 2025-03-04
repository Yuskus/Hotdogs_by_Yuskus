using UnityEngine;

public class ParMoney : MonoBehaviour //5 + trash
{
    private int price;
    public void WritingInPrice(int value) => price = value;
    public void WritingOutPrice(out int value) => value = price;
}
