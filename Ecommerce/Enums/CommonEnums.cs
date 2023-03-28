using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ecommerce.Enums
{
    public class CommonEnums
    {
        public enum UserType
        {
            [Description("Admin")]
            Admin=1,
            [Description("Customer")]
            Customer=2,
            [Description("Vendor")]
            Vendor=3
        }
    }
}