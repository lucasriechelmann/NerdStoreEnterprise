using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;
using NSE.Order.API.Application.Events;
using NSE.Order.Domain.Orders;
using NSE.Order.Domain.Vouchers;
using NSE.Order.Domain.Vouchers.Specs;

namespace NSE.Order.API.Application.Commands;
public class OrderCommandHandler : CommandHandler, IRequestHandler<AddOrderCommand, ValidationResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IVoucherRepository _voucherRepository;

    public OrderCommandHandler(IOrderRepository orderRepository, IVoucherRepository voucherRepository)
    {
        _orderRepository = orderRepository;
        _voucherRepository = voucherRepository;
    }

    public async Task<ValidationResult> Handle(AddOrderCommand message, CancellationToken cancellationToken)
    {
        // Validação do comando
        if (!message.IsValid()) return message.ValidationResult;

        // Mapear Pedido
        var order = MapOrder(message);

        // Aplicar voucher se houver
        if (!await ApplyVoucher(message, order)) return ValidationResult;

        // Validar pedido
        if (!ValidarPedido(order)) return ValidationResult;

        // Processar pagamento
        if (!ProcessarPagamento(order)) return ValidationResult;

        // Se pagamento tudo ok!
        order.AutorizarPedido();

        // Adicionar Evento
        order.AddEvent(new OrderRegisteredEvent(order.Id, order.CustomerId));

        // Adicionar Pedido Repositorio
        _orderRepository.Add(order);

        // Persistir dados de pedido e voucher
        return await PersistData(_orderRepository.UnitOfWork);
    }

    private Domain.Orders.Order MapOrder(AddOrderCommand message)
    {
        var address = new Address
        {
            Street = message.Address.Street,
            Number = message.Address.Number,
            Complement = message.Address.Complement,
            District = message.Address.District,
            ZipCode = message.Address.ZipCode,
            City = message.Address.City,
            State = message.Address.State
        };

        var items = message.OrderItems.Select(x => (OrderItem)x).ToList();

        var order = new Domain.Orders.Order(message.CustomerId, message.TotalValue,
            items, message.VoucherUsed, message.Discount);

        order.ApplyAddress(address);
        return order;
    }

    private async Task<bool> ApplyVoucher(AddOrderCommand message, Domain.Orders.Order order)
    {
        if (!message.VoucherUsed) return true;

        var voucher = await _voucherRepository.GetVoucherByCode(message.VoucherCode);
        if (voucher == null)
        {
            AddError("O voucher informado não existe!");
            return false;
        }

        var voucherValidation = new VoucherValidation().Validate(voucher);
        if (!voucherValidation.IsValid)
        {
            voucherValidation.Errors.ToList().ForEach(m => AddError(m.ErrorMessage));
            return false;
        }

        order.ApplyVoucher(voucher);
        voucher.DebitQuantity();

        _voucherRepository.Update(voucher);

        return true;
    }

    private bool ValidarPedido(Domain.Orders.Order order)
    {
        var orderOriginalValue = order.TotalValue;
        var orderDiscount = order.Discount;

        order.CalculateOrderValue();

        if (order.TotalValue != orderOriginalValue)
        {
            AddError("O valor total do pedido não confere com o cálculo do pedido");
            return false;
        }

        if (order.Discount != orderDiscount)
        {
            AddError("O valor total não confere com o cálculo do pedido");
            return false;
        }

        return true;
    }

    public bool ProcessarPagamento(Domain.Orders.Order order)
    {
        return true;
    }
}
