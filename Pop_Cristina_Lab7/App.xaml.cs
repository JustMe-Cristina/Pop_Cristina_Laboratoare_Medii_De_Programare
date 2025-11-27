using System;
using System.IO;
using Pop_Cristina_Lab8.Data;

namespace Pop_Cristina_Lab8;

public partial class App : Application
{
    static ShoppingListDatabase database;

    public static ShoppingListDatabase Database
    {
        get
        {
            if (database == null)
            {
                database = new ShoppingListDatabase(
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "ShoppingList.db3"));
            }
            return database;
        }
    }

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }
}