using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField]
    private int segments;

    [SerializeField]
    LineRenderer linePrefab;
       
    void Start ()
    {
        
       
        
        
    }
   
   
    public Vector3 CreatePoints (float xradius, float yradius)
    {
        float x=0f;
        float y= 0f;
        float z=0f ;
       
        float angle = 20f;
        Vector3 randomPlanetPos = Vector3.zero;
        int randomint = Random.Range(1,segments);
       
        for (int i = 0; i < (segments + 1); i++)
        {
                           
            
            LineRenderer line = Instantiate(linePrefab, transform);
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius; 
            angle += (360f / segments);
            line.SetPosition(0, new Vector3(x,y,z));
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos (Mathf.Deg2Rad * angle) * yradius; 
            angle += (360f / segments);
            line.SetPosition(1, new Vector3(x,y,z));
            angle -= (360f / segments);
            if(randomint == i)
            {
                randomPlanetPos = new Vector3(x,y,z);
            }
            
        }
        return randomPlanetPos;
    }
}
