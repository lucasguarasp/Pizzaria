namespace Market.ViewModels
{
    public class ViewModelCheckout
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }

        public int Quantidade { get; set; }

        public string Imagem { get; set; }
        public string Descricao { get; set; }
    }
}