using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Extension methods for the MonoBehaviour class
/// </summary>
public static class MonoBehaviourExtensions
{

    /// <summary>
    /// Allows retrieving a child component by a specific name and type
    /// </summary>
    /// <param name="gameObject">The game object to call this on</param>
    /// <param name="name">The name of the component to get</param>
    /// <param name="type">The type fo the component to get</param>
    /// <returns>The first component that matches by name and type, if it exists</returns>
    public static Component GetChildComponentByName (this GameObject gameObject, string name, Type type)
        => gameObject.GetComponentsInChildren(type).FirstOrDefault(child => child.name == name);

}
