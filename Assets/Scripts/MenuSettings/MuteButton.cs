using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private Button _buttonsMute;

    private void OnEnable()
    {
        _buttonsMute.onClick.AddListener(ButtonMute);
    }

    private void OnDisable()
    {
        _buttonsMute.onClick.RemoveListener(ButtonMute);
    }

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
}
