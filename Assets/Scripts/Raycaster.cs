using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 100.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _maxDistance) && hit.collider.CompareTag("Interactable"))
            {
                hit.rigidbody.GetComponent<Cube>().OnClick();
            }
        }
    }
}
