using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _distanceFromCamera = 5.0f;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = _distanceFromCamera;
            Vector3 mouseScreenToWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = mouseScreenToWorld;
        }
        
    }
}
