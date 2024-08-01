using UnityEngine;
using System.Collections;
using System;

public class Heal : MonoBehaviour
{
    [SerializeField] private float _value = 25f;
    [SerializeField] private float _timeLife = 5f;

    public event Action<Heal> HealDisable;

    public float Value => _value;

    private void OnEnable()
    {
        StartCoroutine(DeleteAfterTime());
    }

    public void Deactivate ()
    {
        gameObject.SetActive(false);
        HealDisable?.Invoke(this);
    }

    private IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(_timeLife);
        Deactivate();
    }
}
