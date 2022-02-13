using appLoginMAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appLoginMAUI.Interfaces
{
    interface ICredit
    {
        Task<ObservableCollection<Credit>> CheckForCredit(string token);
    }
}
