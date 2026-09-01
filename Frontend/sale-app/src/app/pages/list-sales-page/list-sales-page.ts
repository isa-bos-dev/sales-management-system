import { Component, effect, inject, signal } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap/pagination';
import { GetSaleResponse } from '../../interfaces/get-sale-response';
import { SaleService } from '../../services/sale-service';
import { GetSalesQueryRequest } from '../../interfaces/get-sales-query-request';

@Component({
  standalone: true,
  imports: [NgbPagination, CurrencyPipe, RouterLink],
  selector: 'app-list-sales-page',
  styleUrl: './list-sales-page.css',
  templateUrl: './list-sales-page.html',
})

// Component class for the sales list page
export class ListSalesPage {
  // the current page number for pagination
  protected currentPage = signal(1);
  // the number of records to display per page
  protected pageSize = signal(10);
  // the total number of records available
  protected totalRecords = signal(0);
  // the list of sales to display on the current page
  protected sales = signal<GetSaleResponse[]>([]);

  // Inject the SaleService to fetch sales data
  private saleService = inject(SaleService);

  constructor() {
    effect(() => {
      // Create a query object with the current page and page size
      const query: GetSalesQueryRequest = {
        page: this.currentPage(),
        pageSize: this.pageSize()
      };

      // Fetch sales data from the service based on the current page and page size
      this.saleService.get(query).subscribe({
        next: (response) => {
          // If the response is successful, update the sales and total records signals
          if (response.isSuccess) {
            // Destructure the response data to get total items and the list of sales
            const {totalItems, items} = response.data;
            this.sales.set(items); // Update the sales signal with the fetched items
            this.totalRecords.set(totalItems); // Update the total records signal with the fetched total items
          }
        },
        // Log any errors that occur during the fetch operation
        error: (e) => { console.log(e.error)}
      }); 
    });
  }

  protected pageChanged(page: number): void {
    this.currentPage.set(page); // Update the current page signal when the page changes
  }
}
