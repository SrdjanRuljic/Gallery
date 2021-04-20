using System.Collections.Generic;

namespace Domain.Entities
{
    public class Code
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }

        public Code Parent { get; set; }
        public ICollection<Code> Children { get; set; }

        public ICollection<CodeLanguage> CodesLanguages { get; set; }

        public Code()
        {
            CodesLanguages = new HashSet<CodeLanguage>();
            Children = new HashSet<Code>();
        }
    }
}
