using System.Collections;
using UnityEngine;
using System;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _timeLife = 5f;

    private Vector2 _startPosition;

    public event Action<Coin> CoinDisable;

    public void Deactivate()
    {
        gameObject.SetActive(false);
        CoinDisable?.Invoke(this);
    }

    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolAfterTime());
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        yield return new WaitForSeconds(_timeLife);
        Deactivate();
    }
}
