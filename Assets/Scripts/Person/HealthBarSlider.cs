using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarSlider : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Health _player;

    private float _targetSliderValue = 1;
    private Coroutine _smoothFillCoroutine;

    private void OnEnable()
    {
        _healthSlider.value = _targetSliderValue;
        _player.ChangeHealth += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.ChangeHealth -= OnHealthChanged;
    }

    private void OnHealthChanged(float health, float maxhealth)
    {
        if (_smoothFillCoroutine != null)
        {
            StopCoroutine(_smoothFillCoroutine);
        }

        _targetSliderValue = health / maxhealth;
        _smoothFillCoroutine = StartCoroutine(SmoothFill());
    }

    private IEnumerator SmoothFill()
    {
        while (_healthSlider.value != _targetSliderValue)
        {
            _healthSlider.value = Mathf.MoveTowards(_healthSlider.value, _targetSliderValue, Time.deltaTime);
            yield return null;
        }

        _smoothFillCoroutine = null;
    }
}
