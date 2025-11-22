using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action MouseClick;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseClick?.Invoke();
        }
    }
}
