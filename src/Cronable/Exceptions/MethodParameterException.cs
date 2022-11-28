using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cronable
{
    public class MethodParameterException : Exception
    {
        public MethodParameterException(MethodInfo method) : base($"{method.Name} cron method connot take a parameter")
        {

        }
    }
}
