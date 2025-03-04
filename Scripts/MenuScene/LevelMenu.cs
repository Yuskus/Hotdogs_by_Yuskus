using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    private MyData data;
    public void OpenLevelMenu(int index)
    {
        data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        if (index > data.AvailableLevels) { transform.GetChild(index).GetComponent<Button>().interactable = false; }
        else { transform.GetChild(index).GetChild(1).GetComponent<Text>().text = $"Plan: {RecData.plans[index]}\nRecord: {data.LvlRec[index]}"; }
    }
}
