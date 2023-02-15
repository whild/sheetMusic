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
    Vector3 dirContainer;
    [SerializeField] RaycastHit[] item;
    [SerializeField] RaycastHit2D item2d;
    [SerializeField] float length;

    private List<Vector3> Positions = new List<Vector3>();
    private List<Mirror> mirrors = new List<Mirror>();
    private int countContainer;

    private void Start()
    {
        if (!TryGetComponent(out lineRenderer))
        {
            lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        Vector3 pos = this.transform.position;
        pos = pos - direction;
        SetStartPoint();
        lineRenderer.SetPosition(1, pos);
    }

    private void SetStartPoint()
    {
        item = Physics.RaycastAll(transform.position, direction, length);
        foreach (var item in item)
        {
            if (item.collider.CompareTag(TagManager.wall))
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
        countContainer = Positions.Count;

        float distance = float.MaxValue;
        item = CheckMirror(lineRenderer.GetPosition(0), direction * -1);
        foreach (var item in item)
        {
            if (item.collider.CompareTag(TagManager.wall) || item.collider.CompareTag(TagManager.ground) || item.collider.CompareTag(TagManager.laserEvent))
            {
                Vector3 itempoint = item.point;
                float dis = Vector3.Distance(itempoint, Positions[Positions.Count - 2]);
                if (dis < distance)
                {
                    distance = dis;
                    Positions[Positions.Count - 1] = GetCoordinatePosition(itempoint);
                }
            }
        }
        if (distance == float.MaxValue)
        {
            Positions[Positions.Count - 1] = this.transform.position;
        }
        RefreshLineRenderer();
        Check2DPlayerHit();
    }

    private RaycastHit[] CheckMirror(Vector3 orisinal, Vector3 direction)
    {
        item = Physics.RaycastAll(orisinal, direction, length);
        var mirror = Array.Find(item, x => x.collider.CompareTag(TagManager.mirror));

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
        }

        Check3DPlayerHit(orisinal);
        if (Positions.Count > countContainer)
        {
            countContainer = Positions.Count;
            return CheckMirror(Positions[Positions.Count - 2], dirContainer);
        }
        return item;
    }

    private void PlayerHitEvent(RaycastHit hit)
    {
        if (hit.collider.CompareTag(TagManager.player) && lineRenderer.enabled)
        {
            Debug.Log("3D PlayerHit Effect");
        }
    }

    private void Check3DPlayerHit(Vector3 orisinal)
    {
        int a = Positions.FindIndex(0, Positions.Count - 1, x => x == orisinal);
        Vector3 dir_ = Positions[a + 1] - Positions[a];

        float length = Vector3.Distance(Positions[a], Positions[a + 1]);

        Debug.Log(dir_.normalized);
        var hits = Physics.RaycastAll(orisinal, dir_.normalized, length);

        foreach (var item in hits)
        {
            PlayerHitEvent(item);
            LaserEventCheck(item);
        }
    }

    private void Check2DPlayerHit()
    {
        item2d = Physics2D.Raycast(transform.position, direction, length);
        if (item2d.transform != null && item2d.collider.CompareTag(TagManager.player))
        {
            lineRenderer.enabled = false;
            return;
        }
        lineRenderer.enabled = true;
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

    private void LaserEventCheck(RaycastHit hit)
    {
        if (hit.collider.CompareTag(TagManager.laserEvent))
        {
            hit.transform.GetComponent<LaserEvent>().Event();
        }
    }

}
