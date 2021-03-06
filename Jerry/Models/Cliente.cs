﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jerry.Models
{
    public class Cliente
    {
        [Key]
        public int clienteID { get; set; }

        [Required]
        [Display (Name ="Nombre")]
        public string nombre { get; set; }
        
        [Required]
        [Display(Name ="Apellido Paterno")]
        public string apellidoP { get; set; }

        [Required]
        [Display(Name ="Apellido Materno")]
        public string apellidoM { get; set; }

        [Display(Name = "Cliente")]
        public string nombreCompleto { get { return String.Format("{0} {1} {2}", 
            this.nombre, this.apellidoP, this.apellidoM); } }

        [Required]
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [Display(Name ="Telefono")]
        public string telefono { get; set; }

        //Un cliente tiene una coleccion de reservaciones, es decir, puede tener varias reservaciones
        virtual public ICollection<Evento> eventos { get; set; }
    }
}