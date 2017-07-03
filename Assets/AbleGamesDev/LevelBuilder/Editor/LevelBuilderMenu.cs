using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// Level Builder: Helps create 2D levels
/// </summary>

namespace AGDLevelBuilder
{
	public class LevelBuilderMenu : EditorWindow 
	{
		// creates the Level_Map gameobject and attaches the LevelBuilder script to it
		[MenuItem ("Level Builder/Create LevelMap %#_m")]
		static void CreateLevelMap()
		{
			// create a new gameobject
			GameObject level_map = new GameObject();

			// give it a name
			level_map.name = "Level_Map";

			// add the LevelBuilder script to it
			level_map.AddComponent<LevelBuilder>();

			// highlight the newly created gameobject in the Scene View
			Selection.activeGameObject = level_map;
		}

		// shows the Prefabs Builder editor window
		[MenuItem ("Level Builder/Prefabs Builder %#_u")]
		static void BuildPrefabs()
		{
			EditorWindow.GetWindow(typeof(PrefabsBuilder));
		}
	}
}