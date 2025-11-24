using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 

public class Cube : MonoBehaviour
{
    [SerializeField] private int _respawnChance = 100;
    [SerializeField] private Rigidbody _rigidbody;

    public int RespawnChance => _respawnChance;
    public Rigidbody Rigidbody => _rigidbody;
    public float Scale => transform.localScale.x;

    private void Awake()
    {
        if(_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetRespawnChance(int value)
    {
        _respawnChance = value;
    }
}
