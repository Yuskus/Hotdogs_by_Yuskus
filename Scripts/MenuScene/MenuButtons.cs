using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Linq;

public class MenuButtons : MonoBehaviour
{
    private MyData data;
    private GameObject Canvas, Panel1, Panel2, Panel3, Panel4, PanelLevels, Fire;
    private Transform FonTr, Fon2Tr, circle1, circle2;
    private GameObject ButtonFire;
    private float offset, offset2, circleStep;
    private bool move;
    private AudioSource[] audioSource;
    private string soundChanged = "";
    private void Awake()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        Panel1 = Canvas.transform.GetChild(3).gameObject;
        Panel2 = Panel1.transform.GetChild(4).gameObject;
        Panel3 = Panel1.transform.GetChild(5).gameObject;
        Panel4 = Panel1.transform.GetChild(6).gameObject;
        PanelLevels = Canvas.transform.GetChild(5).gameObject;
        GameObject MovingForest = GameObject.FindGameObjectWithTag("Table");
        Fire = GameObject.FindGameObjectWithTag("Fire");
        ButtonFire = Canvas.transform.GetChild(4).gameObject;
        FonTr = MovingForest.transform.GetChild(0).GetComponent<Transform>();
        Fon2Tr = MovingForest.transform.GetChild(1).GetComponent<Transform>();
        circle1 = MovingForest.transform.GetChild(2).GetComponent<Transform>();
        circle2 = MovingForest.transform.GetChild(3).GetComponent<Transform>();
        audioSource = GetComponents<AudioSource>();
        audioSource[0].clip = Resources.Load<AudioClip>("Sounds/tryFullTheme");
        audioSource[1].clip = Resources.Load<AudioClip>("Sounds/click");
        audioSource[0].loop = true;
        audioSource[0].Play();
        move = true;
    }
    private void Start()
    {
        offset = 0;
        offset2 = -35;
        Camera.main.GetComponent<FocusCamera>().CameraPos(Camera.main);
        data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        RecData.LoadStateOfSound(ref soundChanged);
        ButtonSoundSwitcher();
        if (data.LvlRec[^1] > RecData.plans[^1]) { ButtonFire.SetActive(true); }
    }
    private void MovingCircle()
    {
        circleStep += Time.deltaTime * 30;
        circle1.rotation = Quaternion.AngleAxis(circleStep, Vector3.forward);
        circle2.rotation = Quaternion.AngleAxis(circleStep * 2, Vector3.forward);
    }
    private void MovingFon()
    {
        offset += Time.deltaTime;
        offset2 += Time.deltaTime;
        FonTr.localPosition = new Vector2(offset, 0);
        Fon2Tr.localPosition = new Vector2(offset2, 0);
        if (move && Fon2Tr.localPosition.x > 0)
        {
            offset -= 70f;
            move = false;
        }
        else if (!move && FonTr.localPosition.x > 0)
        {
            offset2 -= 70f;
            move = true;
        }
    }
    private void Update()
    {
        MovingFon();
        MovingCircle();
    }
    public void ButtonOpenLevelsPanel()
    {
        PanelLevels.SetActive(!PanelLevels.activeInHierarchy);
    }
    public void ButtonContinue()
    {
        SceneManager.LoadScene("GameLevel1");
    }
    public void ButtonForAllTime()
    {
        Panel4.transform.GetChild(0).GetComponent<Text>().text = "Your Salary For All Time:\n\n" + data.LvlRec.Sum();
    }
    public void ButtonSettingsOrClosePanel1()
    {
        PanelSwitcher(Panel1);
        ButtonSoundSwitcher();
    }
    public void ButtonOpenOrClosePanel2() => PanelSwitcher(Panel2);
    public void ButtonOpenOrClosePanel3() => PanelSwitcher(Panel3);
    public void ButtonOpenOrClosePanel4() => PanelSwitcher(Panel4);
    private void PanelSwitcher(GameObject pan) //сокращение
    {
        pan.SetActive(!pan.activeInHierarchy);
    }
    public void DeleteRecords() //если согласился удалить рекорды
    {
        data.ResetData();
        ButtonOpenOrClosePanel2();
    }
    public void MakeButtonsBlocked() => ButtonsRaycast(false);
    public void MakeButtonsUnblocked() => ButtonsRaycast(true);
    private void ButtonsRaycast(bool yesorno)
    {
        Canvas.transform.GetChild(1).GetComponent<Button>().enabled = yesorno;
        Canvas.transform.GetChild(2).GetComponent<Button>().enabled = yesorno;

        Panel1.transform.GetChild(0).GetComponent<Button>().enabled = yesorno;
        Panel1.transform.GetChild(1).GetComponent<Button>().enabled = yesorno;
        Panel1.transform.GetChild(2).GetComponent<Button>().enabled = yesorno;
        Panel1.transform.GetChild(3).GetComponent<Button>().enabled = yesorno;

        Panel1.transform.GetChild(7).GetComponent<Button>().enabled = yesorno;
    }
    public void ButtonSoundSwitcher()
    {
        if (soundChanged == "false") ForAudioSwitcher("Sounds Off", 0);
        else ForAudioSwitcher("Sounds On", 1);
    }
    private void ForAudioSwitcher(string text, int vol)
    {
        Panel1.transform.GetChild(7).GetChild(0).GetComponent<Text>().text = text;
        AudioListener.volume = vol;
    }
    public void ButtonSounds()
    {
        RecData.CangeAndSaveStateOfSound(ref soundChanged);
        ButtonSoundSwitcher();
    }
    public void SoundsOfButtons()
    {
        audioSource[1].Play();
    }
    public void ButtonForChoosingLevel()
    {
        data.ContinueGame = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        SceneManager.LoadScene("GameLevel1");
    }
    public void SaveDog()
    {
        if (Fire.activeInHierarchy)
        {
            ButtonFire.transform.GetChild(0).GetComponent<Text>().text = "Fire is off";
            Fire.SetActive(false);
        }
        else
        {
            Fire.SetActive(true);
            ButtonFire.transform.GetChild(0).GetComponent<Text>().text = "Fire is on";
        }
    }

}
