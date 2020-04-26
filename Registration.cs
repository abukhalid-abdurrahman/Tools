namespace Tools
{
    public class Registration
    {
        public static bool CreateAccount(string _TableName, string _AccountName, string _ColumnName, string _Columns, string _Values, SQLManager _Manager)
        {
            if(Login.isPreviouslyCreated(_AccountName, _ColumnName, _TableName, _Manager))
                return false;
            else
            {
                _Manager.InsertRecord(_TableName, _Columns, _Values);
                return true;
            }
        }
    }
}
