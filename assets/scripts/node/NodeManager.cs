
using Godot;
using System.Collections.Generic;
using System.Linq;
using System;


// All entities that initialize at the first frame of gameplay should inherit from IEntity.
public interface IEntity
{
    public abstract void Initialize();
}


// The NodeManager is responsible for managing and initializing all nodes in the scene.
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
