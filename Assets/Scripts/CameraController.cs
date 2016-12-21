using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector2 playerPosition;
	private Vector2 cameraPosition;

	void Update () {
		playerPosition = player.transform.position;
		cameraPosition = gameObject.transform.position;
	}
	
	void LateUpdate ()
	{
		if (player.transform.position.x > -26.65f && player.transform.position.x < 169.65f)
		/*(player.GetComponent<Rigidbody2D>().velocity.x >= 0)*/{
			if(playerPosition.x > cameraPosition.x)
			transform.position = new Vector3(player.transform.position.x, 6.45f, -3f);
		}
	}
}