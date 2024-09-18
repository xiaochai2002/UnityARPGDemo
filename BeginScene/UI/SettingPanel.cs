using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    private readonly string buttonName = "btnBack";
    private readonly string musictoggleName = "musicToggle";
    private readonly string soundtoggleName = "soundToggle";
    private readonly string musicSldName = "musicSld";
    private readonly string soundSldName = "soundSld";

    private void Start()
    {
        List<VolumeInfo> volumeList = GameDataMgr.Instance.volumeList;
        GetControl<Toggle>(musictoggleName).isOn = volumeList.Count > 0 ? volumeList[0].IsOpen : false;
        GetControl<Toggle>(soundtoggleName).isOn = volumeList.Count > 0 ? volumeList[1].IsOpen : false;
        GetControl<Slider>(musicSldName).value = volumeList.Count > 0 ? volumeList[0].VolumeValue : 0.1f;
        GetControl<Slider>(soundSldName).value = volumeList.Count > 0 ? volumeList[1].VolumeValue : 0.1f;
        UIMgr.AddCustomEventListener(GetControl<Button>(buttonName), EventTriggerType.PointerEnter, (obj) =>
        {
            MusicMgr.Instance.PlaySound(GameDataMgr.Instance.sceneSoundList[1].name);
        });
    }

    protected override void ToggleValueChange(string toggleName, bool value)
    {
        if (toggleName == musictoggleName)
        {
            GameDataMgr.Instance.volumeList[0].IsOpen = value;
        }
        else if (toggleName == soundtoggleName)
        {
            GameDataMgr.Instance.volumeList[1].IsOpen = value;
        }
    }

    protected override void SliderValueChange(string sliderName, float value)
    {
        if (sliderName == musicSldName)
        {
            GameDataMgr.Instance.volumeList[0].VolumeValue = value;
        }
        else if (sliderName == soundSldName)
        {
            GameDataMgr.Instance.volumeList[1].VolumeValue = value;
        }
    }

    protected override void ClickBtn(string btnName)
    {
        MusicMgr.Instance.PlaySound(GameDataMgr.Instance.sceneSoundList[0].name);
        GameDataMgr.Instance.SaveVolumeData();
        alpanSpeed = 5;
        UIMgr.Instance.HidePanel<SettingPanel>(true, false, () =>
        {
            UIMgr.Instance.ShowPanel<BeginPanel>();
        });
    }
}
