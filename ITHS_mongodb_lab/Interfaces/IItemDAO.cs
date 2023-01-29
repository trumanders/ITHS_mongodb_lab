using MongoDB.Bson;
using MongoDB.Driver;

namespace ITHS_mongodb_lab.Interfaces;


/// <summary>
/// Create a DAO from an ODM object. The ItemODM-object that was created from
/// the database Bson-document is passed in to the CRUD-methods.
/// </summary>
public interface IItemDAO
{

    // CRUD
    public void CreateItem(ItemODM item);
    public List<ItemODM> ReadAllItems();
    public void UpdateItem(FilterDefinition<ItemODM> filter, UpdateDefinition<ItemODM> update);
    public void DeleteItem(ObjectId id);
}

