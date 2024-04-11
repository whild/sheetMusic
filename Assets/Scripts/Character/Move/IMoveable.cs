using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    /// <summary>
    /// 移動方向を"direction"に変更します。
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(Vector3 direction);
    /// <summary>
    /// 現在の方向を返還
    /// </summary>
    /// <returns></returns>
    public Vector3 GetDirection();

    public void Jump();

    public void Dash(float val);
    /// <summary>
    /// 現楽器の能力を使う
    /// もし、マイクで能力を使うともっと強い能力を使う
    /// </summary>
    /// <param name="isMike"></param>
    public void PlayerAct(bool isMike);
}
