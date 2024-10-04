using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class DistrictCreateDto
    {
        public Guid ProvinceId { get; set; }

        public string Name { get; set; }
    }
}
