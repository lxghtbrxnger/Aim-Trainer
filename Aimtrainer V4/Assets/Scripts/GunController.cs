using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    /**
     * Store a reference to the camera so we can calculate where the user clicked.
     * We want to keep this reference so we don't have to look up the camera each frame,
     * which would slow down the game's performance.
     */
    private Camera _camera;

    // Public variables.  In Unity, the editor allows you to inspect public variables while
    // playing which makes debugging much easier.  Unity also remembers the value you set as
    // a default, which means you can set up a lot of your game's constants in the Unity UI

    // The Gun Controller tracks the bullets left in the magazine
    public TMP_Text BulletsText;

    // The Magazine Size was public as I assigned it through the Unity editor.  This let me
    // experiment with various magazine sizes easily without needing to recompile my game
    public int MagazineSize;

    // This is public so I can see the number of bullets in the editor to help with debugging
    public int Bullets;

    /**
     * Record the reference to the camera at game start, and give the player a full magazine
     */
    void Start()
    {
        _camera = Camera.main;
        Bullets = MagazineSize;
    }

    /**
     * Called many times a second.  The Gun Controller determines whether the player
     * has fired the gun or not this frame, and if so whether they hit anything.  It
     * also displays how many bullets are left in the magazine.  
     */
    void Update()
    {
        BulletsText.text = "" + Bullets;

        // Set the bullet count to Orange or Red as the number of bullets goes down to zero
        if(Bullets == 0)
        {
            BulletsText.color = Color.red;
        } else if(Bullets < 6)
        {
            BulletsText.color = new Color(1.0f, 0.5f, 0.0f);
        }
        else
        {
            BulletsText.color = Color.white;
        }

        // Check if the player is reloading
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        // Only check for hits if there are bullets in the gun
        if (Bullets > 0 && Input.GetMouseButtonDown(0))
        {
            Bullets--;

            // Hit detection is done by casting a ray from the camera to the mouse position.
            // If the ray intersects with something, then it is a hit - otherwise it's a miss.
            Vector3 mousePos = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                // A rigidbody is a Unity component set on the Targets
                if(hit.rigidbody != null)
                {
                    GameObject go = hit.rigidbody.gameObject;
                    TargetBehaviour tb = go.GetComponent<TargetBehaviour>();
                    if(tb != null)
                    {
                        // record the hit
                        GameController.Hit();

                        // inform the target of its demise
                        tb.Die();
                    }
                }
            } else
            {
                // Nothing was hit, record a miss
                GameController.Miss();
            }
        }
    }

    /**
     * This is written as a co-routine so that reloading can take 2 seconds, but the
     * entire game doesn't pause while we wait for the reload.
     */
    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        Bullets = MagazineSize;
        yield return null;
    }
}
