using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 

public class Cube : MonoBehaviour
{
    [SerializeField] private int _respawnChance = 100;

    public int RespawnChance => _respawnChance;
    public Rigidbody Rigidbody;
    public float Scale => transform.localScale.x;

    private void Awake()
    {
        if(Rigidbody == null)
            Rigidbody = this.GetComponent<Rigidbody>();
    }

    public void SetRespawnChance(int value)
    {
        _respawnChance = value;
    }
}
