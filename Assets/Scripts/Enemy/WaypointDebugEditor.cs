// //C# Example (LookAtPointEditor.cs)
// using UnityEngine;
// using UnityEditor;
//
// [CustomEditor(typeof(WayPointDebug))]
// //[CanEditMultipleObjects]
// public class WaypointDebugEditor : Editor 
// {
//    // SerializedProperty lookAtPoint;
//     
//     void OnEnable()
//     {
//     //    lookAtPoint = serializedObject.FindProperty("lookAtPoint");
//     }
//
//     public override void OnInspectorGUI()
//     {
//         DrawDefaultInspector();
//         
//         WayPointDebug myScript = (WayPointDebug)target;
//         if(GUILayout.Button("Add Node"))
//         {
//             myScript.AddOnePoint();
//         }
//         
//         if(GUILayout.Button("Remove Node"))
//         {
//             myScript.RemoveOnePoint();
//         }
//         
//         //  serializedObject.Update();
//         //  EditorGUILayout.PropertyField(lookAtPoint);
//         //  serializedObject.ApplyModifiedProperties();
//     }
// }