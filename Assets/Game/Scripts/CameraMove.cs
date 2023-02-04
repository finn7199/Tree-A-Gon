using UnityEngine;

public class CameraMove : MonoBehaviour {

    Vector3 touchStart;
    public Camera cam;

    Vector3 camPos = Vector3.zero;

    [Range(0, 100)]
    [SerializeField] float scrollSensitivity = 10f;

    [SerializeField] float maxOrthoSize = 8;

    void Update() {
        if (Input.GetMouseButtonDown(1)) {
            touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
            camPos = transform.position;
        }

        if (Input.GetMouseButton(1)) {
            Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 pos = Vector3.Lerp(transform.position, camPos + direction, 0.8f);

            transform.position = pos;
        }

        if (Input.mouseScrollDelta.y < 0)
            cam.orthographicSize += 0.1f * scrollSensitivity;
        else if (Input.mouseScrollDelta.y > 0)
            cam.orthographicSize -= 0.1f * scrollSensitivity;

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 3, maxOrthoSize);
    }
}
