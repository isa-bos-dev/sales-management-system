import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SaleService } from '../../services/sale-service';
import { GetSaleResponse } from '../../interfaces/get-sale-response';
import { CurrencyPipe} from '@angular/common';
import { MyNavbar } from '../../components/my-navbar/my-navbar';
import Swal from 'sweetalert2';

@Component({
  standalone: true,
  imports: [RouterLink, CurrencyPipe, MyNavbar],
  selector: 'app-detail-sale-page',
  styleUrl: './detail-sale-page.css',
  templateUrl: './detail-sale-page.html',
})
export class DetailSalePage {
  // Inject active route service to retrieve URL route parameters
  private activatedRoute = inject(ActivatedRoute);

  // Inject sale HTTP service
  private saleService = inject(SaleService);

  // Signal holding single sale detail state with initial values
  protected saleModel = signal<GetSaleResponse>({
    saleId: 0,
    customerName: '',
    paymentTypeName: '',
    total: 0,
    saleDate: '',
    details: [{
      productName: '',
      quantity: 1,
      unitPrice: 0
    }]
  });

  constructor() {
    // Read route parameters on component load
    this.activatedRoute.params.subscribe ((params) => {
      this.saleService.getById(params['id']).subscribe({
        next: (response) => {
          // If the response is successful, update the saleModel signal with the fetched sale details
          if (response.isSuccess) {
            this.saleModel.set(response.data);
          }else {
            // If the response is not successful, display an error message using SweetAlert2
            Swal.fire({
              text: response.message, icon: 'error'});
          }
        },
        error: (e) => {
          // Log any errors that occur during the fetch operation
          console.log(e.error);
        }
      }); 
       
    }
  )}
  
}

