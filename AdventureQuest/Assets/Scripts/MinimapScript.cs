using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour {

    public Transform player;

    void LateUpdate()
    {
        Vector2 newPosition = player.position;
        newPosition.y = transform.position.y;
        newPosition.x = transform.position.x;
        transform.position = newPosition;
    }

}
