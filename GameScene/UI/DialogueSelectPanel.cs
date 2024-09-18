using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueSelectPanel : BasePanel
{
    private Button selectBtn1;
    private Button selectBtn2;
    private List<SceneSoundInfo> soundList;

    protected override void Awake()
    {
        alpanSpeed = 5;
        base.Awake();
        soundList = GameDataMgr.Instance.sceneSoundList;
        selectBtn1 = GetControl<Button>("selectBtn1");
        selectBtn2 = GetControl<Button>("selectBtn2");
        UIMgr.AddCustomEventListener(selectBtn1,EventTriggerType.PointerEnter, BtnEnter);
        UIMgr.AddCustomEventListener(selectBtn2, EventTriggerType.PointerEnter, BtnEnter);
    }

    public void ChangeButtonTxt(string btn1txt,string btn2txt)
    {
        selectBtn1.GetComponentInChildren<TextMeshProUGUI>().text = btn1txt;
        selectBtn2.GetComponentInChildren<TextMeshProUGUI>().text = btn2txt;
    }

    protected override void ClickBtn(string btnName)
    {
        MusicMgr.Instance.PlaySound(soundList[0].name);
        UIMgr.Instance.HidePanel<DialogueSelectPanel>(true, false, () =>
        {
            UIMgr.Instance.ShowPanel<PloyDialoguePanel>(E_UILayer.System, false, (p) =>
            {
                p.ChangeUpdatetxt();

            });
        });

    }

    private void BtnEnter(BaseEventData baseEvent)
    {
        MusicMgr.Instance.PlaySound(soundList[1].name);
    }
}
