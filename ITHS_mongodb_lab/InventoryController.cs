using ITHS_mongodb_lab.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data;

namespace ITHS_mongodb_lab;


/// <summary>
/// Displays instruction to user and takes in user input
/// </summary>
public class InventoryController
{
    IStringIO io;
    IItemDAO itemDAO;

    public InventoryController(IStringIO io, IItemDAO itemDAO)
    {
        this.io = io;
        this.itemDAO = itemDAO;
    }

    /// <summary>
    /// Presents a menu with a title, and a list of 4 CRUD-actions
    /// to choose from. 
    /// </summary>
    public void Start()
    {
        do
        {
            switch (Menu())
            {
                case 1:
                    // create new ODM
                    itemDAO.CreateItem(CreateItemODM());
                    break;
                case 2:
                    // Read all items
                    Console.WriteLine();
                    GetAllFullNames().ForEach(name => io.PrintString(name));
                    break;
                case 3:
                    // update
                    UpdateItem();
                    break;
                case 4:
                    // delete
                    DeleteItem();
                    break;
            }
        } while (true);
    }


    private int Menu()
    {
        io.PrintString("\n\nDatabase CRUD - program! \n");
        // Send a menu as a List to the menu method. The type of list is "CRUD-method"
        return io.ShowMenuAndGetChoise(new List<string> { "Create", "Read", "Update", "Delete" }, "CRUD-method");
    }

    /// <summary>
    /// Asks user to input name values.
    /// </summary>
    /// <returns>One ItemODM object.</returns>
    private ItemODM CreateItemODM()
    {
        return new ItemODM
        {
            FirstName = io.GetString("First name: "),
            LastName = io.GetString("Last name: "),
            MiddleName = io.GetString("Middle name: "),
            //BirthYear = Convert.ToInt32(io.GetString("Birth year: ")),
            //DeathYear = Convert.ToInt32(io.GetString("Death year: "))
        };
    }

    /// <summary>
    /// Calls DAO Update-method after asking the user to choose which document and what field in the
    /// document to update.
    /// </summary>
    private void UpdateItem()
    {
        // Create List of all items     
        var allItems = itemDAO.ReadAllItems();        

        // Show a list of all items with names, and let user choose which entry to update. Save choise as index.
        int indexToUpdate = io.ShowMenuAndGetChoise(GetAllFullNames(), "Choose document to update: ") - 1;
        
        // Create a list of available variables to update
        var listOfVariables = new List<string> { "firstName", "lastName", "middleName" };

        // Show list of variables and let user choose what variable to update.
        string varableToUpdate = listOfVariables[io.ShowMenuAndGetChoise(listOfVariables, "Choose what variable to update: ") - 1];

        // Create filer that matches the firstName of the chosen document via the index. 
        var filter = Builders<ItemODM>.Filter.Eq("firstName", itemDAO.ReadAllItems()[indexToUpdate].FirstName);           
        var update = Builders<ItemODM>.Update.Set(varableToUpdate, io.GetString("Enter new value: "));
        //Console.WriteLine("dbItemIndex: " + databaseDocumentIndex + "\nvarToUpd: " + varableToUpdate + "\n");
        itemDAO.UpdateItem(filter, update);
    }

    /// <summary>
    /// Calls the io-method to let user choose from a list of documents. That index
    /// is uset to get the docuement's ObjectId, and is passed to the DAO delete-method.
    /// </summary>
    private void DeleteItem()
    {
        // Show list with names and let user choose which to delete
        var indexToDelete = io.ShowMenuAndGetChoise(GetAllFullNames(), "Choose which to delete: ") - 1;

        // Pass in the id of the document at the chosen index
        var objectID = itemDAO.ReadAllItems()[indexToDelete].Id;
        itemDAO.DeleteItem(objectID);
    }

    /// <summary>
    /// Helper method to get and format all names as a list
    /// </summary>
    /// <returns>List<string> of all names.</returns>
    private List<string> GetAllFullNames()
    {
        return itemDAO.ReadAllItems().Select(item => $"{item.FirstName,-12} {item.MiddleName,-12} {item.LastName,-17}").ToList();
    }
}
