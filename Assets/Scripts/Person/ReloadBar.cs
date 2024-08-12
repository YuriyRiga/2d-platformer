using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private VampireAttack _attack;

    private float _targetFillValue = 1;
    private Coroutine _fillBarCoroutine;

    private void Start()
    {
        _image.transform.localScale = Vector3.one * _attack.AttackRange;
    }

    private void OnEnable()
    {
        _attack.ChangeTimer += OnChangeTimer;
    }

    private void OnDisable()
    {
        _attack.ChangeTimer -= OnChangeTimer;
    }

    private void OnChangeTimer(float current, float max)
    {
        if (_fillBarCoroutine != null)
        {
            StopCoroutine( _fillBarCoroutine );
        }
        _targetFillValue = current / max;

        _fillBarCoroutine = StartCoroutine(FillBar());
    }

    private IEnumerator FillBar()
    {
        while (_image.fillAmount != _targetFillValue)
        {
            _image.fillAmount = Mathf.MoveTowards(_image.fillAmount, _targetFillValue, Time.deltaTime);
            yield return null;
        }

        _fillBarCoroutine = null;
    }
}
