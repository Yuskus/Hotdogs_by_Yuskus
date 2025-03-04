using UnityEngine;

public class WhoIsOnScene : MonoBehaviour
{
    public bool TheLastClient()
    {
        if (transform.childCount > 0) { return transform.GetChild(0).GetComponent<AnyPerson>().TheClientLeaves(); }
        return true;
    }
}
