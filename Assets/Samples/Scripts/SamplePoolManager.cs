﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sample pool manager.
/// </summary>
public class SamplePoolManager : MonoBehaviour
{
    private const float SpawnDelay = 0.1f;
    private const float DestroyDelay = 1.0f;

    [SerializeField]
    private PoolableObject poolableObject;
    private Pool pool;

    public void Start()    
    {
        pool = new Pool(poolableObject);
        StartCoroutine(RepeatSpawnPoolableObject(SpawnDelay));
    }

    private IEnumerator RepeatSpawnPoolableObject(float _delay)
    {
        yield return new WaitForSeconds(_delay);

        PoolableObject obj = pool.GetOrCreate(this.transform);

        float randX = Random.Range(0.0f, 1.0f);
        float randY = Random.Range(0.0f, 1.0f);
        float randZ = Random.Range(0.0f, 1.0f);
        obj.GetComponent<Rigidbody>().velocity = new Vector3(randX, randY, randZ);
        obj.transform.localPosition = Vector3.zero;

        obj.Return2Pool(DestroyDelay);

        StartCoroutine(RepeatSpawnPoolableObject(_delay));
    }
}