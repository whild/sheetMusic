using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ªºªÃªÈstartª«ªéendªÞªÇÔÑª¯«ª«Ö«¸«§«¯«È
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] float Duration;

    [SerializeField] Vector3 startpos;
    [SerializeField] Vector3 endpos;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (start == null || end == null)
        {
            Debug.LogError("There is no Pos");
            return;
        }
        startpos = start.position;
        endpos = end.position;

        this.transform.position = startpos;

        this.transform
            .DOMove(endpos, Duration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
