using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    public void SetDirection(Vector3 direction);
    public Vector3 GetDirection();
    public void Jump();

    public void Dash(float val);

    public void PlayerAct(bool isMike);
}
