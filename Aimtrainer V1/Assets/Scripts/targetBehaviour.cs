using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetBehaviour : MonoBehaviour
{
    public float velocity;
    public Vector3 direction;

    public static float MAX_X = 10f;
    public static float MIN_X = -10f;
    public static float MAX_Y = 10f;
    public static float MIN_Y = 0.5f;
    public static float MAX_Z = 10f;
    public static float MIN_Z = -5f;

    public static float MIN_V = 5;
    public static float MAX_V = 10f;

    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        direction = new Vector3(x, y, z);
        direction.Normalize();
        velocity = Random.Range(MIN_V, MAX_V);
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0f, 1f) > 0.999f)
        {
            velocity = Random.Range(MIN_V, MAX_V);
        }

        Vector3 currentPosition = gameObject.transform.position;

        if(currentPosition.x < MIN_X || currentPosition.x > MAX_X) 
        {
            direction = new Vector3(-direction.x, direction.y, direction.z);
        }

        if (currentPosition.z < MIN_Z || currentPosition.z > MAX_Z)
        {
            direction = new Vector3(direction.x, direction.y, -direction.z);
        }

        if (currentPosition.y < MIN_Y || currentPosition.y > MAX_Y)
        {
            direction = new Vector3(direction.x, -direction.y, direction.z);
        }


        currentPosition = new Vector3(Mathf.Clamp(currentPosition.x, MIN_X, MAX_X), Mathf.Clamp(currentPosition.y, MIN_Y, MAX_Y), Mathf.Clamp(currentPosition.z, MIN_Z, MAX_Z));
        Vector3 newPosition = currentPosition + (direction * velocity * Time.deltaTime);
       

        gameObject.transform.SetPositionAndRotation(newPosition, Quaternion.identity);
    }
}
