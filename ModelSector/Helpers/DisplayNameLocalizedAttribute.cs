using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ModelSector.Helpers
{
    public class DisplayNameLocalizedAttribute : DisplayNameAttribute
    {
        private readonly string m_ResourceName;
        private readonly string m_ClassName;

        public DisplayNameLocalizedAttribute(string className, string resourceName)
        {
            m_ResourceName = resourceName;
            m_ClassName = className;
        }

        public override string DisplayName
        {
            get
            {
                // get and return the resource object
                return HttpContext.GetGlobalResourceObject(
                       m_ClassName,
                       m_ResourceName,
                       Thread.CurrentThread.CurrentCulture).ToString();
            }
        }
    }
}
