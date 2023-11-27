using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using NSE.Core.Validation;

namespace NSE.WebApp.MVC.Models;

public class OrderTransactionViewModel
{
    public decimal TotalValue { get; set; }
    public decimal Discount { get; set; }
    public string VoucherCode { get; set; }
    public bool VoucherUsed { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
    public AddressViewModel Address { get; set; }
    [Required(ErrorMessage = "Informe o número do cartão")]
    [DisplayName("Número do Cartão")]
    public string CardNumber { get; set; }

    [Required(ErrorMessage = "Informe o nome do portador do cartão")]
    [DisplayName("Nome do Portador")]
    public string CardName { get; set; }

    [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "O vencimento deve estar no padrão MM/AA")]
    [CardExpiration(ErrorMessage = "Cartão Expirado")]
    [Required(ErrorMessage = "Informe o vencimento")]
    [DisplayName("Data de Vencimento MM/AA")]
    public string CardExpiration { get; set; }

    [Required(ErrorMessage = "Informe o código de segurança")]
    [DisplayName("Código de Segurança")]
    public string CardCVV { get; set; }
}
