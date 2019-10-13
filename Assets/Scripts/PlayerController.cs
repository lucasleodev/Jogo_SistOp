using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetKeyDirection()
    {
        var i = 2;
        if (Input.GetKeyDown(KeyCode.A))
        {
            i = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            i = 1;
        }
        return i;
    }
}
