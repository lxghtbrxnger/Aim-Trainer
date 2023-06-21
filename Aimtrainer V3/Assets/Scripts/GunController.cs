using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunController : MonoBehaviour
{
    private Camera _camera;
    // Start is called before the first frame update

    public TMP_Text bulletsText;

    public int magazineSize;
    public int bullets;

    void Start()
    {
        _camera = Camera.main;
        bullets = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        bulletsText.text = "" + bullets;
        if(bullets == 0)
        {
            bulletsText.color = Color.red;
        } else if(bullets < 6)
        {
            bulletsText.color = new Color(1.0f, 0.5f, 0.0f);
        }
        else
        {
            bulletsText.color = Color.white;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (bullets > 0 && Input.GetMouseButtonDown(0))
        {
            GameController.Shot();
            bullets--;

            Vector3 mousePos = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if(hit.rigidbody != null)
                {
                    GameObject go = hit.rigidbody.gameObject;
                    TargetBehaviour tb = go.GetComponent<TargetBehaviour>();
                    if(tb != null)
                    {
                        GameController.Hit();
                        tb.Die();                        
                    }
                }
            } else
            {
                GameController.Miss();
            }


        }
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        bullets = magazineSize;
        yield return null;
    }
}
