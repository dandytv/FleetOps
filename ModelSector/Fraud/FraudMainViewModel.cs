using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSector.Fraud
{
    public class FraudMainViewModel
    {
        public FraudMainViewModel()
        {

            if (this.FraudCardDetailsViewModel == null)
                this.FraudCardDetailsViewModel = new FraudCardDetailsViewModel();

            if (this.FraudCustomerDetailsViewModel == null)
                this.FraudCustomerDetailsViewModel = new FraudCustomerDetailsViewModel();

            if (this.FraudIncidentsViewModel == null)
                this.FraudIncidentsViewModel = new FraudIncidentsViewModel();
        }
        public FraudCustomerDetailsViewModel FraudCustomerDetailsViewModel { get; set; }
        public FraudCardDetailsViewModel FraudCardDetailsViewModel { get; set; }
        public FraudIncidentsViewModel FraudIncidentsViewModel { get; set; }
    }
}
