namespace WordenaServerEF.CommandProcessing
{
    public interface ICommand
    {
        string LogIn(string data);
        string NewUser(string data);
        string NewChar(string data);
        string RemoveChar(string data);
        string StartGame(string data);
        string CatchResults(string data);
        string GetResult(string data);

    }
}
