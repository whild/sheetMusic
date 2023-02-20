using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StageManager : Manager<StageManager>
{
    [SerializeField] Transform _3Dparent;
    [SerializeField] Transform _2Dparent;
    [SerializeField] GameObject Current3D;
    [SerializeField] GameObject Current2D;

    [SerializeField] public bool isGetKey;

    [SerializeField] private ReactiveProperty<StageDataBase> stageData = new ReactiveProperty<StageDataBase>();

    [SerializeField] AudioSource stageEndAudio;
    [SerializeField] AudioClip clearClip;
    [SerializeField] AudioClip overClip;

    protected override void Awake()
    {
        base.Awake();

        stageData
            .Where(data => data != null)
            .Subscribe(data =>
            {
                ParseStage(data);
            });
    }

    private void Start()
    {
        ChangeStagePerData(FindStage());
    }

    private StageDataBase FindStage()
    {
        var allStages = ResourceData<StageDataBase>.GetDatas("World/StageData");
        return Array.Find(allStages, x => x.stageIndex == GameManager.Instance.data.currentStage);
    }

    private void ParseStage(StageDataBase data)
    {
        Debug.Log("??");
        GameObject.Destroy(Current3D);
        GameObject.Destroy(Current2D);
        Current3D = GameObject.Instantiate(data.stage_3D, _3Dparent);
        Current2D = GameObject.Instantiate(data.stage_2D, _2Dparent);

        isGetKey = false;

        GameManager.Instance.player3D.localPosition = data.spawnPos_3D;
        GameManager.Instance.player2D.localPosition = new Vector2(data.spawnPos_2D.x, data.spawnPos_2D.y);
    
        CinemachineController.Instance.CameraZoom(60, null);
    }

    public void NextStage(StageDataBase stageData)
    {
        stageEndAudio.clip = clearClip;
        stageEndAudio.Play();

        SceneChange.SceneChageEvent(() =>
        {
            ChangeStagePerData(stageData);
        });
        GameManager.Instance.data.currentStage = stageData.stageIndex;
        GameManager.Instance.SaveGameData();
    }

    public void ChangeStagePerData(StageDataBase stageData)
    {
        ChangeStage.ReData(stageData.goal3d, stageData.goal2d, stageData.needKey);
        this.stageData.Value = null;
        this.stageData.Value = stageData;
    }

    public void Retry()
    {
        NextStage(this.stageData.Value);
        stageEndAudio.clip = overClip;
        stageEndAudio.Play();
    }
}