/*using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        // Check if the left mouse button or touch has been pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Calculate the offset between the object's position and the mouse position
            offset = gameObject.transform.position - GetMouseWorldPosition();

            // Set dragging to true
            isDragging = true;
        }
    }

    void OnMouseUp()
    {
        // Check if the left mouse button or touch has been released
        if (Input.GetMouseButtonUp(0))
        {
            // Set dragging to false
            isDragging = false;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            // Update the object's position based on the current mouse position and the offset
            gameObject.transform.position = GetMouseWorldPosition() + offset;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        //mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        //distance from the camera to the object
        float distanceFromCamera = 3    f;

        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * distanceFromCamera, Color.red);

        // Calculate the position of the object along the ray
        Vector3 worldPosition = ray.GetPoint(distanceFromCamera);

        return worldPosition;
    }
}
*/
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody rb;
    private float throwForce = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        // Check if the left mouse button or touch has been pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Calculate the offset between the object's position and the mouse position
            offset = gameObject.transform.position - GetMouseWorldPosition();

            // Set dragging to true
            isDragging = true;
        }
    }

    void OnMouseUp()
    {
        // Check if the left mouse button or touch has been released
        if (Input.GetMouseButtonUp(0))
        {
            // Set dragging to false
            isDragging = false;

            // Calculate the throwing velocity based on mouse movement during dragging
            Vector3 throwVelocity = (GetMouseWorldPosition() + offset - gameObject.transform.position) * throwForce;

            // Apply the throwing velocity to the object's rigidbody
            rb.velocity = throwVelocity;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            // Update the object's position based on the current mouse position and the offset
            gameObject.transform.position = GetMouseWorldPosition() + offset;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Set the desired distance from the camera to the object
        float distanceFromCamera = 3f;

        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // Calculate the position of the object along the ray
        Vector3 worldPosition = ray.GetPoint(distanceFromCamera);

        return worldPosition;
    }
}