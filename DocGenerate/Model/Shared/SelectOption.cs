using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Model.Shared
{
    public class SelectOption
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public SelectOption() { }
        public SelectOption(string? name, string? value)
        {
            Name = name;
            Value = value;
        }
    }
}
