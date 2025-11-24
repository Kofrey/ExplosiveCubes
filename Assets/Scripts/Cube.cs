using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _respawnChance = 100;

    public int RespawnChance => _respawnChance;
    public Rigidbody Rigidbody => this.GetComponent<Rigidbody>();
    public float Scale => transform.localScale.x;

    private void OnEnable() 
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void SetRespawnChance(int value)
    {
        _respawnChance = value;
    }
}
