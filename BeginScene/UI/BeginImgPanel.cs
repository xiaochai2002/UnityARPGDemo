using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BeginImgPanel : BasePanel
{
    private bool isShow;
    protected override void Awake()
    {
        base.Awake();

        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;
        Camera.main.cullingMask = 0;
    }


    protected override void Update()
    {
        base.Update();

        if (!isShow && canvasGroup.alpha >= 1)
        {
            isShow = true;

            Invoke("HideInvke", 1);
        }
    }

    private void HideInvke()
    {
        UIMgr.Instance.HidePanel<BeginImgPanel>(true, false, () =>
        {
            Cursor.visible = false;
            Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("UI"));
            UIMgr.Instance.ShowPanel<BeginPanel>(E_UILayer.System);
        });
    }
}
