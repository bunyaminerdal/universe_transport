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
    Dictionary<Material, float> matDic = new Dictionary<Material, float>();
    List<Material> materials = new List<Material>();
    Material mat;
    Editor matEditor;

    float percentage;

    void OnGUI()
    {

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
                    matDic.Add(mat, percentage);
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

        if (matDic.Count > 0)
        {

            for (int i = 0; i < matDic.Count; i++)
            {
                Editor mEditor = Editor.CreateEditor(materials[i]);
                mEditor.OnPreviewGUI(GUILayoutUtility.GetRect(50, 50), EditorStyles.whiteLabel);
                matDic[materials[i]] = EditorGUILayout.FloatField("Percentage: ", matDic[materials[i]]);
                //GUILayout.TextField(matDic[materials[i]].ToString());
                if (GUILayout.Button("Remove: " + materials[i].name))
                {
                    matDic.Remove(materials[i]);
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
            foreach (var item in matDic)
            {
                sumofpercentage += item.Value;
            }
            for (int i = 0; i < matDic.Count; i++)
            {
                float temppercentage = matDic[materials[i]];
                matDic[materials[i]] = temppercentage * 100f / sumofpercentage;
            }

        }
        if (GUILayout.Button("Create Material List"))
        {

            MaterialList asset = ScriptableObject.CreateInstance<MaterialList>();
            asset.deneme = matDic;
            asset.listOfMaterial = materials.ToArray();
            AssetDatabase.CreateAsset(asset, "Assets/NewScripableObject.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }
}