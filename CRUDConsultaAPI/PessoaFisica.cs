using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDConsultaAPI
{
    public class PessoaFisica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public double ValorRenda { get; set; }
        public string CPF { get; set; }
    }
}
