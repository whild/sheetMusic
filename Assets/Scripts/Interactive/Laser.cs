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
    [SerializeField] RaycastHit hit;
    [SerializeField] RaycastHit2D hit2d;
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
        lineRenderer.SetPosition(0, pos);
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, direction, out hit, length))
        {
            Vector3 hitpoint = hit.point;
            if (TagManager.IsRightTag(targetTags, hit.collider.gameObject.tag))
            {
                hitpoint = hit.point;
            }
            lineRenderer.SetPosition(1, hitpoint);

        }
        hit2d = Physics2D.Raycast(transform.position, direction, length);
        if(hit2d)
        {
            Vector3 hitpoint = hit2d.point;
            if (TagManager.IsRightTag(targetTags, hit2d.collider.gameObject.tag))
            {
                hitpoint = hit2d.point;
                hitpoint.z = hit2d.collider.transform.position.z;
            }
            lineRenderer.SetPosition(1, hitpoint);
        }
    }


}
