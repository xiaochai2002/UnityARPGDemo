using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BeginPanel : BasePanel
{
    private Button btnStart;
    private Button btnSetting;
    private Button btnQuit;
    private TextMeshProUGUI txtStart;
    private TextMeshProUGUI txtSetting;
    private TextMeshProUGUI txtQuit;
    private List<SceneSoundInfo> soundList;
    protected override void Awake()
    {
        UpdateCameraXr();
        base.Awake();
        soundList = GameDataMgr.Instance.sceneSoundList;
    }

    private void Start()
    {
        btnStart = GetControl<Button>("btnStart");
        btnSetting = GetControl<Button>("btnSetting");
        btnQuit = GetControl<Button>("btnQuit");
        txtStart = btnStart.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        txtSetting = btnSetting.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        txtQuit = btnQuit.gameObject.GetComponentInChildren<TextMeshProUGUI>();

        AddControlEvent(btnStart, EventTriggerType.PointerEnter);
        AddControlEvent(btnStart, EventTriggerType.PointerExit);
        AddControlEvent(btnSetting, EventTriggerType.PointerEnter);
        AddControlEvent(btnSetting, EventTriggerType.PointerExit);
        AddControlEvent(btnQuit, EventTriggerType.PointerEnter);
        AddControlEvent(btnQuit, EventTriggerType.PointerExit);


    }

    protected override void ClickBtn(string btnName)
    {

        switch (btnName)
        {
            case "btnStart":
                MusicMgr.Instance.PlaySound("clickDown");
                alpanSpeed = 10;
                UpdateCameraXr(false);
                UIMgr.Instance.ShowPanel<LoadPanel>(E_UILayer.System, true, (pan) =>
                {
                    UIMgr.Instance.HidePanel<BeginPanel>(true, false, () =>
                    {
                        SceneMgr.Instance.LoadSceneAoAsyn("GameScene");
                    });
                });
                break;

            case "btnSetting":
                MusicMgr.Instance.PlaySound(soundList[0].name);
                alpanSpeed = 8;
                UIMgr.Instance.HidePanel<BeginPanel>(true, false, () =>
                {
                    UIMgr.Instance.ShowPanel<SettingPanel>();
                });
                break;

            case "btnQuit":
                MusicMgr.Instance.PlaySound(soundList[0].name);
                break;
        }
    }

    private void AddControlEvent(Button contorlName, EventTriggerType type)
    {
        TextMeshProUGUI textMesh;

        if (contorlName == btnStart)
            textMesh = txtStart;
        else if (contorlName == btnSetting)
            textMesh = txtSetting;
        else
            textMesh = txtQuit;


        if (type == EventTriggerType.PointerEnter)
        {
            UIMgr.AddCustomEventListener(contorlName, EventTriggerType.PointerEnter, (even) =>
            {
                MusicMgr.Instance.PlaySound(soundList[1].name);
                Color color = textMesh.color;
                color.a = 0.5f;
                textMesh.color = color;
            });

        }
        else if (type == EventTriggerType.PointerExit)
        {
            UIMgr.AddCustomEventListener(contorlName, EventTriggerType.PointerExit, (even) =>
            {
                Color color = textMesh.color;
                color.a = 1f;
                textMesh.color = color;
            });
        }

    }

    /// <summary>
    /// –ﬁ∏ƒœ‡ª˙‰÷»æ
    /// </summary>
    private void UpdateCameraXr(bool IsXrUI = true)
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Color color;
        UnityEngine.ColorUtility.TryParseHtmlString("#292929", out color);
        Camera.main.backgroundColor = color;
        Camera.main.cullingMask = IsXrUI == true ? ~(1 << LayerMask.NameToLayer("UI")) : 0;
    }

}
