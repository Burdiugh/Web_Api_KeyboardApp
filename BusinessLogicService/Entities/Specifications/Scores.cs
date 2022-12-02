using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Specifications
{
    public static class Scores
    {

        public class ByUserId : Specification<AppScore>
        {
            public ByUserId(string id)
            {
                Query
                    .Where(x => x.AppUserId == id);
            }
        }

    }
}
