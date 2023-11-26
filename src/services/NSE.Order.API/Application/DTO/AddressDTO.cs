using NSE.Order.Domain.Orders;

namespace NSE.Order.API.Application.DTO;
public class AddressDTO
{
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Complement { get; private set; }
    public string District { get; private set; }
    public string ZipCode { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public static explicit operator AddressDTO(Address address) =>
        new AddressDTO
        {
            Street = address.Street,
            Number = address.Number,
            Complement = address.Complement,
            District = address.District,
            ZipCode = address.ZipCode,
            City = address.City,
            State = address.State
        };
}
