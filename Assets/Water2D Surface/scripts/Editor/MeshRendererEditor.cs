using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using System.Reflection;
using System;

/// <summary>
/// 
/// </summary>
/// <remarks>Source: http://forum.unity3d.com/threads/drawing-order-of-meshes-and-sprites.212006/ </remarks>
[CanEditMultipleObjects]
[CustomEditor(typeof(MeshRenderer))]
public class MeshRendererEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();
    serializedObject.Update();
    SerializedProperty sortingLayerID = serializedObject.FindProperty("m_SortingLayerID");
    SerializedProperty sortingOrder = serializedObject.FindProperty("m_SortingOrder");
    //MeshRenderer renderer = target as MeshRenderer;
    Rect firstHoriz = EditorGUILayout.BeginHorizontal();
    EditorGUI.BeginChangeCheck();
    EditorGUI.BeginProperty(firstHoriz, GUIContent.none, sortingLayerID);
    string[] layerNames = GetSortingLayerNames();
    int[] layerID = GetSortingLayerUniqueIDs();
    int selected = -1;
    int sID = sortingLayerID.intValue;
    for (int i = 0; i < layerID.Length; i++)
      if (sID == layerID[i])
        selected = i;
    if (selected == -1)
      for (int i = 0; i < layerID.Length; i++)
        if (layerID[i] == 0)
          selected = i;
    selected = EditorGUILayout.Popup("Sorting Layer", selected, layerNames);
    sortingLayerID.intValue = layerID[selected];
    EditorGUI.EndProperty();
    EditorGUILayout.EndHorizontal();
    EditorGUILayout.BeginHorizontal();
    EditorGUI.BeginChangeCheck();
    EditorGUILayout.PropertyField(sortingOrder, new GUIContent("Order in Layer"));
    EditorGUILayout.EndHorizontal();
    serializedObject.ApplyModifiedProperties();
  }
  public string[] GetSortingLayerNames()
  {
    Type internalEditorUtilityType = typeof(InternalEditorUtility);
    PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
    return (string[])sortingLayersProperty.GetValue(null, new object[0]);
  }
  public int[] GetSortingLayerUniqueIDs()
  {
    Type internalEditorUtilityType = typeof(InternalEditorUtility);
    PropertyInfo sortingLayerUniqueIDsProperty = internalEditorUtilityType.GetProperty("sortingLayerUniqueIDs", BindingFlags.Static | BindingFlags.NonPublic);
    return (int[])sortingLayerUniqueIDsProperty.GetValue(null, new object[0]);
  }
}