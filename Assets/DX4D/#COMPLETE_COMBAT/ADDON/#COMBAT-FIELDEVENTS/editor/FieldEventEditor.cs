using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldEvent))]
public class FieldEventEditor : Editor
{
    private Editor _fieldEditor;
    private Editor _vfxEditor;
    private Editor _damageEditor;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.BeginVertical();
        {
            //DEFAULT INSPECTOR
            GUILayout.BeginVertical();
            {
                DrawDefaultInspector();
            }
            GUILayout.EndVertical();

            //var myAsset = serializedObject.FindProperty("path");
            //CreateCachedEditor(myAsset.objectReferenceValue, null, ref _editor);

            //DAMAGE
            //var damage = serializedObject.FindProperty("damage");
            GUILayout.BeginVertical("box");
            {
                CreateCachedEditor(serializedObject.FindProperty("field").objectReferenceValue, null, ref _fieldEditor);
                if (_fieldEditor != null) _fieldEditor.OnInspectorGUI();
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            {
                CreateCachedEditor(serializedObject.FindProperty("vfx").objectReferenceValue, null, ref _vfxEditor);
                if (_vfxEditor != null) _vfxEditor.OnInspectorGUI();
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical("box");
            {
                CreateCachedEditor(serializedObject.FindProperty("damage").objectReferenceValue, null, ref _damageEditor);
                if (_damageEditor != null) _damageEditor.OnInspectorGUI();
            }
            GUILayout.EndVertical();
            //PATH
            //var path = serializedObject.FindProperty("path");
            //if (path != null)
            //{
            //GUILayout.BeginVertical("box");
            //{
            //    CreateCachedEditor(serializedObject.FindProperty("path").objectReferenceValue, null, ref _pathEditor);
            //    if (_pathEditor != null) _pathEditor.OnInspectorGUI();
            //}
            //GUILayout.EndVertical();
            //}

            //VFX
            //var vfx = serializedObject.FindProperty("vfx");
            //if (vfx != null)
            //{
            //GUILayout.BeginVertical("box");
            //{
            //    CreateCachedEditor(serializedObject.FindProperty("vfx").objectReferenceValue, null, ref _vfxEditor);
            //    if (_vfxEditor != null) _vfxEditor.OnInspectorGUI();
            //}
            //GUILayout.EndVertical();
            //}
        }
        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}