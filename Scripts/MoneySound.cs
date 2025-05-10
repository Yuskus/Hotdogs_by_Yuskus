using UnityEngine;

public class MoneySound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Sounds/money");
    }

    public void Play()
    {
        audioSource.Play();
    }
}
