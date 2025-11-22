using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private UnityEngine.GameObject _cubePrefab;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private float _startingCubeRespawnTime = 3.5f; 
    [SerializeField] private int _minAmountCubesOnExplosion = 2;
    [SerializeField] private int _maxAmountCubesOnExplosion = 6;

    private int _respawnChanceFactor = 2;
    private float _childScale = 0.5f;

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
                UnityEngine.GameObject cubeObject = Instantiate(_cubePrefab);
                cubeObject.GetComponent<Cube>().Click += OnCubeClicked;
                elapsedTime += Time.deltaTime - respawnTime;
            }

            yield return null;
        }    
    }

    private void OnCubeClicked(Cube cube)
    { 
        List<Transform> transforms = new List<Transform>();

        if(UserUtils.GenerateRandomNumber(1, 100) <= cube.RespawnChance)
        {
            cube.SetRespawnChance(cube.RespawnChance / _respawnChanceFactor);
            
            transforms = CubesSpawnOnExplosion(cube);    
        }

        cube.Click -= OnCubeClicked;
        _exploder.Explosion(cube, transforms); 
    }

    private List<Transform> CubesSpawnOnExplosion(Cube explodedCube)
    {
        int cubesCount = UserUtils.GenerateRandomNumber(_minAmountCubesOnExplosion, _maxAmountCubesOnExplosion);
        Transform cubeTransform = explodedCube.transform;

        List<Transform> transforms = new List<Transform>();

        for(int i = 0; i < cubesCount; ++i)
        {
            Vector3 spawnPosition = new Vector3(cubeTransform.position.x + (-0.25f + 0.5f * (i % 2)) * cubeTransform.localScale.x, cubeTransform.position.y + (0.5f - 0.5f * (i / 4)) * cubeTransform.localScale.y, cubeTransform.position.z + 0.5f * (i / 2) * cubeTransform.localScale.z);
           
            GameObject newCube = Instantiate(explodedCube.gameObject, spawnPosition, cubeTransform.rotation);  

            newCube.GetComponent<Cube>().Click += OnCubeClicked;

            Transform newCubeTransform = newCube.GetComponent<Transform>();
            transforms.Add(newCubeTransform);
            newCubeTransform.localScale *= _childScale;
        }

        return transforms;
    }
}
