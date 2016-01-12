using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour {

    public GameObject player;   //Referances Player

    private Vector3 offset;     //The offset from player

	void Start () {     //When program star
        offset = transform.position - player.transform.position;
	}
	
	void LateUpdate () {
        transform.position = player.transform.position + offset;
	}
}
