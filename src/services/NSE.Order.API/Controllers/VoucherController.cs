using Microsoft.AspNetCore.Mvc;
using NSE.Order.API.Application.DTO;
using NSE.Order.API.Application.Queries;
using NSE.WebAPI.Core.Controllers;
using System.Net;

namespace NSE.Order.API.Controllers;

public class VoucherController : MainController
{
    private readonly IVoucherQueries _voucherQueries;

    public VoucherController(IVoucherQueries voucherQueries)
    {
        _voucherQueries = voucherQueries;
    }

    [HttpGet("voucher/{code}")]
    [ProducesResponseType(typeof(VoucherDTO), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> ObterPorCodigo(string code)
    {
        if (string.IsNullOrEmpty(code)) return NotFound();

        var voucher = await _voucherQueries.GetVoucherByCode(code);

        return voucher == null ? NotFound() : CustomResponse(voucher);
    }
}
