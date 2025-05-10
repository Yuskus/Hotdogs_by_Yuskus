using UnityEngine;
using UnityEngine.SceneManagement;

public class FocusCamera : MonoBehaviour
{
    public void CameraPos(Camera Cam)
    {
        float aspectRatio = (float) Screen.width / Screen.height;

        switch (aspectRatio)
        {
            case < 1.5f: ValuesForCamera(Cam, 3.4f, 9.1f, true); break;
            case < 2.0f: ValuesForCamera(Cam, 1.5f, 7.2f); break;
            case < 2.2f: ValuesForCamera(Cam, 0.4f, 6.6f); break;
            case < 2.5f: ValuesForCamera(Cam, 0.2f, 6.45f); break;
            default: ValuesForCamera(Cam, 0f, 6.0f); break;
        }
    }

    private void ValuesForCamera(Camera Cam, float yPos, float orthographicSize, bool isBigDisplay = false)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cam.GetComponent<Transform>().position = new(0, 0, -10);
            Cam.GetComponent<Camera>().orthographicSize = orthographicSize;
        }
        else
        {
            Cam.GetComponent<Transform>().position = new(0, yPos, -10);
            Cam.GetComponent<Camera>().orthographicSize = yPos + 4.7f;

            if (isBigDisplay)
            {
                GameObject.FindGameObjectWithTag("Sky").GetComponent<Transform>().position = new Vector2(0, 2);
            }
        }
    }
}
