using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    protected Animator animator;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void SetValue<T>(string name, T obj = default(T))
    {
        if (obj is float objf)
            animator.SetFloat(name, objf);
        else if (obj == null)
            animator.SetTrigger(name);
        else if (obj is bool objb)
            animator.SetBool(name, objb);
    }

    protected void SetLayerWegit(string name,int wegit)
    {
        animator.SetLayerWeight(animator.GetLayerIndex(name), wegit);
    }
}
