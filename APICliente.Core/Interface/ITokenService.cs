﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICliente.Core.Interface
{
    public interface ITokenService
    {
        string GenerateTokenProdutos(string nome, string permissao);
    }
}
