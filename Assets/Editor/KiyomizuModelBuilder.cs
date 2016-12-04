using UnityEngine; 
using UnityEditor;
using System.Collections;

public class EditorExWindowKiyomizuModelBuilder : EditorWindow
{
	[MenuItem("Kiyomizu/ModelBuilder")]
	static void Open()
	{
		EditorWindow.GetWindow<EditorExWindowKiyomizuModelBuilder>( "KiyomizuModelBuilder" );
	}
	
	void OnGUI()
	{
		EditorGUILayout.LabelField( "清水寺参詣曼茶羅モデル生成" );
 
		EditorGUILayout.BeginHorizontal( GUI.skin.box );
		{
 
			EditorGUILayout.BeginVertical( GUI.skin.box );
			{
				EditorGUILayout.LabelField( "モデルの生成 ky01" );

				if( GUILayout.Button( "実行" ) )	{
					Debug.Log( "モデル生成中" );
					BuildModel( "ky01" );
					Debug.Log( "モデル生成完了" );
				}
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal( GUI.skin.box );
		{
			EditorGUILayout.BeginVertical( GUI.skin.box );
			{
				EditorGUILayout.LabelField( "モデルの生成 ky01_inf" );
				
				if( GUILayout.Button( "実行" ) )	{
					Debug.Log( "モデル生成中" );
					BuildModel( "ky01_inf" );
					Debug.Log( "モデル生成完了" );
				}
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndHorizontal();
	}

	void BuildModel( string namePrefix )
	{
		Debug.Log( "モデル生成..." );

		GameObject grp = new GameObject();
		grp.name = namePrefix;

		int numCol = 21;
		int numRow = 20;
		float scale = 85f;
		//string namePrefix = "ky01_";

		Shader shader = Shader.Find( "Custom/UnlitSwitchTexture" );
		if( shader == null ) {
			Debug.Log( "シェーダーの読み込みに失敗しました（Custom/UnlitSwitchTexture）" );
			return;
		}

		for( int i = 0; i < numRow; i++ ) {
			for( int j = 0; j < numCol; j++ ) {
				GameObject obj = GameObject.CreatePrimitive( PrimitiveType.Quad );
				obj.name = namePrefix + "_" + string.Format ( "{0:D3}", numCol * i + j + 1 );
				obj.transform.parent = grp.transform;
				obj.transform.localScale = new Vector3( scale, scale, 1f );
				obj.transform.localPosition = new Vector3( (float)j * scale - scale * (float)numCol / 2.0f + scale / 2f,  scale * (float)numRow / 2.0f - (float)i * scale - scale / 2f, 0f );
				Renderer rend = obj.GetComponent<Renderer>();
				if( rend ) {
					rend.material = new Material( shader );
					Texture2D tex = Resources.Load ( obj.name ) as Texture2D;
					Texture2D texWhite = Resources.Load ( "white" ) as Texture2D;
					if( tex == null || texWhite == null) {
						Debug.Log( "テクスチャの読み込みに失敗しました（" + obj.name + "）" );
						continue;
					}
					rend.sharedMaterial.mainTexture = tex;
					rend.sharedMaterial.SetTexture( "_SwitchTex", texWhite );
					rend.sharedMaterial.SetFloat( "_SwitchParam", 1.0f );
				}
			}
		}
	}

}