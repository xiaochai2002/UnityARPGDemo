using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsObject : MonoBehaviour
{
    private int atk;
    private bool isAtk;
    public void init(int atk)
    {
        this.atk = atk;
    }

    public void IsAtk(bool isatk)
    {
        isAtk = isatk;
    }

    private void Update()
    {
        if (isAtk)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 2, 1 << LayerMask.NameToLayer("Monster"));
            if (colliders.Length > 0)
            {
                MonsterObject monst = colliders[0].gameObject.GetComponent<MonsterObject>();
                if (!monst.IsDead) monst.Wound(atk);
            }
            isAtk = false;
        }
    }
}
