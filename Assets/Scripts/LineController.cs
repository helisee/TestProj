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
        GameObject force = Instantiate((GameObject) Resources.Load("Prefabs/Line"), new Vector3(0f, 0f, 0f) , Quaternion.identity );
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
                    strechLine.DrawLine(hit.collider.transform.position, hit.collider.transform.position);
                }
            }
            else
            {
                strechLine.Disable();
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (strechLine.Enabled())
            {
                Vector3 mousePos = Input.mousePosition;
                float cameraDistance = selectUnit.transform.position.z - Camera.main.transform.position.z;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cameraDistance));
                /*
                ClearLog();
                Debug.Log("mousePosition: " + mousePos);
                Debug.Log("selectUnit pos z = " + selectUnit.transform.position.z);
                Debug.Log("worldPoint: " + mousePosition);
                */
                Vector3 point = new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
                strechLine.DrawLine(point);
                
            }
        }

        if (Input.GetMouseButtonUp(0) && strechLine.Enabled())
        {
            selectUnit.GetComponent<Unit>().AddForce(-strechLine.GetPositionTo());
            strechLine.Disable();
        }
    }


    public void ClearLog()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }


    /*
     * x Сделать отключение пр рисовки линии после рисовки
     
     
     */
}
