using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraOfDeath : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float radius = 10f;
    [SerializeField] private float sphereY = 0f;
    [SerializeField] private float collisionY = 0f;

    private void OnDrawGizmosSelected()
    {
        if (target == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector3(target.position.x, sphereY, target.position.z), radius);
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(new Vector3(target.position.x, collisionY, target.position.z), radius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Level 1"))
            {
                Destroy(collider.gameObject);
            }
        }
    }
}