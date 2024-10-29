using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Domain.Constant
{
    public enum OrderStatus:byte
    {
        UnderConfirm,
        Confirm,
        Cancelled,
        Delivered
    }
}
