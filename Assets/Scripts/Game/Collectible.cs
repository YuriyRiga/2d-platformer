using System.Collections;
using UnityEngine;
using System;

public abstract class Collectible : MonoBehaviour
{
    [SerializeField] private float _timeLife = 5f;

    public event Action<Collectible> CollectibleDisable;

    private void OnEnable()
    {
        StartCoroutine(DeleteAfterTime());
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        CollectibleDisable?.Invoke(this);
    }

    private IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(_timeLife);
        Deactivate();
    }
}