using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndingCutscene : MonoBehaviour
{
    [SerializeField] Image bg;
    [SerializeField] Image cutscene;
    [SerializeField] Text anyKeyText;

    private void Start()
    {
        anyKeyText.gameObject.SetActive(false);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(FadeImage(bg));
        sequence.Append(FadeImage(cutscene)
            .OnComplete(() =>
            {
                StartCoroutine(WaitKeyInput());
            }));

    }

    private Tween FadeImage(Image target)
    {
        target.color = new Color(target.color.r, target.color.g, target.color.b, 0);
        return target.DOFade(1, 2)
                .SetEase(Ease.Linear);
    }

    IEnumerator WaitKeyInput()
    {
        yield return new WaitForSeconds(3);

        anyKeyText.gameObject.SetActive(true);
        yield return new WaitUntil(() =>
        {
            if (Input.anyKey)
            {
                return true;
            }
            return false;
        });

        SceneManager.LoadSceneAsync(0);
    }
}
