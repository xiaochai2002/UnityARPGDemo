using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        UIMgr.Instance.ShowPanel<BeginImgPanel>(E_UILayer.System);
    }

}
