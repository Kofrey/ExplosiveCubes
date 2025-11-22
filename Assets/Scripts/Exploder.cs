using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour
{
    [SerializeField] private int _explosionPower = 700;

    public void Explosion (Cube cube, List<Transform> affectedTransforms)
    {
        foreach(Transform transform in affectedTransforms)
        {
            transform.GetComponent<Rigidbody>().AddForce((transform.position - cube.transform.position) * _explosionPower);
        }

        Destroy(cube.gameObject);
    }
}
