using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour
{
    [SerializeField] private int _explosionPower = 700;
    [SerializeField] private float _explosionRadius = 3;
    [SerializeField] private ParticleSystem _effect;

    public void Explosion (Cube cube, List<Cube> spawnedCubes)
    {
        Vector3 explodePosition = cube.transform.position;
        float explodeScale = cube.Scale;        

        Destroy(cube.gameObject);

        if(spawnedCubes.Count == 0)
        {
            Collider[] hits = Physics.OverlapSphere(explodePosition, (_explosionRadius / explodeScale));

            foreach(Collider hit in hits)
            {
                if(hit.transform.TryGetComponent(out Cube overlappedCube))
                {
                    Vector3 direction = hit.transform.position - explodePosition;
                    float distance = direction.magnitude;
                    float impactPower = _explosionPower / distance / explodeScale;

                    if (impactPower > 0)
                        overlappedCube.Rigidbody.AddForce(direction * impactPower);
                }
            }

            Instantiate(_effect, explodePosition, Quaternion.identity);
        }
        else
        {
            foreach(Cube spawnedCube in spawnedCubes)
            {
                spawnedCube.Rigidbody.AddForce((spawnedCube.transform.position - explodePosition) * _explosionPower);
            }
        }
    }
}
