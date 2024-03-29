﻿using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Services.Api.DTOs
{
    public class ChamadoDto
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "O campo Título é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Título precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Titulo { get; set; }
        
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        [MinLength(20, ErrorMessage = "O campo Descrição deve ter no mínimo 20 caracteres.")]
        public string Descricao { get; set; }
        public long IdSituacaoChamado { get; set; }
        public string DescricaoSituacaoChamado { get; set; }
        public Guid IdGerenciador { get; set; }
        public string NomeGerenciador { get; set; }
        public Guid IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public Guid IdUsuarioGerador { get; set; }
        public string NomeUsuarioGerador { get; set; }
        public Guid IdUsuarioResponsavel { get; set; }
        public string NomeUsuarioResponsavel { get; set; }
    }
}
