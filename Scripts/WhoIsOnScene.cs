using UnityEngine;

public class WhoIsOnScene : MonoBehaviour
{
    public bool TheLastClient()
    {
        return transform.childCount <= 0 || transform.GetChild(0).GetComponent<AnyPerson>().TheClientLeaves();
    }
}
