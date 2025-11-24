using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _startingCubeRespawnTime = 3.5f; 
    [SerializeField] private int _minAmountCubesOnExplosion = 2;
    [SerializeField] private int _maxAmountCubesOnExplosion = 6;

    private int _respawnChanceFactor = 2;
    private float _childScale = 0.5f;
    private int _spawnedChildInLine = 2;
    private int _spawnedChildInPlane = 4;
    private float _shiftHalf = 0.5f;
    private float _shiftQuarter = 0.25f;

    private void Start()
    {
        StartCoroutine(GenerateCubes(_startingCubeRespawnTime));
    }
    
    public List<Cube> CubesSpawnOnExplosion(Cube explodedCube)
    {
        int cubesCount = UserUtils.GenerateRandomNumber(_minAmountCubesOnExplosion, _maxAmountCubesOnExplosion);
        Transform cubeTransform = explodedCube.transform;

        List<Cube> spawnedCubes = new List<Cube>();

        for(int i = 0; i < cubesCount; ++i)
        {
            Vector3 spawnPosition = new Vector3(cubeTransform.position.x + (-_shiftQuarter + _shiftHalf * (i % _spawnedChildInLine)) * cubeTransform.localScale.x, cubeTransform.position.y + (_shiftHalf - _shiftHalf * (i / _spawnedChildInPlane)) * cubeTransform.localScale.y, cubeTransform.position.z + _shiftHalf * (i / _spawnedChildInLine) * cubeTransform.localScale.z);
           
            Cube newCube = Instantiate(explodedCube, spawnPosition, cubeTransform.rotation);  

            newCube.SetRespawnChance(explodedCube.RespawnChance / _respawnChanceFactor);
            newCube.transform.localScale *= _childScale;

            spawnedCubes.Add(newCube);         
        }

        return spawnedCubes;
    }

    private IEnumerator GenerateCubes(float respawnTime)
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
                Cube cubeObject = Instantiate(_cubePrefab.gameObject).GetComponent<Cube>();
                elapsedTime += Time.deltaTime - respawnTime;
            }

            yield return null;
        }    
    }
}
