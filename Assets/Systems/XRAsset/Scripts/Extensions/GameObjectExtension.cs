using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    /// <summary>
    /// Funkcja sprawdza, czy layer gameobjectu znajduje siê na layerMask
    /// </summary>
    /// <param name="layerMask">layermask z wybranymi warstwami</param>
    /// <returns>Zwraca true, je¿eli siê znajduje</returns>
    public static bool InOnLayerMask(this GameObject gameObject, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << gameObject.layer));
    }
}
