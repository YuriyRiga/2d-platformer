using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainSettings : MonoBehaviour
{
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private AudioMixerGroup _masterMixer;
    [SerializeField] private AudioMixerGroup _buttonsMixer;
    [SerializeField] private AudioMixerGroup _backgroudMixer;

    public void ButtonMute()
    {
        if (_audioListener.enabled)
        {
            _audioListener.enabled = false;
        }
        else
        {
            _audioListener.enabled = true;
        }
    }

    public void SetMasterVolume(float volume)
    {
        _masterMixer.audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetButtonVolume(float volume)
    {
        _buttonsMixer.audioMixer.SetFloat("ButtonsVolume", Mathf.Log10(volume) * 20);
    }

    public void SetBackgroudVolume(float volume)
    {
        _backgroudMixer.audioMixer.SetFloat("BackgroundVolume", Mathf.Log10(volume) * 20);
    }
}
