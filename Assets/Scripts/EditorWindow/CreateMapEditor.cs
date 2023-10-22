using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
    public class CreateMapEditor : EditorWindow
    {
        // object and property
        protected SerializedObject serializedObject;
        protected SerializedProperty serializedProperty;

        // a list scriptable object, tile object
        protected MapLevel_SBO[] maps;

        public MapLevel_SBO newMap;

        private void OnGUI()
        {
            maps = GetAllInstances<MapLevel_SBO>();            // Get all tiles item
            // set object = new tile data
            serializedObject = new SerializedObject(newMap);
            serializedProperty = serializedObject.GetIterator();
            serializedProperty.NextVisible(true);
            DrawProperties(serializedProperty);
            newMap.Level = maps.Length;
            newMap.LevelName = "Level " + maps.Length;
            if (GUILayout.Button("save"))
            {
                // Create asset
                AssetDatabase.CreateAsset(newMap, "Assets/ScriptableObject/MapLevels/MapLevel-" + maps.Length + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Close();
            }
            Apply();
        }

        // draw all properties of one object
        protected void DrawProperties(SerializedProperty p)
        {
            while (p.NextVisible(false))
            {
                EditorGUILayout.PropertyField(p, true);
                if (p.displayName == "Tile Sprite")
                {
                    Texture2D texture = AssetPreview.GetAssetPreview(p.objectReferenceValue);
                    if (texture != null) GUILayout.Label(texture);
                }
            }
        }

        public static T[] GetAllInstances<T>() where T : MapLevel_SBO
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
        }

        // Apply Modify change
        protected void Apply()
        {
            if (serializedObject != null)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
#endif