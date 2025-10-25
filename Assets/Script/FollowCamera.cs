using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPos = new Vector3(player.position.x + offset.x, offset.y, player.position.z + offset.z);
            transform.position = newPos;
        }
    }
}
