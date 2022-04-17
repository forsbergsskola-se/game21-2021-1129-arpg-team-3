// //C# Example (LookAtPointEditor.cs)
// using UnityEngine;
// using UnityEditor;
//
// [CustomEditor(typeof(WayPointDebug_ML))]
// //[CanEditMultipleObjects]
// public class WaypointDebugEditor_ML : Editor 
// {
//     SerializedProperty lookAtPoint;
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
//         WayPointDebug_ML myScript = (WayPointDebug_ML)target;
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