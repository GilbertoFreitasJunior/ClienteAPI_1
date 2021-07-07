using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProj.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int DDD { get; set; }
        public string Fone { get; set; }
        public string CNPJouCpf { get; set; }
    }
}
