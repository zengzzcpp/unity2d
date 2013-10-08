using UnityEngine;
using System.Collections;

public class StudyISO : MonoBehaviour {
	public Transform sprite;
	public float TitleHeight;
	public float TitleWidth;
	public float MinMoveStep;
	
	// Use this for initialization
	void Start () {

	}
	
	void Update() {
		if(Input.GetKey(KeyCode.W)) {
			Vector3 v = sprite.position;
			v.y += TitleHeight;
			sprite.position = v;
		} else if(Input.GetKey(KeyCode.S)) {
			Vector3 v = sprite.position;
			v.y -= TitleHeight;
			sprite.position = v;
		}
		if(Input.GetMouseButton(0)) {
			float deltaX = Input.GetAxis("Mouse X");
			if(deltaX > 1.0f) MoveRight();
			else if(deltaX < -1.0f) MoveLeft();
			
			float deltaY = Input.GetAxis("Mouse Y");
			if(deltaY > 1.0f) MoveUp();
			else if(deltaY < -1.0f) MoveDown();
		}
		float wheel = Input.GetAxis("Mouse ScrollWheel");
		
		if(wheel > 0.2f)
		 	Camera.main.orthographicSize += 60.0f;
		else if(wheel < -0.2f)
			Camera.main.orthographicSize -= 60.0f;
	}
	
	void MoveUp() {
		Vector3 v = sprite.position;
		v.y += TitleHeight;
		sprite.position = v;
	}
	void MoveDown() {
		Vector3 v = sprite.position;
		v.y -= TitleHeight;
		sprite.position = v;
	}
	void MoveLeft() {
		Vector3 v = sprite.position;
		v.x -= TitleWidth;
		sprite.position = v;
	}
	void MoveRight() {
		Vector3 v = sprite.position;
		v.x += TitleWidth;
		sprite.position = v;
	}
}
