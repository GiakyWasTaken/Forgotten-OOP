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
    

    #endregion

    #region Contrusctor
    public Entity()
	{
        
	}
    #endregion

    #region Public Methods
    public void Move()
    {
        throw new NotImplementedException("Entity Moving not implemented yet");
    }
    #endregion

    
}
