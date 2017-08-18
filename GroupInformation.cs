using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonitaOrganisationData
{
    class GroupInformation
    {
        public string id {get; set;}
        public string groupName {get; set;}
        public string icon { get; set; }
        public string dinamicTitle { get; set; }
        public string parentName { get; set; }
        public string parentId { get; set; }
        public string description { get; set; }
        public int active { get; set; }
        public DateTime? creationDate { get; set; }
        public DateTime? updateDate { get; set; }
        public int assignedBy { get; set; }

    }
}
