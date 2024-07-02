using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private Coin InstantiateAndSetup()
    {
        Coin coin = Instantiate(_prefab);
        return coin;
    }

    private void ActionOnGet(Coin obj)
    {
        int randomIndex = Random.Range(0, _spawnPoints.Count);
        SpawnPointCoin spawnPoint = _spawnPoints[randomIndex];

        obj.SetPosition(spawnPoint.transform.position); 
        obj.gameObject.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCoin), 0.0f, _repeatRate);
    }

    private void GetCoin()
    {
        _pool.Get();
    }
}

