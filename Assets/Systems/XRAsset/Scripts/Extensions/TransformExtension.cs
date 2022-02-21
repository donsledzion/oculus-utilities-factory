using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{

    /// <summary>
    /// Zwraca wszystkie dzieci danego transformu
    /// </summary>
    /// <returns>Tablica znalezionych dzieci</returns>
    public static Transform[] GetAllChildrenInHierarchy(this Transform transform)
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in transform)
        {
            children.Add(child);

            if (child.childCount > 0)
            {
                Transform[] childrenChildren = GetAllChildrenInHierarchy(child);
                children.AddRange(childrenChildren);
            }
        }

        return children.ToArray();
    }

    public static void SetPositionAndRotation(this Transform transform, Transform setToTransform)
    {
        transform.SetPositionAndRotation(setToTransform.position, setToTransform.rotation);
    }
}
