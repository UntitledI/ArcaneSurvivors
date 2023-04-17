using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;
<<<<<<< HEAD

=======
    // Start is called before the first frame update
>>>>>>> 1d8a3005 (Added Combat Animations)
    void Start()
    {
        anim = GetComponent<Animator>();
    }

<<<<<<< HEAD
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
            {
                anim.SetBool("Hit1", false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
            {
                anim.SetBool("Hit2", false);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit3"))
            {
                anim.SetBool("Hit3", false);
            }
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
=======
    // Update is called once per frame
    void Update()
    {
        
>>>>>>> 1d8a3005 (Added Combat Animations)
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
<<<<<<< HEAD

        if (noOfClicks == 1)
        {
            anim.SetBool("Hit1", true);
        }

        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            anim.SetBool("Hit1", false);
            anim.SetBool("Hit2", true);
            Debug.Log("Hit1 to Hit2 transition");
        }

        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
        {
            anim.SetBool("Hit2", false);
            anim.SetBool("Hit3", true);
            Debug.Log("Hit2 to Hit3 transition");
        }
    }
}


=======
        if(noOfClicks == 1)
        {

        }
    }
}
>>>>>>> 1d8a3005 (Added Combat Animations)
