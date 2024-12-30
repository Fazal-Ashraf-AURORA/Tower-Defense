using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileSlot)), CanEditMultipleObjects]
public class TileSlotEditor : Editor
{
    private GUIStyle centeredStyle;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();

        centeredStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            fontSize = 14
        };

        float oneButtonWidth = (EditorGUIUtility.currentViewWidth - 25);
        float twoButtonWidth = (EditorGUIUtility.currentViewWidth - 25) / 2;
        float threeButtonWidth = (EditorGUIUtility.currentViewWidth - 25) / 3;

        GUILayout.Label("Rotate and Adjust", centeredStyle);

        GUILayout.BeginHorizontal();

        //for Rotating 90deg to the left
        if (GUILayout.Button("Rotate left", GUILayout.Width(twoButtonWidth)))
        {
            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).RotateTile(-1);
            }
        }

        //for Rotating 90deg to the right
        if (GUILayout.Button("Rotate Right", GUILayout.Width(twoButtonWidth)))
        {
            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).RotateTile(1);
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        //for Adjusting Tile - 0.1 in thr Y-axis
        if (GUILayout.Button("- 0.1 on Y", GUILayout.Width(twoButtonWidth)))
        {
            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).AdjustY(-1);
            }
        }

        //for Adjusting Tile + 0.1 in thr Y-axis
        if (GUILayout.Button("+ 0.1 on Y", GUILayout.Width(twoButtonWidth)))
        {
            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).AdjustY(1);
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.Label("Tile Options", centeredStyle);

        GUILayout.BeginHorizontal();

        //for Tile Field
        if(GUILayout.Button("Field", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileField;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        //for Tile Road
        if (GUILayout.Button("Road", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileRoad;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        //for Tile Sideway
        if (GUILayout.Button("Sideway", GUILayout.Width(oneButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileSideway;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.Label("Corner Options", centeredStyle);

        GUILayout.BeginHorizontal();

        //for Tile Inner Corner
        if (GUILayout.Button("Inner Corner", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileInnerCorner;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        //for Tile Outer Corner
        if (GUILayout.Button("Outter Corner", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileOuterCorner;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        //for Tile Inner Corner Small
        if (GUILayout.Button("Inner Corner Small", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileInnerCornerSmall;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        //for Tile Outer Corner Small
        if (GUILayout.Button("Outter Corner Small", GUILayout.Width(twoButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileOuterCornerSmall;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.Label("Bridges and Hills", centeredStyle);

        GUILayout.BeginHorizontal();

        //for Tile Hill 1
        if (GUILayout.Button("Hill 1", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileHill1;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        //for Tile Hill 2
        if (GUILayout.Button("Hill 2", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileHill2;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        //for Tile Hill 3
        if (GUILayout.Button("Hill 3", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileHill3;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        //for Tile Bridge Field
        if (GUILayout.Button("Bridge Field", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileBridgeField;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        //for Tile Bridge Road
        if (GUILayout.Button("Bridge Road", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileBridgeRoad;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        //for Tile Bridge Sideway
        if (GUILayout.Button("Bridge Sideway", GUILayout.Width(threeButtonWidth)))
        {
            GameObject newTile = FindAnyObjectByType<TileSetHolder>().tileBridgeSideway;

            foreach (var targetTile in targets)
            {
                ((TileSlot)targetTile).SwitchTile(newTile);
            }
        }

        GUILayout.EndHorizontal();
    }
}
