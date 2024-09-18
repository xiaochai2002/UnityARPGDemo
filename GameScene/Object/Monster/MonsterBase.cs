using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    private Animator animator;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void SetValue<T>(string name, T obj = default(T))
    {
        if (obj == null)
            animator.SetTrigger(name);
        else if (obj is bool objb)
            animator.SetBool(name, objb);
    }
}
