using UnityEngine;

public class Drag : MonoBehaviour
{
    private GameObject selectedObject, zero;
    public bool isDragging;
    public delegate void Ho();
    public event Ho Choised;
    public void Awake()
    {
        Zero = GameObject.FindGameObjectWithTag("Table").transform.GetChild(0).gameObject;
        SelectedObject = Zero;
    }
    public GameObject Zero
    {
        get { return zero; }
        private set { zero = value; }
    }
    public GameObject SelectedObject
    {
        get { return selectedObject; }
        set
        {
            selectedObject = value;
            Choised?.Invoke();
        }
    }
}
