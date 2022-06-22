using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeClientes
{
    public enum EnumEstadoCivil
    {
        Solteiro,
        Casado,
        Divorciado,
        Viúvo
    }

    
    public class Cliente
    {
        public int Codigo { get; set; }
        public string Nome { get; set;}
        public long CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal RendaMensal { get; set; }
        public EnumEstadoCivil EstadoCivil { get; set; }        
        public bool TemFilhos { get; set; }
        public string Nacionalidade { get; set; }
        public string PlacaVeiculo { get; set; }

        public Cliente(string nome, long cPF, 
            DateTime dataNascimento, 
            decimal rendaMensal, 
            EnumEstadoCivil estadoCivil, 
            bool temFilhos, string nacionalidade, 
            string placaVeiculo)
        {
            this.Codigo = 0;
            this.Nome = nome;
            this.CPF = cPF;
            this.DataNascimento = dataNascimento;
            this.RendaMensal = rendaMensal;
            this.EstadoCivil = estadoCivil;
            this.TemFilhos = temFilhos;
            this.Nacionalidade = nacionalidade;
            this.PlacaVeiculo = placaVeiculo;
        } 
        public Cliente()
        {
            this.Codigo = 0;
        }
        public static List<Cliente> Listagem { get; set; }
    
        public static Cliente Inserir(Cliente cliente)
        {
            int codigo = Cliente.Listagem.Count > 0 ?
            Cliente.Listagem.Max(c => c.Codigo) + 1 : 1;
            cliente.Codigo = codigo;
            Cliente.Listagem.Add(cliente);
            return cliente;
        }

    }
}
