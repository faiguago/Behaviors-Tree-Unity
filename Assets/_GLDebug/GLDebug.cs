using System.Collections.Generic;
using UnityEngine;

public class GLDebug : MonoBehaviour
{
    private struct Line
    {
        public Vector3 start;
        public Vector3 end;
        public Color color;

        public Line(Vector3 start, Vector3 end, Color color)
        {
            this.start = start;
            this.end = end;
            this.color = color;
        }

        public void DrawLine()
        {
            GL.Color(color);
            GL.Vertex(start);
            GL.Vertex(end);
        }
    }

    public Material matZOn;
    public Material matZOff;

    public KeyCode toggleKey;
    public bool displayLines = true;

    private List<Line> linesZOn;
    private List<Line> linesZOff;

    private static GLDebug instance;

    private bool applicationActived;

#if UNITY_EDITOR
    public bool displayGizmos = true;
#endif

    void Awake()
    {
        Init();
    }

    void Init()
    {
        instance = this;

        linesZOn = new List<Line>();
        linesZOff = new List<Line>();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            displayLines = !displayLines;

        if (!displayLines)
        {
            if (linesZOff.Count > 0 || linesZOn.Count > 0)
            {
                linesZOff.Clear();
                linesZOn.Clear();
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        applicationActived = focus;
    }

    void OnPostRender()
    {
        if (!displayLines) return;

        matZOn.SetPass(0);
        GL.Begin(GL.LINES);
        linesZOn.ForEach(l => l.DrawLine());
        linesZOn.Clear();
        GL.End();

        matZOff.SetPass(0);
        GL.Begin(GL.LINES);
        linesZOff.ForEach(l => l.DrawLine());
        linesZOff.Clear();
        GL.End();
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!displayGizmos || !Application.isPlaying)
            return;
        for (int i = 0; i < linesZOn.Count; i++)
        {
            Gizmos.color = linesZOn[i].color;
            Gizmos.DrawLine(linesZOn[i].start, linesZOn[i].end);
        }
        for (int i = 0; i < linesZOff.Count; i++)
        {
            Gizmos.color = linesZOff[i].color;
            Gizmos.DrawLine(linesZOff[i].start, linesZOff[i].end);
        }

        if (!applicationActived)
        {
            linesZOn.Clear();
            linesZOff.Clear();
        }
    }
#endif

    private static void DrawLine(Vector3 start,
        Vector3 end, Color color, bool depthTest)
    {
        if (!instance.displayLines)
            return;

        if (start == end)
            return;

        if (depthTest)
            instance.linesZOn.Add(new Line(start, end, color));
        else
            instance.linesZOff.Add(new Line(start, end, color));
    }

    public static void DrawLine(Vector3 start, Vector3 end,
        Color? color = null, bool depthTest = true)
    {
        DrawLine(start, end, color ?? Color.white, depthTest);
    }

    public static void DrawRay(Vector3 start, Vector3 dir,
        Color? color = null, bool depthTest = true)
    {
        if (dir == Vector3.zero)
            return;

        DrawLine(start, start + dir, color, depthTest);
    }
}