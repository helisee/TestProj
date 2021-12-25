using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    //[SerializeField] private GameObject linePref;
    //LineRenderer reverseLine;

    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        //reverseLine = linePref.GetComponent<LineRenderer>();
    }

    void Update()
    {
        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider != null && hit.collider.GetComponent<Unit>())
                {
                    selectUnit = hit.collider.gameObject;
                    lineRenderer.enabled = true;
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, hit.collider.transform.position);
                }
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (lineRenderer.enabled)
            {
                ClearLog();
                Vector3 mousePos = Input.mousePosition; 
                //Debug.Log("mousePosition: " + mousePos);
                //Debug.Log("selectUnit pos z = " + selectUnit.transform.position.z);
                float cameraDistance = selectUnit.transform.position.z - Camera.main.transform.position.z;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cameraDistance));
                //Debug.Log("worldPoint: " + mousePosition);
                Vector3 point = new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
                lineRenderer.SetPosition(1, point);

                
                
            }
        }

        if (Input.GetMouseButtonUp(0) && lineRenderer.enabled)
        {
            selectUnit.GetComponent<Unit>().AddForce(-lineRenderer.GetPosition(1));
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;

            Instantiate(linePref, new Vector3(0, 0, 0), Quaternion.identity);
            reverseLine.enabled = true;
            reverseLine.positionCount = 2;
            reverseLine.SetPosition(0, selectUnit.transform.position);
            reverseLine.SetPosition(1, -lineRenderer.GetPosition(1));
        }
        */
    }

    public bool Enabled()
    {
        return lineRenderer.enabled;
    }

    public void DrawLine(Vector3 from, Vector3 to)
    {
        lineRenderer.positionCount = 2; // для двух точек прямой линии откуда - куда
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
    }

    /* использовать при активной точке старта from */
    public bool DrawLine(Vector3 to)
    {
        if (!lineRenderer.enabled) return false;
        lineRenderer.SetPosition(1, to);
        return true;
    }

    public void Disable()
    {
        lineRenderer.enabled = false;
    }

    public Vector3 GetPositionTo()
    {
        return lineRenderer.GetPosition(1);
    }

    public void ClearLog()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
