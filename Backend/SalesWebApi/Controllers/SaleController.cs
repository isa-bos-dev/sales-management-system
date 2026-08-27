using Microsoft.AspNetCore.Mvc;
using SalesWebApi.Business;
using SalesWebApi.DTOs;

namespace SalesWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SaleController(SaleBusiness bs) : ControllerBase
{
    // POST: api/sale
    [HttpPost]
    public async Task<IActionResult> Create(CreateSaleRequest req)
    {
        var result = await bs.CreateAsync(req);
        return Ok(result);
    }

    // GET: api/sale?page=1&pageSize=5
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetSalesQueryRequest req)
    {
        var result = await bs.GetAllAsync(req);
        return Ok(result);
    }

    // GET: api/sale/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await bs.GetByIdAsync(id);
        return Ok(result);
    }
}