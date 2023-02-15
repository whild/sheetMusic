using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneChange : MonoBehaviour
{
    [SerializeField] RectTransform BG;
    private static RectTransform bg;

    private static Vector2 zero = Vector2.zero;
    private static Vector2 max = new Vector2(2200, 2200);

    private void Start()
    {
        bg = BG;
    }
    public static void SceneChageEvent(TweenCallback callback)
    {
        bg.gameObject.SetActive(true);
        bg.sizeDelta = max;
        PlayerInputController.Instance.gameObject.SetActive(false);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(bg.DOSizeDelta(zero, 1)
            .OnComplete(() =>
            {
                callback.Invoke();
            })
        );
        sequence.Append(bg.DOSizeDelta(zero, 0.5f));
        sequence.Append(bg.DOSizeDelta(max, 1)
            .OnComplete(() =>
            {
                PlayerInputController.Instance.gameObject.SetActive(true);
                bg.gameObject.SetActive(false);
            })
        );
    }
}
