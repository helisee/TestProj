using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 ForceVector;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();    
    }
    
    void Update()
    {
        
    }

    public void AddForce(Vector3 forceVector)
    {
        _rigidbody.isKinematic = false;  
        _rigidbody.AddForce(forceVector * 200f, ForceMode.Force);
        StartCoroutine(Flight(0.5f));
        
    }

    IEnumerator Flight(float sec = 2f)
    {
        yield return new WaitForSeconds(sec);
        _rigidbody.isKinematic = true;
    }
}
