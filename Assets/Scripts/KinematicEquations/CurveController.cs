using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethod;

[System.Serializable]
public class Path
{
    [SerializeField, HideInInspector]
    List<Vector3> points;

    public Path(Vector3 center)
    {
        points = new List<Vector3>
        {
            center + Vector3.left,
            center + (Vector3.left + Vector3.up) * .5f,
            center + (Vector3.right + Vector3.down) * .5f,
            center + Vector3.right
        };
    }

    public void AddSegment(Vector3 anchorPos)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add(points[points.Count - 1] + anchorPos * .5f);
        points.Add(anchorPos);
    }

    public Vector3[] GetPointsInSegment(int i)
    {
        return new Vector3[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[i * 3 + 3] };
    }
}

public class CurveController : MonoBehaviour
{
    [SerializeField] private Transform[] pos;
    [SerializeField] private int linePositionCount;
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.positionCount = linePositionCount;

        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, ExtensionMethods.QuadraticCurve(pos[0].position, pos[1].position, pos[2].position, ((float)1 / line.positionCount) * i));
        }
    }
}
