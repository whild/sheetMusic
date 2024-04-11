using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ステージ全般を担当します。
/// </summary>
public class StageManager : Manager<StageManager>
{
    [SerializeField] Transform _3Dparent;
    [SerializeField] Transform _2Dparent;
    [SerializeField] GameObject Current3D;
    [SerializeField] GameObject Current2D;

    [SerializeField] public bool isGetKey;

    [SerializeField] private ReactiveProperty<StageDataBase> stageData = new ReactiveProperty<StageDataBase>();

    [SerializeField] AudioSource stageBGMAudio;
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
                StartCoroutine(ParseStage(data));
            });
    }

    private void Start()
    {
        StartCoroutine(ChangeStagePerData(FindStage()));
    }

    private StageDataBase FindStage()
    {
        var allStages = ResourceData<StageDataBase>.GetDatas("World/StageData");
        return Array.Find(allStages, x => x.stageIndex == GameManager.Instance.data.currentStage);
    }

    /// <summary>
    /// ステージの設定通りステージを設定します。
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private IEnumerator ParseStage(StageDataBase data)
    {
        //サウンド設定＆再生
        stageBGMAudio.clip = data.stageBGM;
        stageBGMAudio.Play();
        //元のステージを消します。
        GameObject.Destroy(Current3D);
        GameObject.Destroy(Current2D);

        yield return new WaitForEndOfFrame();

        //ステージを召喚
        Current3D = GameObject.Instantiate(data.stage_3D, _3Dparent);
        Current2D = GameObject.Instantiate(data.stage_2D, _2Dparent);

        isGetKey = false;

        //キャラクターの位置設定
        GameManager.Instance.player3D.localPosition = data.spawnPos_3D;
        GameManager.Instance.player2D.localPosition = new Vector2(data.spawnPos_2D.x, data.spawnPos_2D.y);
    
        CinemachineController.Instance.CameraZoom(60, null);

        PlayerInputController.Instance.currentInstrument.Value = Instrument.Automaton;
    }

    /// <summary>
    /// 次のステージに移動します。
    /// </summary>
    public void NextStage(StageDataBase stageData)
    {
        stageEndAudio.clip = clearClip;
        stageEndAudio.Play();

        SceneChange.SceneChageEvent(() =>
        {
            StartCoroutine(ChangeStagePerData(stageData));
        });
        GameManager.Instance.data.currentStage = stageData.stageIndex;
        GameManager.Instance.SaveGameData();
    }

    public IEnumerator ChangeStagePerData(StageDataBase stageData)
    {
        ChangeStage.ReData(stageData.goal3d, stageData.goal2d, stageData.needKey);
        yield return new WaitForEndOfFrame();
        this.stageData.Value = null;
        yield return new WaitForEndOfFrame();
        this.stageData.Value = stageData;
    }

    /// <summary>
    /// ステージを最初からまた始めます。
    /// </summary>
    public void Retry()
    {
        NextStage(this.stageData.Value);
        stageEndAudio.clip = overClip;
        stageEndAudio.Play();
    }
}