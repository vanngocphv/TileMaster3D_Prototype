using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
    public class CreateTileEditor : EditorWindow
    {
        // object and property
        protected SerializedObject serializedObject;
        protected SerializedProperty serializedProperty;

        // a list scriptable object, tile object
        protected TileInfo_SBO[] tiles;

        public TileInfo_SBO newTile;

        private void OnGUI()
        {
            tiles = GetAllInstances<TileInfo_SBO>();            // Get all tiles item
            // set object = new tile data
            serializedObject = new SerializedObject(newTile);
            serializedProperty = serializedObject.GetIterator();
            serializedProperty.NextVisible(true);
            DrawProperties(serializedProperty);

            newTile.TileName = "Tile-" + (tiles.Length + 1);
            if (GUILayout.Button("save"))
            {
                // Create asset
                AssetDatabase.CreateAsset(newTile, "Assets/ScriptableObject/Tiles/Tile-"+(tiles.Length+1)+".asset");
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

        public static T[] GetAllInstances<T>() where T : TileInfo_SBO
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