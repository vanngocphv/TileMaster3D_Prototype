using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAction : MonoBehaviour
{
    public Camera mainCam;
    public LayerMask tileLayer;
    public float clickCountDown = 0.1f;

    private float countDown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && countDown <= 0 && GameManager.Instance.IsClickEnable())
        {
            //create a ray from mid position of camera point to click of mouse
            //Get position of mouse when clicked
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = mainCam.ScreenPointToRay(touch.position);

                //create a variable for store if ray hit something
                RaycastHit hit;

                //call a function with name Physics, this is cast a logic ray, with 2 - 4 parameter
                if (Physics.Raycast(ray, out hit, 20f, tileLayer))
                {
                    if (hit.collider.gameObject.TryGetComponent<Tile>(out Tile tile))
                    {
                        TileBar.Instance.AddTile(tile);
                    }

                }

                countDown = clickCountDown;
            }
        }

        countDown -= Time.deltaTime;

    }
}
