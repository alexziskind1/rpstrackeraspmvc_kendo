using RPS.Core.Models;
using RPS.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPS.Web.Models.ViewModels
{
    public class PtDashboardVm
    {

        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

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

        public List<DateTime> Categories { get; set; }

        public List<int> ItemsOpenByMonth { get; set; }
        public List<int> ItemsClosedByMonth { get; set; }


        public int? SelectedAssigneeId { get; set; }
        public List<PtUser> Assignees { get; set; }

        public PtDashboardVm(PtDashboardStatusCounts statusCounts, PtDashboardFilteredIssues filteredIssues, List<PtUser> users, int? userId )
        {
            IssueCountOpen = statusCounts.OpenItemsCount;
            IssueCountClosed = statusCounts.ClosedItemsCount;

            ItemsOpenByMonth = new List<int>();
            ItemsClosedByMonth = new List<int>();
            Categories = new List<DateTime>();

            filteredIssues.MonthItems.ForEach(i => {
                ItemsOpenByMonth.Add(i.Open.Count);
                ItemsClosedByMonth.Add(i.Closed.Count);
            });
            Categories = filteredIssues.Categories;

            Assignees = users;
            if (userId.HasValue)
                SelectedAssigneeId = userId.Value;
        }
    }
}