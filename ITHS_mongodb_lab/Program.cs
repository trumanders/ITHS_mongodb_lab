using MongoDB.Driver;
using ITHS_mongodb_lab.Interfaces;
namespace ITHS_mongodb_lab;
public class Program
{
    static MongoClient mongoClient;

    public static async Task Main(string[] args)
    {
        IStringIO io;                   // UI
        IItemDAO itemDAO;               // Database Access Object
        InventoryController inventoryController;

        io = new TraditionalTextIO();   // Choose an implementation of IO / UI
        itemDAO = new MongoDAO(Secrets.clusterConnectionString, io);       // Choose an implemenentation of DAO
        inventoryController = new InventoryController(io, itemDAO);
        inventoryController.Start();
    }
}