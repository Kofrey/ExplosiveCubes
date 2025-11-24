using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField, Range(0f, 1f)] private float _hueMin = 0f;
    [SerializeField, Range(0f, 1f)] private float _hueMax = 1f;
    [SerializeField, Range(0f, 1f)] private float _saturationMin = 1f;
    [SerializeField, Range(0f, 1f)] private float _saturationMax = 1f;
    [SerializeField, Range(0f, 1f)] private float _valueMin = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _valueMax = 1f;

    private void Awake()
    {
        if(_renderer == null)
            _renderer = this.GetComponent<Renderer>();
    }

    private void OnEnable() 
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV(_hueMin, _hueMax, _saturationMin, _saturationMax, _valueMin, _valueMax);
    }
}
