using System.Collections.Generic;

namespace Domain.Entities
{
    public class Language
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<CodeLanguage> CodesLanguages { get; set; }

        public Language()
        {
            CodesLanguages = new HashSet<CodeLanguage>();
        }
    }
}
