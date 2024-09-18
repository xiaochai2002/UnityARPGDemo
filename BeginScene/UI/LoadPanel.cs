using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadPanel : BasePanel
{
    private TextMeshProUGUI txtloading;
    private AsyncOperation ao;
    private float target;
    private float nowCurrent;
    private float Speed = 210;
    private bool isLoadScene;
    protected override void Awake()
    {
        base.Awake();
        alpanSpeed = 3;
    }

    private void Start()
    {
        txtloading = GetControl<TextMeshProUGUI>("txtLoading");
        EventCenter.Instance.AddEventListener<AsyncOperation>(E_EventType.E_AsynSceneAo, ScenePace);
        EventCenter.Instance.AddEventListener(E_EventType.E_SceneSpace, () =>
        {
            ao.allowSceneActivation = true;
            PoolMgr.Instance.ClearPool();
            MusicMgr.Instance.ClearSound();
            EventCenter.Instance.Clear();
            ao.completed += (obj) =>
            {
                GameMain.Instance.InitGameInfo();
                UIMgr.Instance.HidePanel<LoadPanel>();
                UIMgr.Instance.ShowPanel<GameMainPanel>(E_UILayer.System);
            };
        });
    }

    public void ScenePace(AsyncOperation ao)
    {
        this.ao = ao;
        target = ao.progress * 100;
    }

    protected override void Update()
    {
        base.Update();
        if (target == 0 || isLoadScene)
            return;

        nowCurrent = Mathf.MoveTowards(nowCurrent, target, Speed * Time.deltaTime);
        nowCurrent = (float)Math.Truncate(nowCurrent);
        if (nowCurrent != 90)
            txtloading.text = $"{nowCurrent}/100%";
        else
        {
            isLoadScene = true;
            txtloading.text = $"100/100%";
            InputMgr.Instance.StartOrCloseInputMgr(true);
            InputMgr.Instance.ChangeKeyboardInfo(E_EventType.E_SceneSpace, KeyCode.Space, InputInfo.E_InputType.Down);
            GetControl<TextMeshProUGUI>("txTips").gameObject.SetActive(true);

        }

    }

}
