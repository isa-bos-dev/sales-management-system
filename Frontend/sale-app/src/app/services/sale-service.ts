import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { ApiResponse } from '../interfaces/api-response';
import { CreateSaleRequest } from '../interfaces/create-sale-request';
import { GetSalesQueryRequest } from '../interfaces/get-sales-query-request';
import { GetSalesQueryResponse } from '../interfaces/get-sales-query-response';
import { GetSaleResponse } from '../interfaces/get-sale-response';

@Injectable({providedIn: 'root'})
export class SaleService {
    // Inject HTTP client for API requests
  private http = inject(HttpClient);

  // Base API endpoint for sales operations
  private endPoint = `${environment.apiUrl}/Sale`;

  // POST: api/Sale - Create a new sale
  create(req: CreateSaleRequest): Observable<ApiResponse<number>> {
    return this.http.post<ApiResponse<number>>(this.endPoint, req);
  }

  // GET: api/Sale?page=X&pageSize=Y - Retrieve paginated sales list
  get(req: GetSalesQueryRequest): Observable<ApiResponse<GetSalesQueryResponse>> {
    return this.http.get<ApiResponse<GetSalesQueryResponse>>(
      `${this.endPoint}?Page=${req.page}&PageSize=${req.pageSize}`
    );
  }

  // GET: api/Sale/{id} - Retrieve single sale details by ID
  getById(id: number): Observable<ApiResponse<GetSaleResponse>> {
    return this.http.get<ApiResponse<GetSaleResponse>>(
        `${this.endPoint}/${id}`);
  }
}
