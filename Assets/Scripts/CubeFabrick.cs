using System;
using System.Collections;
using UnityEngine;

public class CubeFabrick : MonoBehaviour
{
    [SerializeField] private UnityEngine.Object _cubePrefab;
    [SerializeField] private float _startingCubeRespawnTime = 3.5f; 

    private void Start()
    {
        StartCoroutine(CubeGeneration(_startingCubeRespawnTime));
    }

    private IEnumerator CubeGeneration(float respawnTime)
    {
        float elapsedTime = 0f;
        
        while(true)
        {
            if (elapsedTime < respawnTime)
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                Instantiate(_cubePrefab);
                elapsedTime += Time.deltaTime - respawnTime;
            }

            yield return null;
        }    
    }
}
