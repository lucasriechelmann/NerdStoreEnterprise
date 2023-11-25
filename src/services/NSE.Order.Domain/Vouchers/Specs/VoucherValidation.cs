using NetDevPack.Specification;

namespace NSE.Order.Domain.Vouchers.Specs;
public class VoucherValidation : SpecValidator<Voucher>
{
    public VoucherValidation()
    {
        var dataSpec = new VoucherDataSpecification();
        var qttSpec = new VoucherQuantitySpecification();
        var actSpec = new VoucherActiveSpecification();

        Add("dataSpec", new Rule<Voucher>(dataSpec, "Este voucher está expirado"));
        Add("qttSpec", new Rule<Voucher>(qttSpec, "Este voucher já foi utilizado"));
        Add("actSpec", new Rule<Voucher>(actSpec, "Este voucher não está mais ativo"));
    }
}
