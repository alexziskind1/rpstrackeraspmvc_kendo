using RPS.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS.Web.Models.ViewModels
{
    public class PtActiveIssuesVm
    {
        public PtDashboardStatusCounts StatusCounts { get; set; }
        public int IssueCountOpen { get; set; }
        public int IssueCountClosed { get; set; }

        public int IssueCountActive { get { return IssueCountOpen + IssueCountClosed; } }
        public decimal IssueCloseRate
        {
            get
            {
                if (IssueCountActive == 0)
                {
                    return 0m;
                }
                return Math.Round((decimal)IssueCountClosed / (decimal)IssueCountActive * 100m, 2);
            }
        }

        public PtActiveIssuesVm(PtDashboardStatusCounts statusCounts)
        {
            StatusCounts = statusCounts;

            IssueCountOpen = statusCounts.OpenItemsCount;
            IssueCountClosed = statusCounts.ClosedItemsCount;
        }
    }
}
