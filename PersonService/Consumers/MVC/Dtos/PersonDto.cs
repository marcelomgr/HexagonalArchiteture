namespace MVC.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string? MotherName { get; set; }
        public string? Rg { get; set; }
        public string Cpf { get; set; }
        public string? CondemnedRegister { get; set; }
        public string? CondemnationArticle { get; set; }
        public string? CondemnationProccess { get; set; }
        public string? CondemnationCourt { get; set; }
        public DateTime? CondemnationDate { get; set; }
        public int IdPersonType { get; set; }

        //public int Id { get; set; }
        //public string Name { get; set; }
        //public DateTime Created { get; set; }
        //public string? MotherName { get; set; }
        //public string? Rg { get; set; }
        //public string Cpf { get; set; }

        //public int? Id { get; set; }
        //public string? Name { get; set; }
        //public DateTime? Created { get; set; }
        //public string? MotherName { get; set; }
        //public string? Rg { get; set; }
        //public string? Cpf { get; set; }

        //public string? CondemnedRegister { get; set; }
        //public string? CondemnationArticle { get; set; }
        //public string? CondemnationProccess { get; set; }
        //public string? CondemnationCourt { get; set; }
        //public DateTime? CondemnationDate { get; set; }
        //public int IdPersonType { get; set; }
    }
}
