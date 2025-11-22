using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _minAmountCubesOnExplosion = 2;
    [SerializeField] private int _maxAmountCubesOnExplosion = 6;
    [SerializeField] private int _respawnChance = 100;

    private float _childScale = 0.5f;
    private int _respawnChanceFactor = 2;
    private int _explosionPower = 700;

    private void OnMouseDown() 
    {
        if(UserUtils.GenerateRandomNumber(1, 100) <= _respawnChance)
        {
            _respawnChance = _respawnChance / _respawnChanceFactor;
            CubesSpawn();    
        }

        Destroy(gameObject);    
    }

    private void CubesSpawn()
    {
        int cubesCount = UserUtils.GenerateRandomNumber(_minAmountCubesOnExplosion, _maxAmountCubesOnExplosion);

        for(int i = 0; i < cubesCount; ++i)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x + (-0.25f + 0.5f * (i % 2)) * transform.localScale.x, transform.position.y + (0.5f - 0.5f * (i / 4)) * transform.localScale.y, transform.position.z + 0.5f * (i / 2) * transform.localScale.z);
           
            Cube cube = Instantiate(this, spawnPosition, transform.rotation);  

            Transform cubeTransform = cube.GetComponent<Transform>();
            cubeTransform.localScale *= _childScale;
            cube.GetComponent<Rigidbody>().AddForce((cubeTransform.position - transform.position) * _explosionPower);
        }
    }
    
    private void OnEnable() 
    {
        _renderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
