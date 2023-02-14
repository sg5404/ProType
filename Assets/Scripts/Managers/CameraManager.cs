using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeReference]
    Camera cam;

    [SerializeReference]
    Transform player;

    private void Start()
    {
        cam = Camera.main;
        player = FindObjectOfType<Player>().transform;
    }

    private void LateUpdate()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, player.position, Time.deltaTime * 4f) + (Vector3.back * 10);
    }
}
