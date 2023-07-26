using UnityEngine;

public class DropZone : MonoBehaviour
{
    [SerializeField]
    private DraggableObject draggableObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        if (other.gameObject == draggableObject.gameObject)
        {
            draggableObject.SetSnappingPoint(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == draggableObject.gameObject)
        {
            draggableObject.UnsetSnappingPoint(this.gameObject);
        }
    }
}