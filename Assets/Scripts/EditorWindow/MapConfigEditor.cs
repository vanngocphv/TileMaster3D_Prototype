using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
    public class MapConfigEditor : EditorWindow
{
    // object and property
    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    // a list scriptable object, map object
    protected MapLevel_SBO[] maps;

    // for drawing a piece of pieces
    protected string selectedPropertyPach;
    protected string selectedProperty;

    // Scroll View
    Vector2 scrollPosTitle = Vector2.zero;
    Vector2 scrollPosDetail = Vector2.zero;

    [MenuItem("Window/Game Data/Map Config")]
    protected static void ShowWindow()
    {
        // show the window
        GetWindow<MapConfigEditor>("Map Config");
    }

    private void OnGUI()
    {
        maps = GetAllInstances<MapLevel_SBO>();
        // for drawing a box selected
        EditorGUILayout.BeginHorizontal();
        // draw the select title
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true)); // A(Selected)   |   context
        scrollPosTitle = EditorGUILayout.BeginScrollView(scrollPosTitle, GUILayout.ExpandHeight(true));
        DrawSlideBar(maps);
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        // draw the content data response with select title
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        scrollPosDetail = EditorGUILayout.BeginScrollView(scrollPosDetail, GUILayout.ExpandHeight(true));

        

        if (selectedProperty != null)
        {
            for (int i = 0; i < maps.Length; i++)
            {
                if (maps[i].LevelName == selectedProperty)
                {
                    serializedObject = new SerializedObject(maps[i]);
                    serializedProperty = serializedObject.GetIterator();
                    serializedProperty.NextVisible(true);
                    DrawProperties(serializedProperty);
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select a Map from the List");
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        Apply();
    }




    // Maps Config (for using outside)
    public string DrawMapsTab(string selected)
    {
        maps = GetAllInstances<MapLevel_SBO>();
        // for drawing a box selected
        EditorGUILayout.BeginHorizontal();
        // draw the select title
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true)); // A(Selected)   |   context
        scrollPosTitle = EditorGUILayout.BeginScrollView(scrollPosTitle, GUILayout.ExpandHeight(true));
        selectedProperty = selected;
        DrawSlideBar(maps);
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        // draw the content data response with select title
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        scrollPosDetail = EditorGUILayout.BeginScrollView(scrollPosDetail, GUILayout.ExpandHeight(true));

        if (selectedProperty != null)
        {
            for (int i = 0; i < maps.Length; i++)
            {
                if (maps[i].LevelName == selectedProperty)
                {
                    serializedObject = new SerializedObject(maps[i]);
                    serializedProperty = serializedObject.GetIterator();
                    serializedProperty.NextVisible(true);
                    DrawProperties(serializedProperty);
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select a Map from the List");
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        Apply();
        return selectedProperty;
    }


    // For the title of all Maps
    protected void DrawSlideBar(MapLevel_SBO[] allMaps)
    {
        foreach (MapLevel_SBO map in allMaps)
        {
            if (GUILayout.Button(map.LevelName))
            {
                selectedPropertyPach = map.LevelName;
            }
        }

        // if not empty or null
        if (!string.IsNullOrEmpty(selectedPropertyPach))
        {
            selectedProperty = selectedPropertyPach;
        }

        // Draw create new Maps
        if (GUILayout.Button("NEW"))
        {
            MapLevel_SBO newObj = MapLevel_SBO.CreateInstance<MapLevel_SBO>();
            CreateMapEditor newMapWindow = GetWindow<CreateMapEditor>("New Map");
            newMapWindow.newMap = newObj;
        }
    }


    // draw all properties of one object
    protected void DrawProperties(SerializedProperty p)
    {
        while (p.NextVisible(false))
        {
            EditorGUILayout.PropertyField(p, true);
        }
    }

    // For get all refer to MapLevel Scriptable Object in the project
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