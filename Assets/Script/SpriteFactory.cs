using UnityEngine;
using System.Collections;

public class SpriteFactory : MonoBehaviour {
	public static GameObject GetSprite(string spriteType) {
		GameObject go = new GameObject();
		
		go.AddComponent<MeshFilter>();
		go.AddComponent<MeshRenderer>();
		
		Mesh mesh = new Mesh();
		Vector3[] vertices = new Vector3[4];
		Vector2[] uvs = new Vector2[4];
		int[] triangles;
		
		if(spriteType == "Square") {
			vertices[0] = new Vector3(0.5f, -0.5f, 0.0f);
			vertices[1] = new Vector3(-0.5f, 0.5f, 0.0f);
			vertices[2] = new Vector3(-0.5f, -0.5f, 0.0f);
			vertices[3] = new Vector3(0.5f, 0.5f, 0.0f);
			
			uvs[0] = new Vector2(0.0f, 0.0f);
			uvs[1] = new Vector2(1.0f, 1.0f);
			uvs[2] = new Vector2(1.0f, 0.0f);
			uvs[3] = new Vector2(0.0f, 1.0f);
			
			triangles = new int[] {0, 1, 2, 0, 3, 1};
			
			go.transform.localRotation = new Quaternion(0.0f, 180.0f, 0.0f, 1.0f);
		} else if(spriteType == "Tile") {
			vertices[0] = new Vector3(0.0f, 0.372f, 0.0f);
			vertices[1] = new Vector3(0.0f, -0.372f, 0.0f);
			vertices[2] = new Vector3(-0.5f, 0.0f, 0.0f);
			vertices[3] = new Vector3(0.5f, 0.0f, 0.0f);
			
			uvs[0] = new Vector2(0.5f, 1.0f);
			uvs[1] = new Vector2(0.5f, 0.0f);
			uvs[2] = new Vector2(0.0f, 0.5f);
			uvs[3] = new Vector2(1.0f, 0.5f);
			
			triangles = new int[] {0, 1, 2, 0, 3, 1};
		} else {
			triangles = new int[6];
		}
		
		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		
		go.GetComponent<MeshFilter>().sharedMesh = mesh;
		
		return go;
	}
}
