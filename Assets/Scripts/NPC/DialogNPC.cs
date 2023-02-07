using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DialogNPC : TriggerInteractCore
{
    private readonly string dialogPath = "NPC/NPCDialog";
    [SerializeField] NPCDialogBase npcDialogBase;
    [SerializeField] GameObject dialog;
    [SerializeField] SpriteRenderer image;

    [SerializeField] private bool isTalk; 
    private void Awake()
    {
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
    {
        if (!isTalk)
        {
            isTalk = true;
            dialog.gameObject.SetActive(true);
            StartCoroutine(Talk());
        }

    }

    IEnumerator Talk()
    {
        for (int i = 0; i < npcDialogBase.sprites.Count; i++)
        {
            int temp = i;
            image.sprite = npcDialogBase.sprites[temp];

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
