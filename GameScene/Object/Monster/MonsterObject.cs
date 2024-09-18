using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterObject : MonsterBase
{
    public float MoveSpeed = 2;
    public float pos = 1f;
    public int hp;
    public string monsterName;
    private int maxhp;
    public int atk;
    public bool IsDead => isDead;
    private bool isDead;
    protected override void Awake()
    {
        base.Awake();
        maxhp = hp;
    }

    private void Update()
    {
        if (CheckTransPos() && !isDead)
        {
            SetValue<bool>("IsWalk", true);
            if (MathUtil.CheckObjDistanceXZ(this.transform.position, PlayerInputMgr.Instance.playerObject.transform.position, pos))
            {
                SetValue<bool>("IsWalk", false);
                SetValue<bool>("Atk", true);
            }
            else
            {
                this.transform.Translate(this.transform.forward * MoveSpeed * Time.deltaTime);
                this.transform.rotation = Quaternion.LookRotation(PlayerInputMgr.Instance.playerObject.transform.position - this.transform.position);
            }
            return;
        }
        else
        {
            SetValue<bool>("IsWalk", false);
            SetValue<bool>("Atk", false);
        }

    }

    private bool CheckTransPos()
    {
        return MathUtil.IsInSectorRangeXZ(this.transform.position, this.transform.forward, PlayerInputMgr.Instance.playerObject.transform.position, 5, 180);
    }

    public void MonsterAtk()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 2, 1 << LayerMask.NameToLayer("Player"));
        if (colliders.Length > 0)
        {
            colliders[0].gameObject.GetComponent<PlayerObject>().Wound(atk);
        }
    }

    public void Wound(int woundhp)
    {
        if (hp > 0)
            hp -= woundhp;

        if (hp <= 0)
        {
            hp = 0;
            Dead();
        }
    }

    public void Dead()
    {
        UIMgr.Instance.GetPanel<GameMainPanel>((p) => {
            p.ShowTxtJL($"»÷°ÜÐ¡¹Ö{monsterName}x1");
        });
        isDead = true;
        SetValue<string>("Die");
        Destroy(this.gameObject, 4);
    }
}
