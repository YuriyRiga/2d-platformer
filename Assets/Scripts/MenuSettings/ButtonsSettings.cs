using UnityEngine;
using UnityEngine.UI;

public class ButtonsSettings : MonoBehaviour
{
    [SerializeField] private AudioClip _clip1;
    [SerializeField] private AudioClip _clip2;
    [SerializeField] private AudioClip _clip3;

    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;
    [SerializeField] private Button _button3;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _button1.onClick.AddListener(() => PlayAudio(_clip1));
        _button2.onClick.AddListener(() => PlayAudio(_clip2));
        _button3.onClick.AddListener(() => PlayAudio(_clip3));
    }

    private void PlayAudio(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
