namespace Forgotten_OOP.Entities;

#region Using Directives

using Forgotten_OOP.Injectables;
using Forgotten_OOP.Injectables.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Forgotten_OOP.Enums;
using Forgotten_OOP.Mapping;
#endregion



public class Entity
{
    #region Properties

    public string Name { get; private set; }

    public Room CurrentRoom { get; private set; }

    #endregion

    #region Contrusctor
    public Entity( string name, Room currentRoom)
	{
        Name= name;
        CurrentRoom = currentRoom;
	}
    #endregion

    #region Public Methods
    public void Move()
    {
        throw new NotImplementedException("Entity Moving not implemented yet");
    }

    public void Teleport()
    {
        throw new NotImplementedException("Entity Teleport not implemented yet");
    }
    #endregion

    
}
