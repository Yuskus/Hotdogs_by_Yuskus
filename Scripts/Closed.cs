using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Closed : MonoBehaviour //38 check
{
    private MyData data;
    private GameObject EndPanel, Canvas;
    private int levelNum;
    private readonly float yPos = 0f;
    private string winLoseText = "";
    private bool youWin;
    private bool IsMoving;
    private int MySalary, allTimeSalary;
    private AudioClip audioClip;
    private AudioSource audioSource;
    private void Start()
    {
        data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        IsMoving = false;
        youWin = false;
        transform.localPosition = new Vector2(0, 20);
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        EndPanel = Canvas.transform.GetChild(1).gameObject;
        levelNum = data.ContinueGame; //reading
        audioClip = Resources.Load<AudioClip>("Sounds/closed");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }
    private IEnumerator Closing()
    {
        while (IsMoving)
        {
            Moving(-14f);
            if (transform.localPosition.y < yPos) { IsMoving = false; }
            yield return null;
        }
        IsClosed();
        yield break;
    }
    public void TheCafeIsClosing()
    {
        IsMoving = true;
        Canvas.transform.GetChild(0).gameObject.SetActive(false);
        audioSource.Play();
        StartCoroutine(Closing());
    }
    private void IsClosed()
    {
        data.RecSum(out allTimeSalary);
        EndPanel.SetActive(true); //on endgame panel
        MySalary = Camera.main.GetComponent<Game>().MySalary; //save salary
        if (MySalary >= RecData.plans[levelNum]) { winLoseText = "Good day!"; youWin = true; }
        else { winLoseText = "Try again :("; youWin = false; }
        EndPanel.transform.GetChild(0).GetComponent<Text>().text = winLoseText;
        EndPanel.transform.GetChild(1).GetComponent<Text>().text = $"Plan: {RecData.plans[levelNum]} \nEarned: {MySalary} \nFor all time: {allTimeSalary}";
        EndPanel.transform.GetChild(2).GetComponent<Button>().interactable = youWin;
    }
    private void Moving(float speed) => transform.Translate(new Vector2(0, speed) * Time.deltaTime);
}
