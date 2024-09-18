using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr : BaseManager<GameDataMgr>
{
    public List<SceneMusicInfo> sceneMusicList;
    public List<SceneSoundInfo> sceneSoundList;
    public List<VolumeInfo> volumeList;
    public List<PlotDialogueInfo> plotDialogueList;
    public PlayerInfo playerInfo;
    public List<TaskInfo> taskInfosList;
    private GameDataMgr()
    {
        sceneMusicList = JsonMgr.Instance.LoadData<List<SceneMusicInfo>>("SceneMusicInfo");
        sceneSoundList = JsonMgr.Instance.LoadData<List<SceneSoundInfo>>("SceneSoundInfo");
        volumeList = JsonMgr.Instance.LoadData<List<VolumeInfo>>("VolumeInfo");
        plotDialogueList = JsonMgr.Instance.LoadData<List<PlotDialogueInfo>>("PIotDialogueInfo");
        playerInfo = JsonMgr.Instance.LoadData<PlayerInfo>("PlayerInfo");
        taskInfosList = JsonMgr.Instance.LoadData<List<TaskInfo>>("TaskInfo");
    }

    public void SaveVolumeData()
    {
        JsonMgr.Instance.SaveData(volumeList, "VolumeInfo");
    }

    public void SaveTaskInfoData()
    {
        JsonMgr.Instance.SaveData(taskInfosList, "TaskInfo");
    }
}
