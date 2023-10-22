using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
    public class TileConfigEditor : EditorWindow
{
    // object and property
    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    // a list scriptable object, tile object
    protected TileInfo_SBO[] tiles;

    // for drawing a piece of pieces
    protected string selectedPropertyPach;
    protected string selectedProperty;

    // Scroll View
    Vector2 scrollPosTitle = Vector2.zero;
    Vector2 scrollPosDetail = Vector2.zero;

    [MenuItem("Window/Game Data/Tile Config")]
    protected static void ShowWindow()
    {
        // show the window
        GetWindow<TileConfigEditor>("Tile Config");
    }

    private void OnGUI()
    {
        tiles = GetAllInstances<TileInfo_SBO>();
        // for drawing a box selected
        EditorGUILayout.BeginHorizontal();
        // draw the select title
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true)); // A(Selected)   |   context
        scrollPosTitle = EditorGUILayout.BeginScrollView(scrollPosTitle, GUILayout.ExpandHeight(true));
        DrawSlideBar(tiles);
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        // draw the content data response with select title
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        scrollPosDetail = EditorGUILayout.BeginScrollView(scrollPosDetail, GUILayout.ExpandHeight(true));

        if (selectedProperty != null)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                if (tiles[i].TileName == selectedProperty)
                {
                    serializedObject = new SerializedObject(tiles[i]);
                    serializedProperty = serializedObject.GetIterator();
                    serializedProperty.NextVisible(true);
                    DrawProperties(serializedProperty);
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select a Tile from the List");
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        Apply();
    }




    // Tiles Config (for using outside)
    public string DrawTilesTab(string selected)
    {
        tiles = GetAllInstances<TileInfo_SBO>();
        // for drawing a box selected
        EditorGUILayout.BeginHorizontal();
        // draw the select title
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true)); // A(Selected)   |   context
        scrollPosTitle = EditorGUILayout.BeginScrollView(scrollPosTitle, GUILayout.ExpandHeight(true));
        selectedProperty = selected;
        DrawSlideBar(tiles);
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        // draw the content data response with select title
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        scrollPosDetail = EditorGUILayout.BeginScrollView(scrollPosDetail, GUILayout.ExpandHeight(true));

        if (selectedProperty != null)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                if (tiles[i].TileName == selectedProperty)
                {
                    serializedObject = new SerializedObject(tiles[i]);
                    serializedProperty = serializedObject.GetIterator();
                    serializedProperty.NextVisible(true);
                    DrawProperties(serializedProperty);
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select a Tile from the List");
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        Apply();
        return selectedProperty;
    }


    // For the title of all tiles
    protected void DrawSlideBar(TileInfo_SBO[] allTiles)
    {
        foreach (TileInfo_SBO tile in allTiles)
        {
            if (GUILayout.Button(tile.TileName))
            {
                selectedPropertyPach = tile.TileName;
            }
        }

        // if not empty or null
        if (!string.IsNullOrEmpty(selectedPropertyPach))
        {
            selectedProperty = selectedPropertyPach;
        }

        // Draw create new Tiles
        if (GUILayout.Button("NEW"))
        {
            TileInfo_SBO newObj = TileInfo_SBO.CreateInstance<TileInfo_SBO>();
            CreateTileEditor newTileWindow = GetWindow<CreateTileEditor>("New Tile");
            newTileWindow.newTile = newObj;
        }
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
                GUILayout.Label(texture);
            }
        }
    }

    // For get all refer to TileInfo Scriptable Object in the project
    public static T[] GetAllInstances<T>() where T : TileInfo_SBO
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] a = new T[guids.Length];
        for (int i =0; i < guids.Length; i++)
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