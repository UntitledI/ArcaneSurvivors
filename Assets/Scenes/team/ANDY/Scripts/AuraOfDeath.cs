using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraOfDeath : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float radius = 10f;

    private void OnDrawGizmosSelected()
    {
        if (target == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(target.position, radius);
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(target.position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Level 1"))
            {
                Destroy(collider.gameObject);
            }
        }
    }
}
