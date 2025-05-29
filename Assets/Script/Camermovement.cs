using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camermovement : MonoBehaviour
{
    public float camerasped;
    

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(camerasped * Time.deltaTime, 0, 0);  
    }
}
