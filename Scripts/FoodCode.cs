using UnityEngine;

public class FoodCode : MonoBehaviour
{
    public int Index { get; set; } = 0;

    private void OnDisable()
    {
        Index = 0;
    }
}
