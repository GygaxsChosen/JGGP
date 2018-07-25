using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour {

    public GameObject player;
    private float offsetx;
    private float offsety;
    private Vector3 offset;

    void Start()
    {
        offsetx = transform.position.x - player.transform.position.x;
        offsety = transform.position.y - player.transform.position.y; ;
        offset = new Vector3(offsetx, offsety, -.5f);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

}
