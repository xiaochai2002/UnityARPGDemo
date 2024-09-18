using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PloyDialoguePanel : BasePanel
{
    private Image bkClick;
    private TextMeshProUGUI txtName;
    private TextMeshProUGUI txtcontent;
    private PlotDialogueInfo nowPlotInfo;
    private NpcObject npcObject;
    private AudioClip saudio;

    protected override void Awake()
    {
        base.Awake();
        txtName = GetControl<TextMeshProUGUI>("txtName");
        txtcontent = GetControl<TextMeshProUGUI>("txtcontent");
        bkClick = GetControl<Image>("bkClick");
    }

    private void Start()
    {
        UIMgr.AddCustomEventListener(bkClick, EventTriggerType.PointerClick, ChangeUpdatetxt);
    }

    public void GetNowPlotInfo(string npcid, string name, NpcObject npc)
    {
        npcObject = npc;
        txtName.text = name;
        if (npcid == "1")
            nowPlotInfo = GameDataMgr.Instance.plotDialogueList.FindLast((p) => p.npcid == npcid && p.id == 1);
        else
            nowPlotInfo = GameDataMgr.Instance.plotDialogueList.FindLast((p) => p.npcid == npcid && p.id == 6);
        UpdateText(nowPlotInfo.content);
    }

    private void UpdateText(string Content)
    {
        txtcontent.text = Content;
    }

    public void ChangeUpdatetxt(BaseEventData baseEvent = null)
    {
        nowPlotInfo = GameDataMgr.Instance.plotDialogueList.Find((p) => p.id == nowPlotInfo.jump);
        switch (nowPlotInfo.idei)
        {
            case 1:
                UpdateText(nowPlotInfo.content);
                break;
            case 2:
                List<PlotDialogueInfo> list = new List<PlotDialogueInfo>();
                if (nowPlotInfo.idei == 2)
                    list = GameDataMgr.Instance.plotDialogueList.FindAll((p) => p.idei == 2);
                if (list.Count > 0)
                {
                    alpanSpeed = 5;
                    UIMgr.Instance.HidePanel<PloyDialoguePanel>(true, false, () =>
                    {
                        UIMgr.Instance.ShowPanel<DialogueSelectPanel>(E_UILayer.System, false, (p) =>
                        {
                            p.ChangeButtonTxt(list[0].content, list[1].content);
                        });
                    });
                }
                break;
            case 3:
                UIMgr.Instance.HidePanel<PloyDialoguePanel>(true, false, () =>
                {
                    PlayerInputMgr.Instance.playerObject.IsPlotDialog = false;
                    UIMgr.Instance.ShowPanel<GameMainPanel>(E_UILayer.System);
                    CameraMove c = Camera.main.gameObject.GetComponent<CameraMove>();
                    c.Isnpc = false;
                    c.offestPos.z = -2.99f;
                    EventCenter.Instance.EventTrigger(E_EventType.E_Task);
                    if (nowPlotInfo.npcid == "2")
                        PlayerInputMgr.Instance.playerObject.Task2InfoOver();

                    npcObject.SetAnimator("isTalk", false);

                    MusicMgr.Instance.PlaySound("skype");
                    Cursor.visible = false;
                });
                break;
        }
    }
}
