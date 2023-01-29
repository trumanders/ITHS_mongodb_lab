namespace ITHS_mongodb_lab.Interfaces;

public interface IStringIO
{
    public string GetString(string typeOfString);
    public void PrintString(string input);
    public void Clear();
    public void Exit();

    public int ShowMenuAndGetChoise(List<string> choise, string typeOfList);

}
