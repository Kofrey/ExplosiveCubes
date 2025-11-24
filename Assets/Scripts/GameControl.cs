using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster; 
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private Exploder _exploder;

    private int _maxRespawnChance = 100;
    private int _minRespawnChance = 1;

    private void OnEnable() {
        _raycaster.ObjectClicked += OnObjectClicked;
    }

    private void OnDisable() {
        _raycaster.ObjectClicked -= OnObjectClicked;
    }

    private void OnObjectClicked(RaycastHit hit)
    {
        if(hit.transform.TryGetComponent(out Cube cube))
            OnCubeClicked(cube);
    }

    private void OnCubeClicked(Cube cube)
    { 
        List<Cube> spawnedCubes = new List<Cube>();

        if(UserUtils.GenerateRandomNumber(_minRespawnChance, _maxRespawnChance) <= cube.RespawnChance)  
            spawnedCubes = _cubeFactory.CubesSpawnOnExplosion(cube);    

        _exploder.Explosion(cube, spawnedCubes);
    }
}
