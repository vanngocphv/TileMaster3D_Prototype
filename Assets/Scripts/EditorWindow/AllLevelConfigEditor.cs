using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
    public class AllLevelConfigEditor : EditorWindow
    {
        // object and property
        protected SerializedObject serializedObject;
        protected SerializedProperty serializedProperty;

        // a list scriptable object, map object
        protected TotalMap_SBO[] totalLevels;

        // for drawing a piece of pieces
        protected string selectedPropertyPach;
        protected string selectedProperty;

        // Scroll View
        Vector2 scrollPosTitle = Vector2.zero;
        Vector2 scrollPosDetail = Vector2.zero;

        [MenuItem("Window/Game Data/AllLevel Config")]
        protected static void ShowWindow()
        {
            // show the window
            GetWindow<AllLevelConfigEditor>("AllLevel Config");
        }

        private void OnGUI()
        {
            totalLevels = GetAllInstances<TotalMap_SBO>();
            // for drawing a box selected
            EditorGUILayout.BeginHorizontal();
            // draw the select title
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true)); // A(Selected)   |   context
            scrollPosTitle = EditorGUILayout.BeginScrollView(scrollPosTitle, GUILayout.ExpandHeight(true));
            DrawSlideBar(totalLevels);
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            // draw the content data response with select title
            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
            scrollPosDetail = EditorGUILayout.BeginScrollView(scrollPosDetail, GUILayout.ExpandHeight(true));



            if (selectedProperty != null)
            {
                for (int i = 0; i < totalLevels.Length; i++)
                {
                    if (totalLevels[i].name == selectedProperty)
                    {
                        serializedObject = new SerializedObject(totalLevels[i]);
                        serializedProperty = serializedObject.GetIterator();
                        serializedProperty.NextVisible(true);
                        DrawProperties(serializedProperty);
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("Select an 'All Levels' from the List");
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            Apply();
        }




        // Maps Config (for using outside)
        public string DrawAllLevelsTab(string selected)
        {
            totalLevels = GetAllInstances<TotalMap_SBO>();
            // for drawing a box selected
            EditorGUILayout.BeginHorizontal();
            // draw the select title
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true)); // A(Selected)   |   context
            scrollPosTitle = EditorGUILayout.BeginScrollView(scrollPosTitle, GUILayout.ExpandHeight(true));
            selectedProperty = selected;
            DrawSlideBar(totalLevels);
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            // draw the content data response with select title
            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
            scrollPosDetail = EditorGUILayout.BeginScrollView(scrollPosDetail, GUILayout.ExpandHeight(true));

            if (selectedProperty != null)
            {
                for (int i = 0; i < totalLevels.Length; i++)
                {
                    if (totalLevels[i].name == selectedProperty)
                    {
                        serializedObject = new SerializedObject(totalLevels[i]);
                        serializedProperty = serializedObject.GetIterator();
                        serializedProperty.NextVisible(true);
                        DrawProperties(serializedProperty);
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("Select an 'All Levels' from the List");
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            Apply();
            return selectedProperty;
        }


        // For the title of all Maps
        protected void DrawSlideBar(TotalMap_SBO[] allLevels)
        {
            foreach (TotalMap_SBO allLevel in allLevels)
            {
                if (GUILayout.Button(allLevel.name))
                {
                    selectedPropertyPach = allLevel.name;
                }
            }

            // if not empty or null
            if (!string.IsNullOrEmpty(selectedPropertyPach))
            {
                selectedProperty = selectedPropertyPach;
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
        public static T[] GetAllInstances<T>() where T : TotalMap_SBO
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