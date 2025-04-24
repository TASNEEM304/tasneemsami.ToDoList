using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasneemSami.ToDoList.Services.ViewModel
{
    public class ResponseOutput
    {
        public ResponseOutput()
        {
            
        }
        public object Data { get; set; }
        public ResponseStatus status { get; set; }
        public string StatusDescription { get; set; }
    }
    public enum ResponseStatus
    {
        Success=1,
        Error=2,
        Warnin=3,
        Badrequest=400
    }
}
