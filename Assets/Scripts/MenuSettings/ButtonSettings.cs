using UnityEngine;
using UnityEngine.UI;

public class ButtonSettings : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private Button _button;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(PlayAudio);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PlayAudio);

    }

    private void PlayAudio()
    {
        _audioSource.clip = _clip;
        _audioSource.Play();
    }
}
