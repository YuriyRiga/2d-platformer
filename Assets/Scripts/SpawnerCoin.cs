using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using System;

public class SpawnerCoin : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private List<SpawnPointCoin> _spawnPoints;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private int _poolMaxSize = 5;
    [SerializeField] private float _repeatRate = 5f;

    private ObjectPool<Coin> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => InstantiateAndSetup(),
            actionOnGet: (coin) => OnGetPool(coin),
            actionOnRelease: (coin) => OnReleasePool(coin),
            actionOnDestroy: (coin) => Destroy(coin),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);

    }

    private void Start()
    {
        StartCoroutine(SpawnCooldown());
    }

    private Coin InstantiateAndSetup()
    {
        Coin coin = Instantiate(_prefab);
        coin.CoinDisable += Release;
        return coin;
    }

    private void OnGetPool(Coin coin)
    {
        int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Count);
        SpawnPointCoin spawnPoint = _spawnPoints[randomIndex];
        coin.transform.position = spawnPoint.transform.position;
        coin.gameObject.SetActive(true);
    }

    private void OnReleasePool(Coin coin)
    {
        coin.gameObject.SetActive(false);
    }

    private void Release(Coin coin)
    {
        _pool.Release(coin);
    }

    private void Unsubscribe (Coin coin)
    {
        coin.CoinDisable -= OnReleasePool;
    }

    private void GetCoin()
    {
        _pool.Get();
    }

    private IEnumerator SpawnCooldown()
    {
        while (gameObject.activeSelf)
        {
            GetCoin();
            yield return new WaitForSeconds(_repeatRate);
        }
    }
}
