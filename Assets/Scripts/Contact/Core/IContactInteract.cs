using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 触った時の反応
/// </summary>
public interface IContactInteract
{
    /// <summary>
    /// 3Dで触ったとき
    /// </summary>
    /// <param name="collision"></param>
    public void OnContact(Collision collision);
    /// <summary>
    /// 2Dで触ったとき
    /// </summary>
    /// <param name="collision"></param>
    public void OnContact(Collision2D collision);

    /// <summary>
    /// 3Dで離れるとき
    /// </summary>
    /// <param name="collision"></param>
    public void OnUnContact(Collision collision);
    /// <summary>
    /// 2Dで離れたとき
    /// </summary>
    /// <param name="collision"></param>
    public void OnUnContact(Collision2D collision);
}
