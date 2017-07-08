namespace EstouroDePilhaAPI.Models
{
    public class RegistrarUsuarioModel : UsuarioBaseModel
    {       
        public string Senha { get; set; }
        public RegistrarUsuarioModel(int id, string nome, string email, string senha, 
            string urlImagemPerfil, string endereco, string descricao) 
            : base(id, nome, email, urlImagemPerfil, endereco,  descricao)
        {
            this.Senha = senha;        
        }
    }
}