using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject enemy;
    Camera[] cameras;
    public CameraBrowser browser;
    
	// Use this for initialization
	void Start ()
    {
        cameras = GetComponentsInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if(browser.gameObject.activeSelf)
        //{
        //    for (int i = 0; i < cameras.Length; i++)
        //    {
        //        RaycastHit hit;
        //        Ray ray = new Ray(cameras[i].transform.position, enemy.transform.position - cameras[i].transform.position);
        //        Debug.DrawRay(ray.origin,ray.direction,Color.red,1);
        //        if (!Physics.Raycast(ray.origin, ray.direction, 1 << LayerMask.NameToLayer("Walls")))
        //        {
        //            if (Physics.Raycast(ray.origin, ray.direction, 1 << LayerMask.NameToLayer("Enemy")))
        //            {
        //                //if(hit.collider.gameObject.layer == 10)
        //                //{
        //                cameras[i].transform.LookAt(enemy.transform.position, new Vector3(0, 1, 0));
        //                browser.HighlightCamera(i);
        //                //}

        //            }
        //        }
        //    }
        //}
        
	}
}
