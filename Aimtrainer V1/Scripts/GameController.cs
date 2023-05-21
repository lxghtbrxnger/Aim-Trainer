using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject myTarget;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(myTarget, new Vector3(0, 1, 50), Quaternion.identity);
        Instantiate(myTarget, new Vector3(7, 1, 15), Quaternion.identity);
        Instantiate(myTarget, new Vector3(4, 1, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
