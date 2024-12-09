using System;
using System.Collections;
using System.Collections.Generic;

public static class SceneChanger
{
    public static Queue<int> _sceneID = new();

    public static void AddSceneID(int id)
    {
        _sceneID.Enqueue(id);
    }

    public static int TakeSceneID()
    {
        return _sceneID.Dequeue();
    }
}
