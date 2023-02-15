using System;
using System.Linq;
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
    [SerializeField] Vector3 dirContainer;
    [SerializeField] RaycastHit[] item;
    [SerializeField] RaycastHit2D item2d;
    [SerializeField] float length;

    private List<Vector3> Positions = new List<Vector3>();
    private List<Mirror> mirrors = new List<Mirror>();
    private void Start()
    {
        if(!TryGetComponent(out lineRenderer))
        {
            lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        Vector3 pos = this.transform.position;
        SetStartPoint();
        lineRenderer.SetPosition(1, pos);
    }

    private void SetStartPoint()
    {
        item = Physics.RaycastAll(transform.position, direction, length);
        foreach (var item in item)
        {
            if(item.collider.gameObject.CompareTag("Wall"))
            {
                Vector3 itempoint = item.point;
                Positions.Add(itempoint);
                lineRenderer.SetPosition(0, itempoint);
            }
        }
        Positions.Add(this.transform.position);
        lineRenderer.SetPosition(1, this.transform.position);
    }

    private void Update()
    {
        dirContainer = direction * -1;
        mirrors = new List<Mirror>();
        Positions.RemoveRange(1, Positions.Count - 2);

        item = CheckMirror(lineRenderer.GetPosition(0), direction * -1);
        foreach (var item in item)
        {
            if (item.collider.CompareTag("Wall") || item.collider.CompareTag("Ground"))
            {
                Vector3 itempoint = item.point;
                Positions[Positions.Count - 1] = GetCoordinatePosition(itempoint);
                //lineRenderer.SetPosition(Positions.Count - 1,);
                break;
            }
            Positions[Positions.Count - 1] = this.transform.position;
        }
        RefreshLineRenderer();

        for (int i = 0; i < Positions.Count; i++)
        {
            Debug.Log(Positions[i]);
        }

        /*
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
        */
    }

    private RaycastHit[] CheckMirror(Vector3 orisinal, Vector3 direction)
    {
        item = Physics.RaycastAll(orisinal, direction, length);
        var mirror = Array.Find(item, x => x.collider.CompareTag("Mirror"));

        foreach (var item in item)
        {
            PlayerHitEvent(item);
        }

        if (mirror.transform != null)
        {
            var mir = mirror.collider.gameObject.GetComponent<Mirror>();
            if (!mirrors.Contains(mir))
            {
                mirrors.Add(mir);
                Vector3 dir = mir.direction;
                Positions.Insert(Positions.Count - 1, GetCoordinatePosition(mirror.transform.position));
                
                dirContainer = dir;
            }
            return CheckMirror(Positions[Positions.Count - 2], dirContainer);
        }

        return item;
    }

    private void PlayerHitEvent(RaycastHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("3D PlayerHit Effect");
        }
    }

    private void RefreshLineRenderer()
    {
        lineRenderer.positionCount = Positions.Count;
        lineRenderer.SetPositions(Positions.ToArray());
    }

    private Vector3 GetCoordinatePosition(Vector3 targetPos)
    {
        Vector3 pos = Positions[Positions.Count - 2];
        float newx = (dirContainer.x == 0) ? pos.x : targetPos.x;
        float newy = (dirContainer.y == 0) ? pos.y : targetPos.y;
        float newz = (dirContainer.z == 0) ? pos.z : targetPos.z;
        pos = new Vector3(newx, newy, newz);
        return pos;
    }

}
