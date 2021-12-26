using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.interfaces
{
    public interface IStartupService
    {
        Task Initialize();
    }
}
