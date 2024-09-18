using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMainPanel : BasePanel
{
    private Image nl;
    private TextMeshProUGUI txtnl;
    private float nlWidth;
    private Image hp;
    private TextMeshProUGUI txthp;
    private Image mp;
    private TextMeshProUGUI txtmp;
    private TextMeshProUGUI txtmes;
    private TextMeshProUGUI txtTaskTitle;
    private TextMeshProUGUI txtTaskDetails;
    private TaskInfo NowtaskInfo;
    private Image imgweap;
    private RawImage map;
    private RenderTexture rendertext;
    protected override void Awake()
    {
        alpanSpeed = 5;
        base.Awake();
        map = GetControl<RawImage>("imgmap");
        nl = GetControl<Image>("imgNl");
        hp = GetControl<Image>("imgHp");
        mp = GetControl<Image>("imgMp");
        imgweap = GetControl<Image>("imgweap");
        txtnl = GetControl<TextMeshProUGUI>("txtNl");
        txthp = GetControl<TextMeshProUGUI>("txtHp");
        txtmp = GetControl<TextMeshProUGUI>("txtMp");
        txtTaskTitle = GetControl<TextMeshProUGUI>("txtTaskTitle");
        txtTaskDetails = GetControl<TextMeshProUGUI>("txtTaskDetails");
        txtmes = GetControl<TextMeshProUGUI>("txtmes");
        nlWidth = this.nl.rectTransform.sizeDelta.x;
        NowtaskInfo = GameDataMgr.Instance.taskInfosList.Find((t) => t.taskid == 1);
    }

    private void Start()
    {
        EventCenter.Instance.AddEventListener(E_EventType.E_Task, Task1InfoOver);
        ChangeUpdateTaskInfo(NowtaskInfo);
        imgweap.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
    }

    public void Changehp(int hp, int maxhp)
    {
        txthp.text = $"{hp}/{maxhp}";

        this.hp.rectTransform.sizeDelta = new Vector2((float)hp / maxhp * nlWidth, 20);
    }
    public void Changenl(int nl, int maxnl)
    {
        txtnl.text = $"{nl}/{maxnl}";

        this.nl.rectTransform.sizeDelta = new Vector2((float)nl / maxnl * nlWidth, 20);

    }

    public void ChangeUpdateTaskInfo(TaskInfo nowtask)
    {
        txtTaskTitle.text = nowtask.title;
        txtTaskDetails.text = nowtask.describe;
    }

    public void Task1InfoOver()
    {
        NowtaskInfo = GameDataMgr.Instance.taskInfosList.Find((t) => t.taskid == NowtaskInfo.current);
        ChangeUpdateTaskInfo(NowtaskInfo);
    }

    public void Task2InfoOver()
    {
        MusicMgr.Instance.PlaySound("skype");
        txtmes.text = "»ñµÃÌú½£X1";
        Invoke("ShowTxt", 3);
    }

    private void ShowTxt()
    {
        txtmes.text = "";
    }

    public void ShowTxtJL(string name)
    {
        MusicMgr.Instance.PlaySound("skype");
        txtmes.text = name;
        Invoke("ShowTxt", 3);
    }

    public void Showweap()
    {
        imgweap.gameObject.SetActive(true);
    }
}
