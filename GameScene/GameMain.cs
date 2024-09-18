using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMain : SingletonMono<GameMain>
{
    public GameObject PlayerObj;

    private void Start()
    {
        //InitGameInfo();

    }

    public void InitGameInfo()
    {
        Transform transpos = GameObject.Find("GamePlayerPos").transform;
        ABResMgr.Instance.LoadResAsync<GameObject>("player/models", "player", (obj) =>
        {
            PlayerObj = GameObject.Instantiate<GameObject>(obj, transpos.position, transpos.rotation);

            if (PlayerObj != null)
            {
                Camera.main.GetComponent<CameraMove>().SetTargetPos(PlayerObj.transform);
                PlayerInputMgr.Instance.InfoInputMgr(PlayerObj.GetComponent<PlayerInput>(),PlayerObj.GetComponent<PlayerObject>());
            }
            else
                Debug.LogError("游戏角色没有加载成功！");


        });

    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
