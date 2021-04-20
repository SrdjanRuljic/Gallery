namespace Domain.Entities
{
    public class CodeLanguage
    {
        public long CodeId { get; set; }
        public long LanguageId { get; set; }
        public string Translation { get; set; }

        public Code Code { get; private set; }
        public Language Language { get; private set; }
    }
}
