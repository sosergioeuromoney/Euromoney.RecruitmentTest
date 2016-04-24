using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentConsole.Application.Domain
{
    public class NegativeWord
    {
        public string Value { get; set; }

        public NegativeWord(string value) 
        {
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            if(obj is NegativeWord && obj != null)
                return string.Equals(this.Value, ((NegativeWord)obj).Value, StringComparison.CurrentCultureIgnoreCase);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
