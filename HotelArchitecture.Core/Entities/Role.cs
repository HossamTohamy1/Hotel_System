using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
   public class Role : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<RoleFeature> RoleFeatures { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
