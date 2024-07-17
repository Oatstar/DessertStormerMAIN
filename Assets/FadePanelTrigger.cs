using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanelTrigger : MonoBehaviour
{
    public static FadePanelTrigger instance;
    Animator animator;

    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public void StartFade()
    {
        animator.SetTrigger("TriggerFade");
        Invoke("ResetTrigger", 0.5f);
    }

    void ResetTrigger()
    {
        animator.ResetTrigger("TriggerFade");
    }

    public void FadePanelFadingBack()
    {
        GameMasterManager.instance.NewFloor();
    }
}
