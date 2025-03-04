using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsAndCommands : MonoBehaviour
{
    private MyData data;
    private GameObject Canvas, PausePanel;
    private int indexOfLevel;
    private AudioClip[] audioClip;
    private AudioSource[] audioSource;
    private string soundChanged = "";
    private void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        data = GameObject.FindGameObjectWithTag("Saving").GetComponent<MyData>();
        PausePanel = Canvas.transform.GetChild(5).gameObject;
        indexOfLevel = data.ContinueGame + 1; //reading
        audioClip = new AudioClip[2];
        audioClip[0] = Resources.Load<AudioClip>("Sounds/click");
        audioClip[1] = Resources.Load<AudioClip>("Sounds/crowd");
        audioSource = GetComponents<AudioSource>();
        audioSource[0].clip = audioClip[0];
        audioSource[1].clip = audioClip[1];
        audioSource[1].loop = true;
        audioSource[1].Play();
        RecData.LoadStateOfSound(ref soundChanged);
        ButtonSoundSwitcher();
        audioSource[1].volume = 0;
        StartCoroutine(FadeOfSound(1f));
    }
    public void Scale(float scale) => Time.timeScale = scale;
    public void PauseButton() //גûםוסעט מעהוכüםמ
    {
        Scale(0f);
        PausePanel.SetActive(true);
        ButtonSoundSwitcher();
    }
    public void ButtonSoundSwitcher()
    {
        if (soundChanged == "false") ForAudioSwitcher("Sounds Off", 0);
        else ForAudioSwitcher("Sounds On", 1);
    }
    private void ForAudioSwitcher(string text, int vol)
    {
        PausePanel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = text;
        AudioListener.volume = vol;
    }
    public void SoundButton() //גûםוסעט מעהוכüםמ
    {
        RecData.CangeAndSaveStateOfSound(ref soundChanged);
        ButtonSoundSwitcher();
    }
    public void ContinueButton() //גûםוסעט מעהוכüםמ
    {
        PausePanel.SetActive(false);
        Scale(1f);
    }
    public void RetryButton() //גûםוסעט מעהוכüםמ
    {
        ExRecordSave(indexOfLevel - 1);
        data.ContinueGame = indexOfLevel - 1;
        SceneManager.LoadScene("GameLevel1");
        Scale(1f);
    }
    public void MenuButton() 
    {
        ExRecordSave(indexOfLevel - 1);
        ToLevelMenu();
        Scale(1f);
    }
    public void ExitButton() 
    {
        ExRecordSave(indexOfLevel - 1);
        Application.Quit();
    }
    public void NextLevel() 
    {
        if (RecData.youCompletedTheGame) //reading
        {
            Canvas.transform.GetChild(2).gameObject.SetActive(true);
            Invoke(nameof(ToLevelMenu), 5f);
        }
        else { SceneManager.LoadScene("GameLevel1"); }
    }
    private void ToLevelMenu() => SceneManager.LoadScene(0);
    private void ExRecordSave(int level)
    {
        if (transform.GetComponent<Game>().MySalary > data.LvlRec[level])
        {
            data.LvlRec[level] = transform.GetComponent<Game>().MySalary;
            data.SaveData();
        }
    }
    public void SoundOfButtons() => audioSource[0].Play();
    public IEnumerator FadeOfSound(float targetVolume)
    {
        float currentTime = 0;
        float startVolume = audioSource[1].volume;
        float duration = 15f;
        while (currentTime < 15f)
        {
            currentTime += Time.deltaTime;
            audioSource[1].volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
