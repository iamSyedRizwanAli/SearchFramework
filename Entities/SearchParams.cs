using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SearchParams
    {
        public String ConditionalOperator {get; set; }
        public int FieldID { get; set; }
        public int OperatorID { get; set; }
        public String Value { get; set; }
        
    }
}
