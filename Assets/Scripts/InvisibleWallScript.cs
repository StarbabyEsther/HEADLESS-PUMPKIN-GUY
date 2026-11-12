using UnityEngine;

public class InvisibleWallScript : MonoBehaviour
{
    public float scrollSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * scrollSpeed * Time.deltaTime;
    }
}
