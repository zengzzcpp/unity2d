using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateGameSprite : EditorWindow {
	// Reference to atlas data game object
	public GameAtlasData AtlasData;
	public int PopupIndex = 0;
	public GameObject Sprite;
	public string SpriteName = "SpriteName";
	
	public bool showShadow;
	public bool showGround;
	public bool isTile;
	
	Material shadowMaterial;
	Material SpriteMaterial;
	
	[MenuItem("Firebug/Create Game Sprite")]
	static void Init() {
		GetWindow(typeof(CreateGameSprite), false, "Game Sprite Editor", true);
	}
	Transform t;
	void OnGUI() {
		GUILayout.Label("Game Sprite Generation", EditorStyles.boldLabel);
		
		AtlasData = EditorGUILayout.ObjectField("Atlas Object", AtlasData, typeof(GameAtlasData), true) as GameAtlasData;
		
		if(AtlasData == null) return;		
		
		PopupIndex = EditorGUILayout.Popup("TextureName", PopupIndex, AtlasData.TextureName);
		
		isTile = EditorGUILayout.Toggle("Is Tile", isTile);
		
		// Set UV
		if(GUILayout.Button("Set Sprite Texture")) {
			if(Selection.gameObjects.Length > 0) {
				GameObject go = Selection.gameObjects[0];
				
				
				if(go.renderer == null) return;
				if(go.renderer.sharedMaterial == null) return;
				
				Rect uv = AtlasData.UVs[PopupIndex];
				UpdateUVs(go, uv);
				Texture tex = go.renderer.sharedMaterial.mainTexture;
				go.transform.localScale = new Vector3(uv.width * tex.width, uv.height * tex.height, 1.0f);
			}
		}
		

		SpriteName = EditorGUILayout.TextField("Sprite Name", SpriteName);
		
		SpriteMaterial = EditorGUILayout.ObjectField("Sprite Material", SpriteMaterial, typeof(Material), true) as Material;
		
		showShadow = EditorGUILayout.Toggle("Show Shadow", showShadow);
		if(showShadow) {
			shadowMaterial = EditorGUILayout.ObjectField("Shadow Material", shadowMaterial, typeof(Material), true) as Material;
		}
		
		showGround = EditorGUILayout.Toggle("Show Ground", showGround);
		
		if(GUILayout.Button("Create Sprite")) {
			GameObject Sprite = new GameObject(SpriteName);
			GameObject quare = SpriteFactory.GetSprite("Square");
			quare.name = "Tower";
			
			quare.transform.renderer.material = SpriteMaterial;
			
			Texture t = SpriteMaterial.mainTexture;
			
			quare.transform.localScale = new Vector3(t.width, t.height, 0.0f);// 199.0f, 266.0f, 0.0f);
			quare.transform.parent = Sprite.transform;
			if(showShadow)
				AddShadow(Sprite.transform);
		}
		
		if(GUILayout.Button("Create Tile")) {
			GameObject Sprite = SpriteFactory.GetSprite("Tile");
			Sprite.name = SpriteName;			
			Texture t = SpriteMaterial.mainTexture;
			Sprite.transform.localScale = new Vector3(t.width, t.height, 1.0f);
			Sprite.renderer.material = SpriteMaterial;	
		}
	}
	
	void UpdateUVs(GameObject obj, Rect atlasUVs, bool reset = false) {
		// Get mesh filter component
		MeshFilter meshFilter = obj.GetComponent<MeshFilter>() as MeshFilter;
		if(meshFilter == null) return;
		
		Mesh meshObject = meshFilter.sharedMesh;
		
		// Vertices
		Vector3[] Vertices = meshObject.vertices;
		Vector2[] UVs = new Vector2[Vertices.Length];
		if(isTile) {
			UVs[0].x = (reset)? 0.5f : atlasUVs.x + atlasUVs.width * 0.5f;
			UVs[0].y = (reset)? 1.0f : atlasUVs.y + atlasUVs.height;
			
			UVs[1].x = (reset) ? 0.5f : atlasUVs.x + atlasUVs.width * 0.5f;
			UVs[1].y = (reset) ? 0.0f : atlasUVs.y;
					
			UVs[2].x = (reset) ? 0.0f : atlasUVs.x;
			UVs[2].y = (reset) ? 0.5f : atlasUVs.y + atlasUVs.height * 0.5f;
			
			UVs[3].x = (reset) ? 1.0f : atlasUVs.x;
			UVs[3].y = (reset) ? 0.5f : atlasUVs.y + atlasUVs.height * 0.5f;			
		} else {
			UVs[0].x = (reset)? 0.0f : atlasUVs.x;
			UVs[0].y = (reset)? 0.0f : atlasUVs.y;
			
			UVs[1].x = (reset) ? 1.0f : atlasUVs.x + atlasUVs.width;
			UVs[1].y = (reset) ? 1.0f : atlasUVs.y + atlasUVs.height;
					
			UVs[2].x = (reset) ? 1.0f : atlasUVs.x + atlasUVs.width;
			UVs[2].y = (reset) ? 0.0f : atlasUVs.y;
			
			UVs[3].x = (reset) ? 0.0f : atlasUVs.x;
			UVs[3].y = (reset) ? 1.0f : atlasUVs.y + atlasUVs.height;
		}
		meshObject.uv = UVs;
		//meshObject.vertices = Vertices;
		
		//AssetDatabase.Refresh();
		//AssetDatabase.SaveAssets();
	}
	
	void AddShadow(Transform go) {
		GameObject shadow = SpriteFactory.GetSprite("Square");
		shadow.transform.parent = go;
		shadow.name = "Shadow";
		shadow.transform.localScale = new Vector3(191.0f, 256.0f, 1.0f);
		shadow.transform.localRotation = Quaternion.Euler(0, 180, 90);
		shadow.transform.localPosition = new Vector3(0.0f, -62.0f, 1.0f);
		
		shadow.renderer.material = shadowMaterial;				
	}
	
}
