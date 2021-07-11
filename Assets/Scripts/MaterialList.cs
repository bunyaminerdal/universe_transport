using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planet", menuName = "Mat/MatList")]
public class MaterialList : ScriptableObject
{
    public Material[] listOfMaterial;

    public Dictionary<Material, float> deneme;


}
