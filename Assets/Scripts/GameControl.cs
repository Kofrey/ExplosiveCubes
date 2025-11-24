using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster; 
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private Exploder _exploder;

    private int _respawnChanceFactor = 2;

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
        List<Transform> transforms = new List<Transform>();

        if(UserUtils.GenerateRandomNumber(1, 100) <= cube.RespawnChance)
        {
            cube.SetRespawnChance(cube.RespawnChance / _respawnChanceFactor);
            
            transforms = _cubeFactory.CubesSpawnOnExplosion(cube);    
        }

        _exploder.Explosion(cube, transforms);
    }
}
