using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    private CanvasGroup canvas;
    private bool isShow;
    private float alpanSpeed = 2;
    protected override void Awake()
    {
        base.Awake();
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Color color;
        UnityEngine.ColorUtility.TryParseHtmlString("#292929", out color);
        Camera.main.backgroundColor = color;
        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("UI"));

        canvas = this.GetComponent<CanvasGroup>();
        if (canvas == null)
            this.AddComponent<CanvasGroup>();
    }

    public override void ShowMe()
    {
        canvas.alpha = 0f;
        isShow = true;
    }

    public override void HideMe()
    {
    }

    private void Update()
    {
        if (isShow && canvas.alpha != 1)
        {
            canvas.alpha += alpanSpeed * Time.deltaTime;
            if (canvas.alpha >= 1)
            {
                canvas.alpha = 1;
                isShow = false;
            }
        }
    }
}
