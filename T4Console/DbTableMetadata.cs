using System.Collections.Generic;

namespace T4Console
{
    public class DbTableMetadata
    {
        public string TableName { get; set; }
        public string LegalEntityName { get; set; }
        public List<DbColumnMetadata> Columns { get; set; }
        public string Description { get; set; }
    }
}
