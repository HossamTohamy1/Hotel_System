using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Feature_Role
{
    public class UpdateFeatureDTO
    {
        public string RoleName { get; set; }
        public string OldFeatureName { get; set; }
        public string NewFeatureName { get; set; }
    }
}
