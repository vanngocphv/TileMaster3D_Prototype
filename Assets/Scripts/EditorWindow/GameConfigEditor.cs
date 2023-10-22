using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
    public class GameConfigEditor : EditorWindow
    {
        public int tabs = 0;
        string[] tabOpts = new string[] { "Tile Config", "Map Config", "All Maps" };

        TileConfigEditor tileConfig;
        MapConfigEditor mapConfig;
        AllLevelConfigEditor allLevelConfig;

        string previousSelected;
        string currentSelected;

        [MenuItem("Window/Game Config/Setting")]
        public static void ShowWindow()
        {
            GameConfigEditor configEditor = (GameConfigEditor) GetWindow(typeof(GameConfigEditor));
            configEditor.minSize = new UnityEngine.Vector2(300, 200);
            //configEditor.maxSize = new UnityEngine.Vector2(300, 200);

        }

        private void OnGUI()
        {
            tileConfig = TileConfigEditor.CreateInstance<TileConfigEditor>();
            mapConfig = MapConfigEditor.CreateInstance<MapConfigEditor>();
            allLevelConfig = AllLevelConfigEditor.CreateInstance<AllLevelConfigEditor>();
        
            tabs = GUILayout.Toolbar(tabs, tabOpts);

            // switch tabs
            switch (tabs)
            {
                case 0:
                    TileConfig();
                    break;
                case 1:
                    MapConfig();
                    break;
                case 2:
                    AllMapConfig();
                    break;
            }
        }

        private void TileConfig()
        {

            currentSelected = tileConfig.DrawTilesTab(previousSelected);
            if (currentSelected != null && currentSelected != previousSelected)
            {
                previousSelected = currentSelected;
            }
        }

        private void MapConfig()
        {
            currentSelected = mapConfig.DrawMapsTab(previousSelected);
            if (currentSelected != null && currentSelected != previousSelected)
            {
                previousSelected = currentSelected;
            }
        }

        private void AllMapConfig()
        {
            currentSelected = allLevelConfig.DrawAllLevelsTab(previousSelected);
            if (currentSelected != null && currentSelected != previousSelected)
            {
                previousSelected = currentSelected;
            }
        }

    }
#endif