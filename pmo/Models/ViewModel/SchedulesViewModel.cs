using dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class SchedulesViewModel : Schedule
    {
        public string ViewModelDate 
        { 
            get { 
                return Date.ToString("dd/MM/yyyy"); 
            } set {
                Date = DateTime.Parse(value);                    
            } 
        }
    }
}
