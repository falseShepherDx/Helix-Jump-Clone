using System;
using UnityEngine;
public class RotatePlatforms : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private float moveX;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        moveX = Input.GetAxis("Mouse X");

        if (Input.GetMouseButton(0))
        {
            transform.Rotate(0,moveX*rotateSpeed*Time.deltaTime,0);
        }
    }
}
