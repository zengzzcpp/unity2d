using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class AtlasTexture : ScriptableWizard {
	public string AtlasTextureName = "Atlas_Texture";
	public string AtlasDataName = "Atlas_Data";
	public string SavePath = "Assets/Texture/";
	public int padding = 4;
	public Texture2D[] Textures;
	
	
	[MenuItem("Firebug/Create Atlas")]
	static void CreateWizard() {
		ScriptableWizard.DisplayWizard("Create Atlas", typeof(AtlasTexture));
	}

	void OnWizardCreate() {
		GenerateAtlas();
	}

	void GenerateAtlas() {
		GameObject AtlasObject = new GameObject(AtlasDataName);
		
		GameAtlasData atlasData = AtlasObject.AddComponent<GameAtlasData>();
		
		// Generate texture name
		atlasData.TextureName = new string[Textures.Length];
		
		for(int i = 0; i < atlasData.TextureName.Length; i++) {
			string path = AssetDatabase.GetAssetPath(Textures[i]);
			ConfigureTextureFormat(path);
			atlasData.TextureName[i] = path;	
		}
		
		// Generate Atlas
		Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		
		atlasData.UVs = tex.PackTextures(Textures, padding, 4096);
		
		string AssetPath = AssetDatabase.GenerateUniqueAssetPath(SavePath + AtlasTextureName + ".png");
		
		// Write texture to file
		byte[] bytes = tex.EncodeToPNG();
		File.WriteAllBytes(AssetPath, bytes);
		bytes = null;
		
		// Delete generated texture
		UnityEngine.Object.DestroyImmediate(tex);
		
		// Import Asset
		AssetDatabase.ImportAsset(AssetPath);
		
		// Get imported texture
		tex = AssetDatabase.LoadAssetAtPath(AssetPath, typeof(Texture2D)) as Texture2D;
		
		ConfigureTextureFormat(AssetDatabase.GetAssetPath(tex));
		
		atlasData.AtlasTexture = tex;
	}
	
	public void ConfigureTextureFormat(string TexturePath) {
		TextureImporter textureImporter = AssetImporter.GetAtPath(TexturePath) as TextureImporter;
		TextureImporterSettings settings = new TextureImporterSettings();
		
		textureImporter.textureType = TextureImporterType.Advanced;
		settings.mipmapEnabled = false;
		settings.readable = true;
		settings.maxTextureSize = 4096;
		settings.textureFormat = TextureImporterFormat.ARGB32;
		settings.filterMode = FilterMode.Point;
		settings.wrapMode = TextureWrapMode.Clamp;
		settings.npotScale = TextureImporterNPOTScale.None;
		
		textureImporter.SetTextureSettings(settings);
		
		AssetDatabase.ImportAsset(TexturePath, ImportAssetOptions.ForceUpdate);
		AssetDatabase.Refresh();
	}
}
 