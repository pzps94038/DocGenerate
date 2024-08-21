using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.DatabaseContext
{
    public class DatabaseSetting
    {
        [Key]
        public required Guid UUID { get; set; }
        public required string Name { get; set; }
        public required int DataBaseType { get; set; }
        public required string ConnectionString { get; set; }
        public required DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
