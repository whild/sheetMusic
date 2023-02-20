using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Dimension dimension;
    private Collider collider_;

    private void Start()
    {
        collider_ = GameManager.CheckNull<Collider>(this.transform);
        this.gameObject.tag = TagManager.ladder;
    }
}
