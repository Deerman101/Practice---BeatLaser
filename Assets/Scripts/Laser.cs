using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Laser : MonoBehaviour
{
    private bool _isSlicing = false;
    public int maxDistance;
    public LayerMask layerMask;
    public GameObject slicer;
    public ScoreManagerScript _managerScript;

    void Update()
    {
#if UNITY_EDITOR // Для тестов на ПК
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, maxDistance);
        Debug.DrawLine(ray.origin, hit.point, Color.red);

        if (Input.GetMouseButtonUp(0))
        {
            _isSlicing = true;
            if (hit.collider != null && hit.collider.tag == "cube")
            {
                Debug.DrawLine(ray.origin, hit.point, Color.blue);
                GameObject plane = Instantiate(slicer, hit.point, Quaternion.identity);
                plane.transform.rotation = hit.transform.rotation;
                Destroy(plane, 0.1f);

                _managerScript.AddScore(2);
                _managerScript.IncreaseCombo();
            }

        }
        else if (_isSlicing)
            Debug.DrawLine(ray.origin, hit.point, Color.green);
#elif UNITY_ANDROID // Для тестов на смартфоне
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    Physics.Raycast(ray, out hit, maxDistance, layerMask.value);
                    Debug.DrawLine(ray.origin, hit.point, Color.red);

                    if (hit.collider != null && hit.collider.tag == "cube")
                    {
                        GameObject plane = Instantiate(slicer, hit.point, Quaternion.identity);
                        plane.transform.rotation = hit.transform.rotation;
                        Destroy(plane, 0.1f);

                        _managerScript.AddScore(2);
                        _managerScript.IncreaseCombo();
                    }
                    break;
                case TouchPhase.Ended:
                    break;
            }
        }
#endif

    }
}

// Для тестов на ПК

//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using static UnityEngine.GraphicsBuffer;

//public class Laser : MonoBehaviour
//{
//    private bool _isSlicing = false;
//    public int maxDistance;
//    public LayerMask layerMask;
//    public GameObject slicer;
//    public ScoreManagerScript _managerScript;

//    void Update()
//    {
//        RaycastHit hit;
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        Physics.Raycast(ray, out hit, maxDistance);
//        Debug.DrawLine(ray.origin, hit.point, Color.red);

//        if (Input.GetMouseButtonUp(0))
//        {
//            _isSlicing = true;
//            if (hit.collider != null && hit.collider.tag == "cube")
//            {
//                Debug.DrawLine(ray.origin, hit.point, Color.blue);
//                GameObject plane = Instantiate(slicer, hit.point, Quaternion.identity);
//                plane.transform.rotation = hit.transform.rotation;
//                Destroy(plane, 0.1f);

//                _managerScript.AddScore(2);
//                _managerScript.IncreaseCombo();
//            }

//        }
//        else if (_isSlicing)
//            Debug.DrawLine(ray.origin, hit.point, Color.green);
//    }
//}