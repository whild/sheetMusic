using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [TagSelector]
    [SerializeField] protected string[] targetTags = new string[] { };
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float laserWidth;
    [SerializeField] Vector3 direction;
    [SerializeField] RaycastHit[] item;
    [SerializeField] RaycastHit2D item2d;
    [SerializeField] float length;
    private void Start()
    {
        if(!TryGetComponent(out lineRenderer))
        {
            lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        Vector3 pos = this.transform.position;
        pos = pos - direction;
        lineRenderer.SetPosition(1, pos);
    }

    private void Update()
    {
        item = Physics.RaycastAll(transform.position, direction, length);
        foreach (var item in item)
        {
            if (TagManager.IsRightTag(targetTags, item.collider.gameObject.tag))
            {
                if (item.collider.gameObject.CompareTag("Wall"))
                {
                    Vector3 itempoint = item.point;
                    lineRenderer.SetPosition(0, itempoint);
                }
                else if (item.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("3D PlayerHit Effect");
                }
            }
        }
        item2d = Physics2D.Raycast(transform.position, direction, length);
        if(item2d)
        {
            Vector3 itempoint = item2d.point;
            if (TagManager.IsRightTag(targetTags, item2d.collider.gameObject.tag))
            {
                itempoint = item2d.point;
                itempoint.z = item2d.collider.transform.position.z;
            }
            lineRenderer.SetPosition(0, itempoint);
        }
    }


}
