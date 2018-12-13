using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupChatApp.Models
{
    public class NewGroupViewModel
    {
        public string GroupName { get; set; }
        public List<string> UserNames { get; set; }
    }
}
