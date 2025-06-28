using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void MoveToPanel(Transform panelTransform)
    {
        float canvasScale = panelTransform.root.localScale.x; 
        Vector3 panelWorldPos = panelTransform.localPosition * canvasScale;

        targetPosition = new Vector3(panelWorldPos.x, panelWorldPos.y, -10f); 
    }
}
