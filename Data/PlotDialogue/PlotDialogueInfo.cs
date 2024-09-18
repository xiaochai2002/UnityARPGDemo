using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotDialogueInfo
{
    /// <summary>
    /// 1-是对话内容 2-是对话选项 3-结束对话
    /// </summary>
    public int idei;

    /// <summary>
    /// 对话id
    /// </summary>
    public int id;

    /// <summary>
    /// 对话角色id
    /// </summary>
    public string npcid;

    /// <summary>
    /// 对话内容
    /// </summary>
    public string content;

    /// <summary>
    /// 跳转id
    /// </summary>
    public int jump;

    /// <summary>
    /// 效果
    /// </summary>
    public string effect;
}
