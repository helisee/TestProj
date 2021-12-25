using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 ForceVector;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();    
    }
    
    void Update()
    {
        
    }

    public void AddForce(Vector3 forceVector)
    {
        rigidbody.AddForce(forceVector * 200f, ForceMode.Force);    
    }


}
