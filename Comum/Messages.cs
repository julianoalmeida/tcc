namespace Comum
{
    public static class Messages
    {
        public const string SUCCESSFULLY_INSERTED_RECORD = "Registro <strong>Inserido</strong> com sucesso!";

        public const string SUCCESSFULLY_UPDATED_RECORD = "Registro <strong>Alterado</strong> com sucesso!";

        public const string INVALID_DATE = "Data atual ou futura inválida";

        public const string INVALID_CPF = "CPF Inválido.";

        public const string REQUIRED_FIELD = "O campo {0} é de preenchimento obrigatório";

        public const string INVALID_EMAIL = "Email inválido.";

        public const string COURSE_REQUIRED = "Para salvar um Teacher é necessario vincular ao menos uma disciplina.";

        public const string CLASS_STUDENT_OVERFLOW = "A quantidade de discente vinculados é maior que a quantidade de vagas disponíveis.";

        public const string DUPLICATED_PERSON = "Já existe uma pessoa cadastrada com esse nome e email.";

        public const string DUPLICATED_CLASS = "Turma já cadastrada";

        public const string REQUIRED_FIELDS = "Existem campos de preenchimento obrigatório.";

        public const string USER_NOT_FOUND = "Usuário não encontrado";

        public const string UNAVAILABLE_WS = "O serviço {0} está indisponivel no momento. Favor tentar novamente mais tarde.";
    }
}