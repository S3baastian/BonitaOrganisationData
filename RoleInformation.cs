using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonitaOrganisationData
{
    class RoleInformation
    {
        public string id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string dinamicName { get; set; }
        public string description { get; set; }
        public int active { get; set; }
        public DateTime? creationDate { get; set; }
        public int assignedBy { get; set; }
    }
}
