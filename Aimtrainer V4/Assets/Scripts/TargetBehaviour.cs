using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This script controls the behaviour of my targets.  If I wanted to expand the aim trainer
 * and add more types of target I would need to create another target behaviour and attach
 * it to a new target object.  This behaviour bounces off walls and sometimes changes its 
 * velocity.
 */
public class TargetBehaviour : MonoBehaviour
{
    // Public variables.  In Unity, the editor allows you to inspect public variables while
    // playing which makes debugging much easier.  Unity also remembers the value you set as
    // a default, which means you can set up a lot of your game's constants in the Unity UI
    public float Velocity;
    public Vector3 Direction;

    // I set these as constants rather than setting these through the Unity UI.  They control
    // where the walls are, so the balls can bounce off them.
    public const float MaxX = 10f;
    public const float MinX = -10f;
    public const float MaxY = 10f;
    public const float MinY = 0.5f;
    public const float MaxZ = 10f;
    public const float MinZ = -5f;

    // The minimum and maximum velocity for the targets
    public const float MinV = 5;
    public const float MaxV = 10f;

    // Sets up the velocity vector for the target. 
    void Start()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        Direction = new Vector3(x, y, z);
        Direction.Normalize();
        Velocity = Random.Range(MinV, MaxV);
    }

    /**
     * Makes sure the target stays within the bounds of the game, bouncing if it touches 
     * one of the boundaries.  There is also a small chance that the target will change its
     * speed to make it more difficult to hit.
     */
    void Update()
    {
        // Resets the speedd of the target at a 0.01% chance per frame
        if(Random.Range(0f, 1f) > 0.999f)
        {
            Velocity = Random.Range(MinV, MaxV);
        }

        Vector3 currentPosition = gameObject.transform.position;

        // Checks if the target is outside the bounds, and if so reverses that component of the
        // targets velocity vector, which makes it bounce off the wall.
        if(currentPosition.x < MinX || currentPosition.x > MaxX) 
        {
            Direction = new Vector3(-Direction.x, Direction.y, Direction.z);
        }

        if (currentPosition.z < MinZ || currentPosition.z > MaxZ)
        {
            Direction = new Vector3(Direction.x, Direction.y, -Direction.z);
        }

        if (currentPosition.y < MinY || currentPosition.y > MaxY)
        {
            Direction = new Vector3(Direction.x, -Direction.y, Direction.z);
        }

        // Make sure the target is always within the game's bounds to stop the target getting stuck
        currentPosition = new Vector3(Mathf.Clamp(currentPosition.x, MinX, MaxX), Mathf.Clamp(currentPosition.y, MinY, MaxY), Mathf.Clamp(currentPosition.z, MinZ, MaxZ));
        
        // Move the target in the direction it is going * the velocity * the frame time
        Vector3 newPosition = currentPosition + (Direction * Velocity * Time.deltaTime);
       
        gameObject.transform.SetPositionAndRotation(newPosition, Quaternion.identity);
    }

    /**
     * Called when the target has been hit.  This could be improved to make an explosion or
     * create some particles or something but at present it just destroys the target.
     */
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
