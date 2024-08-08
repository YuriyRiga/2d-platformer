using System.Collections;
using UnityEngine;
using System;

public abstract class Collectible : MonoBehaviour
{
    [SerializeField] private float _timeLife = 5f;

    public event Action<Collectible> CollectibleDisable;
    private bool _isDeactivated = false;

    private void OnEnable()
    {
        _isDeactivated = false;
        StartCoroutine(DeleteAfterTime());
    }

    public void Deactivate()
    {
        if (_isDeactivated) return;
        _isDeactivated = true;
        gameObject.SetActive(false);
        CollectibleDisable?.Invoke(this);
    }

    private IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(_timeLife);
        Deactivate();
    }
}