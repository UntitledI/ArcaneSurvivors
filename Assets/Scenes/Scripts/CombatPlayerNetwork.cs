using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class CombatPlayerNetwork : NetworkBehaviour
{
    private Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;

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
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;

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
        if (isLocalPlayer)
        {
            CmdSendClick(noOfClicks);
        }
    }

    [Command]
    void CmdSendClick(int clickCount)
    {
        RpcSendClick(clickCount);
        if (isServer && !isClient) SyncAnimationsForHost(clickCount);
    }




[ClientRpc]
void RpcSendClick(int clickCount)
{
    if (isLocalPlayer) return;

    noOfClicks = clickCount;

    if (noOfClicks == 1)
    {
        anim.SetBool("Hit1", true);
    }
    else if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
    {
        anim.SetBool("Hit1", false);
        anim.SetBool("Hit2", true);
    }
    else if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
    {
        anim.SetBool("Hit2", false);
        anim.SetBool("Hit3", true);
    }
}



    void SyncAnimationsForHost(int clickCount)
    {
        if (isLocalPlayer) return;

        noOfClicks = clickCount;

        if (noOfClicks == 1)
        {
            anim.SetBool("Hit1", true);
        }
        else if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            anim.SetBool("Hit1", false);
            anim.SetBool("Hit2", true);
        }
        else if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
        {
            anim.SetBool("Hit2", false);
            anim.SetBool("Hit3", true);
        }
    }

    void UpdateAnimationStates(int clickCount)
    {
        noOfClicks = clickCount;

        if (noOfClicks == 1)
        {
            anim.SetBool("Hit1", true);
        }
        else if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit1"))
        {
            anim.SetBool("Hit1", false);
            anim.SetBool("Hit2", true);
        }
        else if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit2"))
        {
            anim.SetBool("Hit2", false);
            anim.SetBool("Hit3", true);
        }
    }

}


