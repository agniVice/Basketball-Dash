using UnityEngine;

public class FitColliderToCamera : MonoBehaviour
{
    private Camera mainCamera;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        mainCamera = Camera.main;
        boxCollider = GetComponent<BoxCollider2D>();

        if (mainCamera != null && boxCollider != null)
        {
            Vector2 cameraSize = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z)) - mainCamera.transform.position;

            boxCollider.size = cameraSize;
        }
        else
        {
            Debug.LogWarning("Camera or BoxCollider2D component is missing.");
        }
    }
}
