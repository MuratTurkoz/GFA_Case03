using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace GFA.Case03.MatchSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawnData _enemySpawnData;
        [SerializeField] private MatchInstance _matchInstance;
        [SerializeField] private ObjectPool _objectPool;
        private Camera _camera;
        private Plane _plane = new Plane(Vector3.up, Vector3.zero);
        [SerializeField] private float _offset;
        [SerializeField] private float _spawnRate;
        [SerializeField] private int _spawnCount = 50;
        [SerializeField] GameObject _prefabEnemy;
        private GameObject[] _pooledObjects;
        private int _currentSpawnedIndex = 0;
        private int _currentSpawnedObjectIndex;

        private void Awake()
        {
            _camera = Camera.main;
            CreatePoolObjects();
        }

        private void CreatePoolObjects()
        {
            int totalSpawnCount = 0;
            foreach (var entry in _enemySpawnData.Entries)
            {
                totalSpawnCount += entry.SpawnCount;
            }

            _pooledObjects = new GameObject[totalSpawnCount];

            int currentSpawnedIndex = 0;
            foreach (var entry in _enemySpawnData.Entries)
            {
                for (int i = 0; i < entry.SpawnCount; i++)
                {
                    var objToSpawn = entry.Prefabs[Random.Range(0, entry.Prefabs.Length)];
                    var inst = Instantiate(objToSpawn, Vector3.zero, Quaternion.identity);
                    inst.SetActive(false);
                    _pooledObjects[currentSpawnedIndex] = inst;

                    currentSpawnedIndex++;
                }
            }
        }
        private void Start()
        {
            StartCoroutine(nameof(CreateEnemy));
        }
        private IEnumerator CreateEnemy()
        {
            
    //    {
    //        while (true)
    //        {
    //            yield return new WaitForSeconds(_spawnRate);

            //            if (!_enemySpawnData.TryGetEntryByTime(_matchInstance.Time, out SpawnEntry entry)) continue;
            //            var spawnPerCall = ((float)entry.SpawnCount / entry.Duration) * _spawnRate;

            //            for (int i = 0; i < spawnPerCall; i++)
            //            {
            //                var viewportPoint = GetViewportPoint(out var offset);

            //                var ray = _camera.ViewportPointToRay(viewportPoint);

            //                if (_plane.Raycast(ray, out float enter))
            //                {
            //                    var worldPosition = ray.GetPoint(enter) + offset;
            //                    var inst = _pooledObjects[_currentSpawnedObjectIndex];

            //                    inst.transform.position = worldPosition;

            //                    inst.SetActive(true);


            //                    _currentSpawnedObjectIndex++;
            //                }
            //            }
            //        }
            //    }
            while (true)
            {
                //if (GameSession.Instance)
                yield return new WaitForSeconds(_spawnRate);
                if (!_enemySpawnData.TryGetEntryByTime(_matchInstance.Time, out SpawnEntry entry)) continue;
                var spawnPerCall = ((float)entry.SpawnCount / entry.Duration) * _spawnRate;


                for (int i = 0; i < spawnPerCall; i++)
                {
                    var viewportPoint = GetViewportPoint(out var offset);

                    var ray = _camera.ViewportPointToRay(viewportPoint);

                    if (_plane.Raycast(ray, out float enter))
                    {
                        var worldPosition = ray.GetPoint(enter) + offset;
                        //var inst = _objectPool.GetPooledObject(0); 
                        var inst = _pooledObjects[_currentSpawnedObjectIndex];
                        inst.transform.position = worldPosition;
                        inst.SetActive(true);
                        _currentSpawnedObjectIndex++;
                    }
                }

                //for (int i = 0; i < spawnPerCall; i++)
                //{
                //    var viewportPoint = GetViewportPoint(out var offset);

                //    var ray = _camera.ViewportPointToRay(viewportPoint);

                //    if (_plane.Raycast(ray, out float enter))
                //    {
                //        var worldPosition = ray.GetPoint(enter) + offset;
                //        var inst = _pooledObjects[_currentSpawnedIndex];

                //        inst.transform.position = worldPosition;

                //        inst.SetActive(true);


                //        _currentSpawnedIndex++;
                //    }
                //}
            }
        }
        private Vector3 GetViewportPoint(out Vector3 offset)
        {
            var viewportPoint = Vector3.zero;

            offset = Vector3.zero;

            if (Random.value > 0.5f)
            {
                var dir = Mathf.Round(Random.value);
                viewportPoint = new Vector3(dir, Random.value);

                offset = GetSpawnOffsetByViewportPosition(Vector3.right, dir < 0.001f ? -1f : 1f);
            }
            else
            {
                var dir = Mathf.Round(Random.value);
                viewportPoint = new Vector3(Random.value, dir);

                offset = GetSpawnOffsetByViewportPosition(Vector3.forward, dir < 0.001f ? -1f : 1f);
            }

            return viewportPoint;
        }

        private Vector3 GetSpawnOffsetByViewportPosition(Vector3 vector, float sign)
        {
            return vector * sign * _offset;
        }

    }
    //    private Camera _camera;

    //    private Plane _plane = new Plane(Vector3.up, Vector3.zero);

    //    [SerializeField] private MatchInstance _matchInstance;

    //    [SerializeField] private EnemySpawnData _enemySpawnData;

    //    [SerializeField] private float _offset;

    //    [SerializeField] private float _spawnRate;

    //    private GameObject[] _pooledObjects;

    //    private int _currentSpawnedObjectIndex;

    //    private void Awake()
    //    {
    //        _camera = Camera.main;
    //        CreatePoolObjects();
    //    }

    //    private void CreatePoolObjects()
    //    {
    //        int totalSpawnCount = 0;
    //        foreach (var entry in _enemySpawnData.Entries)
    //        {
    //            totalSpawnCount += entry.SpawnCount;
    //        }

    //        _pooledObjects = new GameObject[totalSpawnCount];

    //        int currentSpawnedIndex = 0;
    //        foreach (var entry in _enemySpawnData.Entries)
    //        {
    //            for (int i = 0; i < entry.SpawnCount; i++)
    //            {
    //                var objToSpawn = entry.Prefabs[Random.Range(0, entry.Prefabs.Length)];
    //                var inst = Instantiate(objToSpawn, Vector3.zero, Quaternion.identity);
    //                inst.SetActive(false);
    //                _pooledObjects[currentSpawnedIndex] = inst;

    //                currentSpawnedIndex++;
    //            }
    //        }
    //    }

    //    private void Start()
    //    {
    //        StartCoroutine(CreateEnemy());
    //    }

    //    private Vector3 GetSpawnOffsetByViewportPosition(Vector3 vector, float sign)
    //    {
    //        return vector * sign * _offset;
    //    }


    //    private IEnumerator CreateEnemy()
    //    {
    //        while (true)
    //        {
    //            yield return new WaitForSeconds(_spawnRate);

    //            if (!_enemySpawnData.TryGetEntryByTime(_matchInstance.Time, out SpawnEntry entry)) continue;
    //            var spawnPerCall = ((float)entry.SpawnCount / entry.Duration) * _spawnRate;

    //            for (int i = 0; i < spawnPerCall; i++)
    //            {
    //                var viewportPoint = GetViewportPoint(out var offset);

    //                var ray = _camera.ViewportPointToRay(viewportPoint);

    //                if (_plane.Raycast(ray, out float enter))
    //                {
    //                    var worldPosition = ray.GetPoint(enter) + offset;
    //                    var inst = _pooledObjects[_currentSpawnedObjectIndex];

    //                    inst.transform.position = worldPosition;

    //                    inst.SetActive(true);


    //                    _currentSpawnedObjectIndex++;
    //                }
    //            }
    //        }
    //    }

    //    private Vector3 GetViewportPoint(out Vector3 offset)
    //    {
    //        var viewportPoint = Vector3.zero;

    //        offset = Vector3.zero;

    //        if (Random.value > 0.5f)
    //        {
    //            var dir = Mathf.Round(Random.value);
    //            viewportPoint = new Vector3(dir, Random.value);

    //            offset = GetSpawnOffsetByViewportPosition(Vector3.right, dir < 0.001f ? -1f : 1f);
    //        }
    //        else
    //        {
    //            var dir = Mathf.Round(Random.value);
    //            viewportPoint = new Vector3(Random.value, dir);

    //            offset = GetSpawnOffsetByViewportPosition(Vector3.forward, dir < 0.001f ? -1f : 1f);
    //        }

    //        return viewportPoint;
    //    }
    //}
}



