﻿namespace HelpDesk.Business.Models
{
    public class Endereco : Entity
    {
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }

        // EF Relations
        public Pessoa Pessoa { get; set; }
    }
}
