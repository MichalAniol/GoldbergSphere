using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraLook : MonoBehaviour
{
    public GameObject arrow, empty;
    GameObject targetGO;
    Vector3 oddal = new Vector3(0, 10, 0);
    Quaternion look;
    Transform target;
    float proportion;

    public GameObject centrum;

    Vector3 odchylenie;
    Quaternion zajrzyj;

    void Start()
    {
      //  target = arrow.GetComponent<ArrowMove>().one.transform;
      //  look = Quaternion.FromToRotation(Vector3.up, arrow.GetComponent<ArrowMove>().one.transform.position);
        targetGO = Instantiate(empty, Vector3.zero, Quaternion.identity);
        targetGO.name = "Target";

        target = centrum.transform; // arrow.GetComponent<ArrowMove>().one.transform;
                                    //   transform.LookAt(target);
                                    //   transform.position = look * oddal;
    }

    void Update()
    {
        /* if (Input.GetMouseButton(0))
         {
             Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

             if (Input.GetMouseButtonDown(0))
             {
                 odchylenie = mRay.GetPoint(1);
             }

             zajrzyj = Quaternion.FromToRotation(odchylenie, mRay.GetPoint(1));

             targetGO.transform.LookAt(target);
             targetGO.transform.position = zajrzyj * look * oddal;

             if (transform.position.y < (oddal.y * .9f) && transform.position.y > -(oddal.y * .9f))
             {
                 transform.position = Vector3.Lerp(transform.position, targetGO.transform.position, .05f);
                 transform.rotation = Quaternion.Lerp(transform.rotation, targetGO.transform.rotation, .05f);
             }
         }*/


        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                odchylenie = mRay.GetPoint(1);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Ray mRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                zajrzyj = Quaternion.FromToRotation(odchylenie, mRay.GetPoint(1));

                targetGO.transform.LookAt(target);
                targetGO.transform.position = zajrzyj * look * oddal;
            }

            if (transform.position.y < (oddal.y * .9f) && transform.position.y > -(oddal.y * .9f))
            {
                transform.position = Vector3.Lerp(transform.position, targetGO.transform.position, .05f);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetGO.transform.rotation, .05f);
            }

        }
        else
        {
            if (arrow.GetComponent<ArrowMove>().one.transform != null)
            {
                look = Quaternion.FromToRotation(Vector3.up, arrow.GetComponent<ArrowMove>().one.transform.position);

                targetGO.transform.LookAt(target);
                targetGO.transform.position = look * oddal;

                proportion = .24f / Vector3.Magnitude(transform.position - targetGO.transform.position);

                transform.position = Vector3.Lerp(transform.position, targetGO.transform.position, proportion);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetGO.transform.rotation, proportion);
            }
        }
    }
}
