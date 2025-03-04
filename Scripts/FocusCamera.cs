using UnityEngine;
using UnityEngine.SceneManagement;

public class FocusCamera : MonoBehaviour
{
    private float size, aspectRatio;
    private Vector3 yPos;
    public void CameraPos(Camera Cam)
    {
        aspectRatio = (float) Screen.width / Screen.height;
        switch (aspectRatio)
        {
            case < 1.5f: ValuesForCamera(3.4f, 9.1f); ForBigSizeOfDisp(); break;
            case < 2.0f: ValuesForCamera(1.5f, 7.2f); break;
            case < 2.2f: ValuesForCamera(0.4f, 6.6f); break;
            case < 2.5f: ValuesForCamera(0.2f, 6.45f); break;
            default: ValuesForCamera(0f, 6.0f); break;
        }
        Cam.GetComponent<Transform>().position = yPos;
        Cam.GetComponent<Camera>().orthographicSize = size;
    }
    private void ValuesForCamera(float y1, float y2)
    {
        yPos = new(0, y1, -10);
        size = y1 + 4.7f;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            yPos.y = 0;
            size = y2;
        }
    }
    private void ForBigSizeOfDisp()
    {
        if (SceneManager.GetActiveScene().name == "Menu") { return; }
        GameObject.FindGameObjectWithTag("Sky").GetComponent<Transform>().position = new Vector2(0, 2);
    }
}
