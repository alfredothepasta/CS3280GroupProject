using GroupProject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Controller
{
    public class ApplicationController
    {
        // Class that contains data to be passed between windows
        // Contains logic that requires interaction from multiple windows
        // Contains the current list of items
        public int CurrentInvoiceId { get; set; }

        public ApplicationState AppState { get; set; }
        public ApplicationState PreviousState { get; set; }

        public bool ChangesMadeToItemList { get; set; }

        public int SearchInvoiceNumber { get; set; }

        public ApplicationController()
        {
            AppState = ApplicationState.Default;
            ChangesMadeToItemList = false;
        }
        public void UpdateAppState(ApplicationState newState)
        {
            PreviousState = AppState;
            AppState = newState;
        }

        public void RevertState()
        {
            if (PreviousState != null)
            {
                ApplicationState temp = AppState;
                AppState = PreviousState;
                PreviousState = temp;
            } 
        }





    }
}
