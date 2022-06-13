using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//public class ShortcutsCreatePrefabs : MonoBehaviour
//{
//    [MenuItem("GameObject/2D Object/Sprite Shape/Open Shape Force Zone", false, 10)]
//    static void CreateSpriteForceZone(MenuCommand menuCommand)
//    {
//        // Create a custom game object
//        GameObject go = new GameObject("Open Shape Force Zone");
//        // Ensure it gets reparented if this was a context click (otherwise does nothing)
//        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
//        // Register the creation in the undo system
//        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
//        Selection.activeObject = go;
//    }

//    [MenuItem("GameObject/2D Object/Sprite Shape/Open Shape Force", false, 10)]
//    static void CreateSpriteForce(MenuCommand menuCommand)
//    {
//        // Create a custom game object
//        GameObject go = new GameObject("Open Shape Force");
//        // Ensure it gets reparented if this was a context click (otherwise does nothing)
//        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
//        // Register the creation in the undo system
//        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
//        Selection.activeObject = go;
//    }
//}
