namespace Forgotten_OOP.GameManagers;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Forgotten_OOP.GameManagers.Interfaces;

public class SettingsMenu : ISettingsMenu
{
    public void ChangeEnemyDelay()
    {
        
    }

    public void ChangeMapDimension()
    {
        throw new NotImplementedException();
    }

    public void ChangeMaxWeight()
    {
        throw new NotImplementedException();
    }

    public void ChangeNumItems()
    {
        throw new NotImplementedException();
    }

    public void ChangeNumKeys()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        MainMenu mainMenu = new();
        mainMenu.Show();
    }

    public void Show()
    {
        throw new NotImplementedException();
    }
}
