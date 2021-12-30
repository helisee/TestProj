using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private GameObject selectUnit;
    [SerializeField] private Line strechLine;
    [SerializeField] private Line forceLine;

    void Start()
    {
        GameObject strech = Instantiate((GameObject) Resources.Load("Prefabs/Line"),
                                        new Vector3(0f, 0f, 0f) , 
                                        Quaternion.identity );
        GameObject force = Instantiate((GameObject) Resources.Load("Prefabs/Line"), 
                                        new Vector3(0f, 0f, 0f) , 
                                        Quaternion.identity );
        strech.name = "Strech";
        force.name = "Force";

        strechLine = strech.GetComponent<Line>();
        forceLine = force.GetComponent<Line>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider != null && hit.collider.GetComponent<Unit>())
                {
                    selectUnit = hit.collider.gameObject;
                    Vector3 unitPos = new Vector3(
                        selectUnit.transform.position.x,
                        selectUnit.transform.position.y,
                        selectUnit.transform.position.z
                        );
                    strechLine.DrawLine(unitPos, unitPos);
                    strechLine.SetPosition(Vector3.zero);
                    forceLine.DrawLine(unitPos, unitPos);
                    forceLine.SetPosition(Vector3.zero);

                    Debug.Log("hit pos: " + hit.collider.transform.position );
                }
            }
            else
            {
                strechLine.Disable();
                forceLine.Disable();
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (strechLine.Enabled())
            {
                Vector3 mousePos = Input.mousePosition;
                float cameraDistance = selectUnit.transform.position.z - Camera.main.transform.position.z; 
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(
                    mousePos.x,
                    mousePos.y,
                    cameraDistance
                    ));
                
                /*
                ClearLog();
                Debug.Log("mousePosition: " + mousePos);
                Debug.Log("selectUnit pos z = " + selectUnit.transform.position.z);
                Debug.Log("worldPoint: " + mousePosition);
                */
                Vector3 point = new Vector3(
                    mousePosition.x, 
                    mousePosition.y, 
                    point.z = selectUnit.transform.position.z
                    );
                
                Vector3 unitPos = new Vector3(
                    selectUnit.transform.position.x, 
                    selectUnit.transform.position.y, 
                    selectUnit.transform.position.z
                    );
                strechLine.DrawLine(unitPos, point); 
                //Quaternion rot = Quaternion.AngleAxis(180f, selectUnit.transform.position);
                //point = transform.InverseTransformDirection(Vector3.forward);
                forceLine.DrawLine(unitPos, -point + 2 * unitPos); 
            }
        }

        if (Input.GetMouseButtonUp(0) && strechLine.Enabled())
        {
            Debug.Log("Force: " + -strechLine.GetPositionTo());
            selectUnit.GetComponent<Unit>().AddForce(-strechLine.GetPositionTo() );
            strechLine.Disable();
            forceLine.Disable();
            //forceLine.SetPosition(selectUnit.transform.position);
            //forceLine.DrawLine(-strechLine.GetPositionTo(), selectUnit.transform.position );
        }
    }

    public void ClearLog()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
