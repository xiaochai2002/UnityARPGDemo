using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BeginImgPanel : BasePanel
{
    private CanvasGroup canvas;

    private bool isShow = true;

    private float alpanSpeed = 1;

    private UnityAction hideaction;

    protected override void Awake()
    {
        base.Awake();

        canvas = GetComponent<CanvasGroup>();
        if (canvas == null)
            this.AddComponent<CanvasGroup>();

        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = Color.black;
        Camera.main.cullingMask = 0;
    }

    public override void HideMe()
    {
        isShow = false;
        canvas.alpha = 1f;
    }

    public override void ShowMe()
    {
        isShow = true;
        canvas.alpha = 0f;
    }

    void Update()
    {
        if (isShow && canvas.alpha != 1)
        {
            canvas.alpha += alpanSpeed * Time.deltaTime;
            if (canvas.alpha >= 1)
            {
                canvas.alpha = 1;
                Invoke("HideInvke",1);
            }
        }
        else if (!isShow)
        {
            canvas.alpha -= alpanSpeed * Time.deltaTime;
            if (canvas.alpha <= 0)
            {
                canvas.alpha = 0;
                UIMgr.Instance.HidePanel<BeginImgPanel>();
                UIMgr.Instance.ShowPanel<BeginPanel>(E_UILayer.System);
            }
        }
    }

    private void HideInvke()
    {
        HideMe();
    }
}
