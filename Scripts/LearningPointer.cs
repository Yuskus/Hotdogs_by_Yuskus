using UnityEngine;

public class LearningPointer : MonoBehaviour
{
    private Game game;
    private bool moving;
    private bool upDown;
    private float time, sinusTimer, yPos;
    private void Awake()
    {
        game = Camera.main.GetComponent<Game>();
        time = 0;
        yPos = 0;
    }
    private void Update()
    {
        if (upDown)
        {
            sinusTimer += Time.deltaTime;
            yPos = 0.25f * Mathf.Sin(sinusTimer * 4f);
            transform.Translate(new Vector2(yPos, yPos) * Time.deltaTime);
        }
    }
    public void Press(GameObject obj_0) //once
    {
        upDown = true;
        transform.localPosition = obj_0.transform.localPosition;
    }
    public void OnlyPress(GameObject obj_0)
    {
        moving = false;
        time = 0;
        transform.localPosition = obj_0.transform.localPosition;
        Invoke(nameof(MovingOn), 0.3f);
    }
    public void MovingOn() => moving = true;
    public void Move(GameObject obj_1, GameObject obj_2, float speed)
    {
        if (moving)
        {
            upDown = false;
            time += Time.deltaTime;
            transform.localPosition = Vector2.Lerp(obj_1.transform.localPosition, obj_2.transform.localPosition, time * speed / Vector2.Distance(obj_1.transform.localPosition, obj_2.transform.localPosition));
            if (transform.localPosition == obj_2.transform.localPosition)
            {
                time = 0;
                moving = false;
            }
        }
    }
    public void WriteText(string text)
    {
        if (!game.Learn.activeInHierarchy) { game.Learn.SetActive(true); }
        game.LearnText.text = text;
    }
    public void TurnLearnOff()
    {
        game.Learn.SetActive(false);
    }
}
