namespace Forgotten_OOP.Items;

#region Using Directives

using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Items.Interfaces;

#endregion

public class Key() : Item("Chiave", "Puoi aprirci stanze chiuse", 0f), IKeyItem
{
    #region Public Methods

    /// <inheritdoc />
    public override void Use(GameManager game)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void Grab(GameManager game)
    {
        throw new NotImplementedException();
    }

    #endregion
}