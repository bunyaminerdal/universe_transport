using System.Collections.Generic;
using UnityEngine;

public static class StaticClasses
{
    private static System.Random rng = new System.Random();

    public static void ShuffleList<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public static Transform Clear(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        return transform;
    }
    public static Transform ShowAll(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        return transform;
    }
    public static Transform HideAll(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        return transform;
    }
    public static List<Material> CreateMatList(this MaterialList matlist, int count)
    {
        float matCount = 0;
        float[] percentage = new float[matlist.percentages.Length];
        for (int i = 0; i < matlist.percentages.Length; i++)
        {
            percentage[i] = matlist.percentages[i];
            matCount += percentage[i];
        }

        float solarcountdividematcount = count / matCount;

        for (int i = 0; i < percentage.Length; i++)
        {
            percentage[i] *= solarcountdividematcount;
        }

        List<Material> tempMatList = new List<Material>();
        for (int i = 0; i < percentage.Length; i++)
        {
            var tempMat = matlist.listOfMaterial[i];

            for (int j = 0; j < Mathf.RoundToInt(percentage[i]); j++)
            {
                tempMatList.Add(tempMat);
            }
        }

        if (count > tempMatList.Count)
        {
            int diff = count - tempMatList.Count;
            for (int i = 0; i < diff; i++)
            {
                tempMatList.Add(matlist.listOfMaterial[0]);
            }
        }

        return tempMatList;
    }

}
