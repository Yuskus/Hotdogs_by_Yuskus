using UnityEngine;

public class MoneySound : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip money;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        money = Resources.Load<AudioClip>("Sounds/money");
        audioSource.clip = money;
    }
    public void Play()
    {
        audioSource.Play();
    }
}
