﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class SituacaoCliente : BaseEntity
    {
        public string Descricao { get; set; }
    }
}
