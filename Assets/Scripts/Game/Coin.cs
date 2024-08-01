using System.Collections;
using UnityEngine;
using System;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _timeLife = 5f;

    public event Action<Coin> CoinDisable;

    private void OnEnable()
    {
        StartCoroutine(DeleteAfterTime());
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        CoinDisable?.Invoke(this);
    }

    private IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(_timeLife);
        Deactivate();
    }
}
