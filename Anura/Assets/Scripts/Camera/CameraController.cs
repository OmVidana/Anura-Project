using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool isFollowing;
    [SerializeField] private Transform followEntity;
    [SerializeField] private Vector3 cameraPosition;
    [SerializeField] private Vector3 cameraRotation;
    [SerializeField] private Vector2 maxCameraRotation;
    public Vector2 rotationSpeed;
    private float rotationInputHorizontal;
    private float rotationInputVertical;
    private float rotationX;
    private float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = cameraRotation;
        transform.position = cameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing && (followEntity.CompareTag("Player") || followEntity.CompareTag("Enemy")))
            transform.position = new Vector3(followEntity.position.x + cameraPosition.x, cameraPosition.y, cameraPosition.z);

        rotationInputHorizontal = Input.GetAxis("Camera Vertical");
        rotationInputVertical = Input.GetAxis("Camera Horizontal");

        if (rotationInputHorizontal != 0 || rotationInputVertical != 0)
        {
            rotationX -= rotationInputHorizontal * rotationSpeed.x * Time.deltaTime;
            rotationY += rotationInputVertical * rotationSpeed.y * Time.deltaTime;

            rotationX = Mathf.Clamp(rotationX, cameraRotation.x - maxCameraRotation.x, cameraRotation.x + maxCameraRotation.x);
            rotationY = Mathf.Clamp(rotationY, cameraRotation.y - maxCameraRotation.y, cameraRotation.y + maxCameraRotation.y);
        }
        else
        {
            rotationX = Mathf.Lerp(rotationX, cameraRotation.x, rotationSpeed.x * Time.deltaTime);
            rotationY = Mathf.Lerp(rotationY, cameraRotation.y, rotationSpeed.x * Time.deltaTime);
        }
        
        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
    }
}