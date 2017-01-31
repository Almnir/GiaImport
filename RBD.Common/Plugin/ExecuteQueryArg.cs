using System;
using System.Collections.Generic;

namespace RBD.Common.Plugin
{
    public class ExecuteQueryArg
    {
        public  ExecuteQueryArg()
        {
            ParamQuery = new Dictionary<string, object>();
        }
        
        public string Query { get; set;}
        public Type ReturnType { get; set; }
        public Dictionary<string, object> ParamQuery { get; set; }
    }
}