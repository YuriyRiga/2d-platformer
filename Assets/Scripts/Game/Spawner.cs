using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner <T> : MonoBehaviour where T : Collectible
{
    [SerializeField] private T _prefab;
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private int _poolMaxSize = 5;
    [SerializeField] private float _repeatRate = 5f;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(),
            actionOnGet: (item) => InitializePool(item),
            actionOnRelease: (item) => Disable(item),
            actionOnDestroy: (item) => Unsubscribe(item),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(SpawnCouldown());
    }

    private T Instantiate()
    {
        T item = Instantiate(_prefab);
        Subscribe(item);
        return item;
    }

    protected virtual void Subscribe(T item)
    {
        item.CollectibleDisable += OnCollectibleDisable;
    }

    protected virtual void Unsubscribe(T item)
    {
        item.CollectibleDisable -= OnCollectibleDisable;
        Destroy(item.gameObject);
    }

    private void OnCollectibleDisable(Collectible collectible)
    {
        Release(collectible as T);
    }

    private void InitializePool(T item)
    {
        int randomIndex = Random.Range(0, _spawnPoints.Count);
        SpawnPoint spawnPoint = _spawnPoints[randomIndex];
        item.transform.position = spawnPoint.transform.position;
        item.gameObject.SetActive(true);
    }

    private void Disable(T item)
    {
        item.gameObject.SetActive(false);
    }

    protected void Release(T item)
    {
        _pool.Release(item);
    }

    private void ActivateItem()
    {
        _pool.Get();
    }

    private IEnumerator SpawnCouldown()
    {
        var delay = new WaitForSeconds(_repeatRate);

        while (gameObject.activeSelf)
        {
            ActivateItem();
            yield return delay;
        }
    }
}
