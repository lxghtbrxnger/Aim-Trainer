using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePos);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);


            //Vector3 mousePos = Input.mousePosition;
            // Do they have any bullets left?
            // burn a bullet
            // Work out where aiming
            //Ray ray = _camera.ScreenPointToRay(mousePos);
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2);

            Debug.Log(ray.origin);
            Debug.Log(ray.direction);

            Debug.Log("X: " + mousePos.x);
            Debug.Log("Y:" + mousePos.y);
            // if it is a hit, make a hit event
            // add score
            // kill target
            // If not a hit?
            // reduce score


            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                Debug.Log("Hit");
            }
        }
    }
}
