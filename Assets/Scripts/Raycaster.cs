using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private float _maxDistance = 100.0f;

    public event Action<RaycastHit> ObjectClicked;

    private void OnEnable()
    {
        _inputHandler.MouseClick += Cast;
    }

    private void OnDisable() 
    {
        _inputHandler.MouseClick -= Cast;
    }

    private void Cast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _maxDistance))
        {
            ObjectClicked?.Invoke(hit);
        }
    }
}
