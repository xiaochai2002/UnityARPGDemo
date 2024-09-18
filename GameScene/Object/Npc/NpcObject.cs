using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcObject : MonoBehaviour
{
    public string npcId;
    public string npcName;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimator(string name,bool istalk)
    {
        animator.SetBool(name, istalk);
    }
}
