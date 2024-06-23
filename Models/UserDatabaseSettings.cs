namespace UserApi.Models
{
    public class UserDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string CadastroCollectionName { get; set; } = null!;

        public string LoginCollectionName { get; set; } = null!;

        public string FeedbackCollectionName { get; set; } = null!;

        public string ContatoCollectionName { get; set; } = null!;

        public string PersonalizacaoUsuarioCollectionName { get; set; } = null!;
    }
}
