using Microsoft.EntityFrameworkCore;
using SalesWebApi.Data;
using SalesWebApi.Data.Entities;
using SalesWebApi.Data.Enums;
using SalesWebApi.DTOs;
using System.Globalization;

namespace SalesWebApi.Business
{
    public class SaleBusiness(AppDbContext _db)
    {

        // 1. Create a new sale with validations
        public async Task<ApiResponse<int>> CreateAsync(CreateSaleRequest req)
        {

            // Validate required customer name
            if (string.IsNullOrEmpty(req.CustomerName))
                return ApiResponse<int>.Fail("Customer name is required");

            // Validate allowed payment type enum
            if (!Enum.IsDefined(typeof(PaymentType), req.PaymentTypeValue))
                return ApiResponse<int>.Fail("Invalid payment type");

            // Validate product names inside details list
            var productNameEmpty = req.Details.Any(p => string.IsNullOrEmpty(p.ProductName));
            if (productNameEmpty)
                return ApiResponse<int>.Fail("Product name is required");

            // Map request DTO to database entity
            var dbEntity = new Sale()
            {
                CustomerName = req.CustomerName,
                PaymentType = (PaymentType)req.PaymentTypeValue,
                Total = req.Total,
                Details = req.Details.Select(d => new SaleDetail
                {
                    ProductName = d.ProductName,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList()
            };

            // Add the new sale entity to the database context and save changes
            await _db.Sale.AddAsync(dbEntity);
            await _db.SaveChangesAsync();

            // Return success response with the new sale ID
            return ApiResponse<int>.Success(dbEntity.SaleId);
        }

        // 2. Get paginated list of sales
        public async Task<ApiResponse<GetSalesQueryResponse>> GetAllAsync(GetSalesQueryRequest req)
        {
            // Read-only query without change tracking
            var query = _db.Sale.AsNoTracking(); // select * from Sale

            var totalItems = await query.CountAsync(); // select count(*) from Sale

            // Apply ordering and pagination
            var sales = await query
                .OrderByDescending(c => c.SaleDate)
                .Skip((req.Page - 1) * req.PageSize)
                .Take(req.PageSize)
                .ToListAsync();

            // Map entities to response DTOs
            var formatSales = sales.Select(s => new GetSaleResponse
            (
                SaleId: s.SaleId,
                CustomerName: s.CustomerName,
                PaymentTypeName: Enum.GetName(typeof(PaymentType), s.PaymentType)!,
                Total: s.Total,
                SaleDate: s.SaleDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            )).ToList();

            // Create response with paginated data
            var response = new GetSalesQueryResponse(
               formatSales,
               req.Page,
               req.PageSize,
               totalItems
            );

            // Return successful API response with data
            return ApiResponse<GetSalesQueryResponse>.Success(response);
        }

        // 3. Get single sale details by ID
        public async Task<ApiResponse<GetSaleResponse>> GetByIdAsync(int id)
        {
            // Load sale including related detail items
            var sale = await _db.Sale
                .Include(d => d.Details)
                .AsNoTracking()
                .Where(s => s.SaleId == id)
                .FirstOrDefaultAsync();

            // Check if sale exists
            if (sale == null) return ApiResponse<GetSaleResponse>.Fail("Sale not found");

            // Map sale and nested details collection to response DTO
            var result = new GetSaleResponse(
                SaleId: sale.SaleId,
                CustomerName: sale.CustomerName,
                PaymentTypeName: Enum.GetName(typeof(PaymentType), sale.PaymentType)!,
                Total: sale.Total,
                SaleDate: sale.SaleDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                Details: sale.Details.Select(d => new GetSaleDetailResponse(
                    ProductName: d.ProductName,
                    Quantity: d.Quantity,
                    UnitPrice: d.UnitPrice
                )).ToList()
            );

            // Return successful API response with sale details
            return ApiResponse<GetSaleResponse>.Success(result);
        }
    }
}