using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform lookAt;

    private Vector3 offset = new Vector3(2f, 0f, -9f);

    private void LateUpdate()
    {

        transform.position = lookAt.transform.position + offset;

    }

} // CameraMovement
