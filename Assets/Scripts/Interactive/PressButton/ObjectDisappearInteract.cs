using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisappearInteract : ObjectInteractCore
{
    public override void Interact(float val)
    {
        this.gameObject.SetActive((val == 1) ? false : true);
    }
}
