using System.Xml.Serialization;
using ITHS_mongodb_lab.Interfaces;

namespace ITHS_mongodb_lab;

public class TraditionalTextIO : IStringIO
{
    public void Clear()
    {
        // Behövs inte
    }


    public void Exit()
    {
        System.Environment.Exit(0);
    }


    public string GetString(string typeOfString)
    {
        Console.Write(typeOfString);
        return Console.ReadLine();
    }


    public void PrintString(string output)
    {
        Console.WriteLine(output);
    }


    /// <summary>
    /// Takes a List, shows the list, and lets user choose one item
    /// from the list. The menu choise is returned as an int.
    /// </summary>
    /// <param name="menu">The list of items to make up the menu.</param>
    /// <param name="typeOfList">The string that informs the user what type of list it is.</param>
    /// <returns>Integer representing the menu choise. (Menu choise = menu index - 1)</returns>
    public int ShowMenuAndGetChoise(List<string> menu, string typeOfList)
    {
        int menuChoise;
        int i = 0;
        foreach (var menuItem in menu)
        {
            i++;
            string menuNumber = i.ToString();
            Console.WriteLine($"{menuNumber}. {menuItem}");
        }
        Console.Write("\nPlease choose " + typeOfList + ": ");

        int numOfMenuItems = menu.Count();
        return GetUserInt(1, numOfMenuItems);
    }

    /// <summary>
    /// Takes user input in the specified range.
    /// </summary>
    /// <param name="minAllowed">The min allowed integer to be enteretd.</param>
    /// <param name="maxAllowed">The max allowed integer to be enteretd.</param>
    /// <returns>The user input as integer.</returns>
    private int GetUserInt(int minAllowed, int maxAllowed)
    {
        int inp;
        while (!int.TryParse(Console.ReadLine(), out inp) || inp < minAllowed || inp > maxAllowed)
        {
            Console.Write("\tTry again: ");
        }
        return inp;
    }   
}
