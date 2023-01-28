using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] GameObject camera;
    private static GameObject player;
    public float camera_high;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = new Vector3(player.transform.position.x, camera_high, player.transform.position.z );
    }

    public static void setPlayer(GameObject pl)
    {
        player = pl;
    }
}
