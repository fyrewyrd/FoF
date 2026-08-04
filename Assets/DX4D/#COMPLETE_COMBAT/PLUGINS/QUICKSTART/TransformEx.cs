//FIND CHILD BY NAME
//Searches an object's children recursively to find the transform of a named object.
//ADAPTED FROM: https://www.loekvandenouweland.com/content/unity-find-gameobject-in-hierarchy.html
using System;
using UnityEngine;

public static class TransformEx
{
    //public static Transform FirstOrDefault(this Transform transform, Func<Transform, bool> query)
    /// <summary>Searches an object's children recursively to find the transform of a named object.</summary>
    public static Transform FindChildByName(this Transform transform, string nameToFind)
    {
        Func<Transform, bool> query = (x => x.name == nameToFind);
        if (query(transform))
        {
            return transform;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform result = FindChildByName(transform.GetChild(i), nameToFind);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
    /*
    //
    //FROM: https://www.loekvandenouweland.com/content/finding-gameobjects-and-transforms-recursively-linq-style.html
    /// <summary>Searches through a Game Object's heirarchy to find </summary>
    /// <param name="query">FIND BY NAME: (x => x.name == "d")</param>
    public static Transform FirstChildOrDefault(this Transform parent, Func<Transform, bool> query)
    {
        if (parent.childCount == 0)
        {
            return null;
        }

        Transform result = null;
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            if (query(child))
            {
                return child;
            }
            result = FirstChildOrDefault(child, query);
        }

        return result;
    }
    */
}