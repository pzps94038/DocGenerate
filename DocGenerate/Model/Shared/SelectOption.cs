using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Model.Shared
{
    public class SelectOption<T>
    {
        public string? Name { get; set; }
        public T? Value { get; set; }
        public SelectOption() { }
        public SelectOption(string? name, T? value)
        {
            Name = name;
            Value = value;
        }
    }
}
