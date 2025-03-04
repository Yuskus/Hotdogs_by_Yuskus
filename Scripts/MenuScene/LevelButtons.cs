using UnityEngine;

public class LevelButtons : MonoBehaviour
{
    private void OnEnable()
    {
        transform.parent.GetComponent<LevelMenu>().OpenLevelMenu(transform.GetSiblingIndex());
    }
}
