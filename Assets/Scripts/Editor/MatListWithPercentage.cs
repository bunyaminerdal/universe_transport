using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
class MatListWithPercentage : EditorWindow
{

    [MenuItem("MaterialList/Material List With Percentage")]
    static void ShowWindow()
    {
        GetWindow<MatListWithPercentage>("Material List With Percentage");
    }
    List<float> percentages = new List<float>();
    List<Material> materials = new List<Material>();
    Material mat;
    Editor matEditor;

    float percentage;
    Vector2 scrollPos;

    void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        percentage = EditorGUILayout.FloatField("Percentage: ", percentage);

        mat = EditorGUILayout.ObjectField(mat, typeof(Material), true) as Material;
        EditorGUILayout.Separator();
        if (mat != null)
        {

            matEditor = Editor.CreateEditor(mat);

            matEditor.OnPreviewGUI(GUILayoutUtility.GetRect(50, 50), EditorStyles.whiteLabel);
        }
        if (GUILayout.Button("ADD"))
        {
            if (mat != null && percentage > 0)
            {
                if (!materials.Contains(mat))
                {
                    percentages.Add(percentage);
                    materials.Add(mat);
                }
                else
                {
                    Debug.Log("The material already exists!");
                }
            }
            else
            {
                Debug.Log("select a material or percentage must be grather then '0' ");
            }
        }
        EditorGUILayout.Separator();

        if (percentages.Count > 0)
        {

            for (int i = 0; i < percentages.Count; i++)
            {
                Editor mEditor = Editor.CreateEditor(materials[i]);
                mEditor.OnPreviewGUI(GUILayoutUtility.GetRect(50, 50), EditorStyles.whiteLabel);
                percentages[i] = EditorGUILayout.FloatField("Percentage: ", percentages[i]);
                //GUILayout.TextField(matDic[materials[i]].ToString());
                if (GUILayout.Button("Remove: " + materials[i].name))
                {
                    percentages.RemoveAt(i);
                    materials.RemoveAt(i);
                    i--;
                }
                EditorGUILayout.Separator();
            }
        }

        EditorGUILayout.Separator();

        if (GUILayout.Button("Calculate Percentage"))
        {
            float sumofpercentage = 0;
            foreach (var item in percentages)
            {
                sumofpercentage += item;
            }
            for (int i = 0; i < percentages.Count; i++)
            {

                percentages[i] *= 100f / sumofpercentage;
            }

        }
        if (GUILayout.Button("Create Material List"))
        {

            MaterialList asset = ScriptableObject.CreateInstance<MaterialList>();

            asset.percentages = percentages.ToArray();
            asset.listOfMaterial = materials.ToArray();
            AssetDatabase.CreateAsset(asset, "Assets/NewScripableObject.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
        EditorGUILayout.EndScrollView();

    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }
}