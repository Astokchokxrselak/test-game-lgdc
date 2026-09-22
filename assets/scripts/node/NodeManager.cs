
using Godot;
using System.Collections.Generic;
using System.Linq;
using System;


public interface IEntity
{
    public abstract void Initialize();
}

public static class NodeManager
{
    private static void InitializeNodes()
    {
        var tree = GameManager.Singleton;
        tree.FindChildren("*").OfType<IEntity>().ToList().ForEach(character =>
        {
            character.Initialize();
        });
    }
    public static void Initialize()
    {
        InitializeNodes();
    }
}
