using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonitaOrganisationData
{
    class MembershipInformation
    {
        public string id { get; set; }
        public int groupId { get; set; }
        public int roleId { get; set; }
        public int userId { get; set; }
        public int active { get; set; }
        public DateTime? assignedDate { get; set; }
        public int assignedBy { get; set; }
    }
}
