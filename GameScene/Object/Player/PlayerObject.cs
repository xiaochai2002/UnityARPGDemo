using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : PlayerBase
{
    private int nowhp;
    private int maxhp;
    private int nowmana;
    private int maxmana;
    private int nowspeed;
    private int maxspeed;
    private int atk;
    private float nowRuntime = 0;
    private float nowidletime = 0;
    public float routateSpeed = 50;
    public bool IsPlotDialog;
    private PlayerInfo playerInfo;
    private GameMainPanel gameMainpanel;
    public Transform targetPos;
    private WeaponsObject weapons;
    private Transform canmerPos;

    protected override void Awake()
    {
        base.Awake();
        playerInfo = GameDataMgr.Instance.playerInfo;
        nowhp = maxhp = playerInfo.hp;
        nowmana = maxmana = playerInfo.mana;
        nowspeed = maxspeed = playerInfo.speed; 
        atk = playerInfo.atk;
        canmerPos = GameObject.Find("MapCamera").transform;
        canmerPos.SetParent(this.transform);
    }

    private void Start()
    {
        UIMgr.Instance.GetPanel<GameMainPanel>((t) => gameMainpanel = t);
    }

    private void Update()
    {
        if (PlayerInputData.ButtonVector2 != Vector2.zero && PlayerInputData.ShiftDown)
        {
            ChangeValueRun();
        }
        else
        {
            ChangeValueidle();
        }

        if (!IsPlotDialog)
        {
            this.transform.Rotate(Vector3.up * PlayerInputData.MoveVector2.x * routateSpeed * Time.deltaTime);
            SetValue<float>("XSpeed", PlayerInputData.ButtonVector2.x);
            SetValue<float>("YSpeed", PlayerInputData.ButtonVector2.y);
        }

        if (PlayerInputData.EDown)
        {
            EDownChangeEvent();
        }

        if (PlayerInputData.LeftMoveDown && weapons!=null)
        {
            SetValue<string>("atk");
            PlayerInputData.LeftMoveDown = false;
        }

        if (PlayerInputData.Key1Down)
            addHp(5);
    }

    public void CheckKeyState(bool isShift)
    {
        if (isShift)
        {
            SetLayerWegit("RunLayer", 1);
            SetLayerWegit("MoveLayer", 0);
            return;
        }
        SetLayerWegit("RunLayer", 0);
        SetLayerWegit("MoveLayer", 1);
    }

    public void ChangeValueRun()
    {
        if (nowspeed <= 0)
            return;

        nowRuntime += Time.deltaTime;
        if (nowRuntime >= 1)
        {
            nowRuntime = 0;
            nowspeed -= 2;
            if (nowspeed <= 0)
                nowspeed = 0;

            gameMainpanel.Changenl(nowspeed, maxspeed);
        }

    }

    public void Weaponsatk()
    {
        weapons.IsAtk(true);
    }

    public void ChangeValueidle()
    {
        if (nowspeed >= 100)
            return;

        nowidletime += Time.deltaTime;
        if (nowidletime >= 1)
        {
            nowidletime = 0;
            nowspeed += playerInfo.timespeed;
            if (nowspeed >= 100)
                nowspeed = 100;

            gameMainpanel.Changenl(nowspeed, maxspeed);
        }
    }

    private void EDownChangeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 3, 1 << LayerMask.NameToLayer("Npc"));
        if (colliders.Length <= 0)
            return;

        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject obj = colliders[i].gameObject;
            NpcObject npc = obj.GetComponent<NpcObject>();
            if (npc != null)
            {
                UIMgr.Instance.HidePanel<GameMainPanel>(true, false, () =>
                {
                    UIMgr.Instance.ShowPanel<PloyDialoguePanel>(E_UILayer.System, false, (p) =>
                    {
                        IsPlotDialog = true;
                        CameraMove c = Camera.main.gameObject.GetComponent<CameraMove>();
                        c.npctarget = obj.transform;
                        c.Isnpc = true;
                        c.offestPos.z = -1;
                        obj.GetComponent<NpcObject>().SetAnimator("isTalk", true);
                        p.GetNowPlotInfo(npc.npcId, npc.npcName,npc);
                    });
                });
            }
        }
    }

    public void Task2InfoOver()
    {
        UIMgr.Instance.GetPanel<GameMainPanel>((p) =>
        {
            p.Task2InfoOver();
            p.Showweap();
            targetPos.gameObject.SetActive(true);
            weapons = targetPos.GetChild(0).GetComponent<WeaponsObject>();
            weapons.init(atk);
        });
    }

    public void Wound(int atk)
    {
        if (nowhp <= 0)
            return;

        nowhp -= atk;

        UIMgr.Instance.GetPanel<GameMainPanel>((p) =>
        {
            p.Changehp(nowhp, maxhp);
        });
    }

    public void addHp(int atk)
    {
        if (nowhp >= 100 || nowhp <=0)
            return;

        nowhp += atk;
        if (nowhp > 100)
            nowhp = 100;

        UIMgr.Instance.GetPanel<GameMainPanel>((p) =>
        {
            p.Changehp(nowhp, maxhp);
            p.ShowTxtJL("使用血瓶，血量加5");
        });
    }

    public void FootR() { }
    public void FootL() { }
}
