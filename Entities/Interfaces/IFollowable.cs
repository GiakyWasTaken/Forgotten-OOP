namespace Forgotten_OOP.Entities.Interfaces;

using Forgotten_OOP.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal interface IFollowable
{
    /// <summary>
    /// Gets or sets the list of entities following the player
    /// </summary>
    public List<Entity> FollowingEntities { get; set; }
}

