using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour
{
    [SerializeField] private int _explosionPower = 700;
    [SerializeField] private float _explosionRadius = 5;
    [SerializeField] private ParticleSystem _effect;

    public void Explosion (Cube cube, List<Transform> affectedTransforms)
    {
        Vector3 explodePosition = cube.transform.position;
        float explodeScale = cube.transform.localScale.x;        

        Destroy(cube.gameObject);

        if(affectedTransforms.Count == 0)
        {
            Collider[] hits = Physics.OverlapSphere(explodePosition, _explosionRadius);

            foreach(Collider hit in hits)
            {
                if(hit.transform.TryGetComponent(out Cube overlappedCube))
                {
                    Vector3 direction = hit.transform.position - explodePosition;
                    float magn = direction.magnitude;
                    float impactPower = _explosionPower / magn / explodeScale;

                    if (impactPower > 0)
                        overlappedCube.Rigidbody.AddForce(direction * impactPower);//(Vector3.Distance(hit.transform.position, cube.transform.position) * cube.transform.localScale.x));
                }
            }

            Instantiate(_effect, explodePosition, Quaternion.identity);
        }
        else
        {
            foreach(Transform transform in affectedTransforms)
            {
                transform.GetComponent<Rigidbody>().AddForce((transform.position - cube.transform.position) * _explosionPower);
            }
        }
    }
}
