using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换管理器 主要用于切换场景
/// </summary>
public class SceneMgr : BaseManager<SceneMgr>
{
    private SceneMgr() { }

    //同步切换场景的方法
    public void LoadScene(string name, UnityAction callBack = null)
    {
        //切换场景
        SceneManager.LoadScene(name);
        //调用回调
        callBack?.Invoke();
        callBack = null;
    }

    //异步切换场景的方法
    public void LoadSceneAsyn(string name,bool allow = true, UnityAction callBack = null)
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAsyn(name,allow, callBack));
    }

    private IEnumerator ReallyLoadSceneAsyn(string name, bool allow, UnityAction callBack)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        ao.allowSceneActivation = allow;
        //不停的在协同程序中每帧检测是否加载结束 如果加载结束就不会进这个循环每帧执行了
        while (!ao.isDone)
        {
            //可以在这里利用事件中心 每一帧将进度发送给想要得到的地方
            EventCenter.Instance.EventTrigger<float>(E_EventType.E_SceneLoadChange, ao.progress);
            yield return 0;
        }
        //避免最后一帧直接结束了 没有同步1出去
        EventCenter.Instance.EventTrigger<float>(E_EventType.E_SceneLoadChange, 1);

        callBack?.Invoke();
        callBack = null;
    }

    //异步切换场景的方法
    public void LoadSceneAoAsyn(string name, UnityAction callBack = null)
    {
        MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAoAsyn(name, callBack));
    }

    private IEnumerator ReallyLoadSceneAoAsyn(string name, UnityAction callBack)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        ao.allowSceneActivation = false;
        //不停的在协同程序中每帧检测是否加载结束 如果加载结束就不会进这个循环每帧执行了
        while (!ao.isDone)
        {
            EventCenter.Instance.EventTrigger<AsyncOperation>(E_EventType.E_AsynSceneAo, ao);
            yield return 0;
        }

        callBack?.Invoke();
        callBack = null;
    }
}
