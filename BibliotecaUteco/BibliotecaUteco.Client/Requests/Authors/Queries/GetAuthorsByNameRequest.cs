using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BibliotecaUteco.Client.Requests.Authors.Queries
{
    public class GetAuthorsByNameRequest
    {
        [MaxLength(30, ErrorMessage = "El nombre del autor no puede exceder los 30 caracteres.")]
        public string AuthorsName { get; set; } = "";
    }
}