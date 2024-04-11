using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 話してるNPCオブジェクト
/// </summary>
public class DialogNPC : TriggerInteractCore
{
    private readonly string dialogPath = "NPC/NPCDialog";
    [SerializeField] NPCDialogBase npcDialogBase;
    [SerializeField] GameObject dialog;
    [SerializeField] SpriteRenderer image;
    //話したのかを判断
    [SerializeField] private bool isTalk;

    [SerializeField] AudioSource talkAudio;
    [SerializeField] List<AudioClip> talkClips = new List<AudioClip>();
    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        if(dialog == null)
        {
            dialog = Instantiate(ResourceData<GameObject>.GetData(dialogPath), this.transform);
        }
        dialog.transform.localPosition = new Vector3(0, 2, 0);

        if(image == null)
        {
            image = dialog.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        dialog.gameObject.SetActive(false);
        isTalk = false;
    }

    public override void OnTrigger(Collider collision)
    {//触ったから
        if (!isTalk)//話したのかを判断して
        {//話を進行します。
            isTalk = true;
            dialog.gameObject.SetActive(true);
            StartCoroutine(Talk());
        }

    }

    IEnumerator Talk()
    {//話す過程のCorotine
        for (int i = 0; i < npcDialogBase.sprites.Count; i++)
        {
            int temp = i;
            image.sprite = npcDialogBase.sprites[temp];

            talkAudio.clip = (talkAudio.clip == talkClips[0] || talkAudio.clip == null) ? talkClips[1] : talkClips[0];
            talkAudio.Play();

            dialog.transform.localScale = Vector3.zero;
            dialog.transform
                .DOScale(1, 0.5f)
                .SetEase(Ease.OutElastic)
                .OnUpdate(() =>
                {
                    dialog.transform.LookAt(Camera.main.transform);
                });

            yield return new WaitForSeconds(5);
        }
        dialog.gameObject.SetActive(false);
        yield return null;
    }
}
