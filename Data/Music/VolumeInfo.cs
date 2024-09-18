using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeInfo
{
    /// <summary>
    /// id 1:音乐 2:音效
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// 音量大小
    /// </summary>
    public float VolumeValue { get; set; }
    /// <summary>
    /// 是的开启
    /// </summary>
    public bool IsOpen { get; set; }
}
