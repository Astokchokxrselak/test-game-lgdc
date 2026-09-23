
using Godot;
using System.Collections.Generic;
using System.Linq;
using System;



// All entities that initialize at the first frame of gameplay should inherit from IEntity.
public interface IEntity
{
    public abstract void Initialize();
}

// Entities that initialize at the first frame of gameplay but not necessarily before others should inherit from ILowPriorityEntity.
public interface ILowPriorityEntity : IEntity
{
}

// Entities that initialize before other entities should inherit from IHighPriorityEntity.
public interface IHighPriorityEntity : IEntity
{
}


// The NodeManager is responsible for managing and initializing all nodes in the scene.
public static class NodeManager
{
    private static void InitializeNodes()
    {
        var tree = GameManager.Singleton.FindChildren("*");
        tree.OfType<IHighPriorityEntity>().ToList().ForEach(character =>
        {
            character.Initialize();
        });
        tree.OfType<ILowPriorityEntity>().ToList().ForEach(character =>
        {
            character.Initialize();
        });
    }
    public static void Initialize()
    {
        InitializeNodes();
    }
}
