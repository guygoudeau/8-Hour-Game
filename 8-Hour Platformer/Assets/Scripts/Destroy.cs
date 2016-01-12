using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

    public float lifetime;

	// Use this for initialization
	void Start () {
        DestroyObject(gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
