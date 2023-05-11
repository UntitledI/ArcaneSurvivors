using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        animator.SetBool("SpinningAttackTrigger", true);
        yield return new WaitForSeconds(5);
        animator.SetBool("SpinningAttackTrigger", false);
    }
}
