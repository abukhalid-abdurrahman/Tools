namespace Tools
{
    public class Login
    {
        public static bool isPreviouslyCreated(string _Name, string _ColumnName, string _TableName, SQLManager _sqlManager)
        {
            if(_sqlManager.Select(_TableName, _ColumnName, $"{_ColumnName} = {_Name}").Count > 0)
                return true;
            else
                return false;
        }
        public bool SignIn(string _Name, string _Password, string _ColumnName, string _ColumnPassword, string _TableName, SQLManager _sqlManager)
        {
            if(_sqlManager.Select(_TableName, $"{_ColumnName}, {_ColumnPassword}", $"({_ColumnName} = {_Name} and {_ColumnPassword} = {_Password})").Count > 0)
                return true;
            else
                return false;
        }
    }
}
