using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerSliderSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private string _mixerVolume = "MixerVolume";
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetMixerVolume);
    }
    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetMixerVolume);
    }

    public void SetMixerVolume(float volume)
    {
        float minValue = -80;
        float multiplier = 20;

        _mixer.audioMixer.SetFloat(_mixerVolume, Mathf.Log10(volume) * multiplier);

        if (volume == 0)
        {
            _mixer.audioMixer.SetFloat(_mixerVolume, minValue);
        }
    }
}
