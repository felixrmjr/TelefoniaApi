using System;

namespace TelefoniaApi.Models
{
    public class Import
    {
        public int LogSistemaId { get; set; }
        public DateTime Data { get; set; }
        public int? LogOrigemId { get; set; }
        public string Severidade { get; set; }
        public string Mensagem { get; set; }
        public string ArquivoFonte { get; set; }
        public string MetodoFonte { get; set; }
        public int? LinhaFonte { get; set; }
        public string Maquina { get; set; }
        public string Propriedades { get; set; }
        public string Excecao { get; set; }
        public int? UsuarioId { get; set; }
        public int? LogContextoId { get; set; }
    }
}

