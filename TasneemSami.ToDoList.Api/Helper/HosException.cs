namespace TasneemSami.ToDoList.Api.Helper
{
    public interface IHosException
    {
        string Exception(Exception ex);
    }
    public class HosException : IHosException
    {
        public string Exception(Exception ex)
        {
            if (ex.GetType().Name == "DbUpdateException")
            {
                Microsoft.EntityFrameworkCore.DbUpdateException e=(Microsoft.EntityFrameworkCore.DbUpdateException)ex;
                return GetDbExc(e);
             }
            else
            {
                return ex.Message;
            }
            return null;
        }
        private static string  GetDbExc(Exception ex)
        {

            string message=ex.GetBaseException().Message;
            string arrmsg = message.Substring(message.IndexOf('o'), message.IndexOf(':')).Trim();
            int start = message.IndexOf("(") + 1;
            int end=message.IndexOf(")");
            if(start!=0&&end!=0)
            {
                string constraintName= message.Substring(message.IndexOf("("), end-start).Trim();
                constraintName = constraintName + "_" + arrmsg;
                return constraintName;
            }
            else
            {
                return arrmsg;
            }

        }   
    }
}   
