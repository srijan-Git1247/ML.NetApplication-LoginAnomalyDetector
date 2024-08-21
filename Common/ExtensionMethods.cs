using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAnomalyDetector.Common
{
    public static class ExtensionMethods //Returns all of the properities in a class(objType) except the label
    {
        public static string[] ToPropertyList<T>(this Type objType, string labelName) => objType.GetProperties().Where(a => a.Name != labelName).Select(a => a.Name).ToArray();
    }
}
