using SalesWebApi.Data.Enums;

namespace SalesWebApi.DTOs
{

    #region requests
    public record CreateSaleRequest(
        string CustomerName,
        int PaymentTypeValue,
        decimal Total,
        IEnumerable<CreateSaleDetailRequest> Details);

    public record CreateSaleDetailRequest(
        string ProductName,
        int Quantity,
        decimal UnitPrice);

    public record GetSalesQueryRequest(
        int Page,
        int PageSize);

    #endregion

    #region Responses
    public record GetSaleResponse(
        int SaleId,
        string CustomerName,
        string PaymentTypeName,
        decimal Total,
        string SaleDate,
        IEnumerable<GetSaleDetailResponse>? Details = null);

    public record GetSaleDetailResponse(
        string ProductName,
        int Quantity,
        decimal UnitPrice);

    public record GetSalesQueryResponse(
        IEnumerable<GetSaleResponse> Items,
        int Page,
        int PageSize,
        int TotalItems);

    #endregion
}