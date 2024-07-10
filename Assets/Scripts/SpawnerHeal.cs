using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerHeal : MonoBehaviour
{
    [SerializeField] private Heal _prefab;
    [SerializeField] private List<SpawnPointHeal> _spawnPoints;
    [SerializeField] private int _poolCapacity = 3;
    [SerializeField] private int _poolMaxSize = 5;
    [SerializeField] private float _repeatRate = 5f;

    private ObjectPool<Heal> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Heal>(
            createFunc: () => InstantiateAndSetup(),
            actionOnGet: (heal) => OnGetPool(heal),
            actionOnRelease: (heal) => OnReleasePool(heal),
            actionOnDestroy: (heal) => Unsubscribe(heal),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(SpawnCouldown());
    }

    private Heal InstantiateAndSetup()
    {
        Heal heal = Instantiate(_prefab);
        heal.HealDisable += Release;
        return heal;
    }

    private void OnGetPool(Heal heal)
    {
        int randomIndex = Random.Range(0, _spawnPoints.Count);
        SpawnPointHeal spawnPoint = _spawnPoints[randomIndex];
        heal.transform.position = spawnPoint.transform.position;
        heal.gameObject.SetActive(true);
    }

    private void OnReleasePool (Heal heal)
    {
        heal.gameObject.SetActive(false);
    }

    private void Release (Heal heal)
    {
        _pool.Release(heal);
    }
    private void Unsubscribe(Heal heal)
    {
        heal.HealDisable -= Release;
        Destroy(heal);
    }

    private void GetHeal()
    {
        _pool.Get();
    }

    private IEnumerator SpawnCouldown()
    {
        while (gameObject.activeSelf)
        {
            GetHeal();
            yield return new WaitForSeconds(_repeatRate);
        }
    }
}
