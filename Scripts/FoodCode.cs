using UnityEngine;

public class FoodCode : MonoBehaviour
{
    public int index = 0;
    public int Code() => index;
    private void OnDisable() => index = 0;
}
