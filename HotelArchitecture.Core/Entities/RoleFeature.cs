using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
   public class RoleFeature : BaseEntity
    {
        public int Id { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int FeatureId { get; set; }
        public Feature Feature { get; set; }

    }
}
