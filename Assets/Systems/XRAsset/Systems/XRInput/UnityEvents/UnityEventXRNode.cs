using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class UnityEventXRNode<T> : UnityEvent<XRNode, T> where T : struct { }
