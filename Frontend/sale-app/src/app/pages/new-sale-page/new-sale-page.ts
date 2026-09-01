import { Component, computed, inject, signal } from '@angular/core';
import { SaleService } from '../../services/sale-service';
import { form, FormField, required, validate } from '@angular/forms/signals';
import { CreateSaleRequest } from '../../interfaces/create-sale-request';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-new-sale-page',
  imports: [FormField],
  styleUrl: './new-sale-page.css',
  templateUrl: './new-sale-page.html',
})
export class NewSalePage {
  // Inject sale HTTP service
  private saleService = inject(SaleService);

  // Initial sale state model structure
  private initialSale = {
    customerName: '',
    paymentType: "0",
    total: 0,
    details: [{
      productName: '',
      quantity: 1,
      unitPrice: 0
    }]
  };

  // Signal model representing sale state
  private saleModel = signal( this.initialSale);

  // Form signal with validation rules for sale data
  protected saleForm = form(this.saleModel, (schemaPath) => {
    required(schemaPath.customerName, 
              {message: 'Customer name is required'});
    validate(schemaPath.paymentType, 
              // value the user selected for payment type, if it is '0' then it is invalid
              ({value}) => {
                  // Custom validation for payment type selection
                  if(value().match('0')) 
                    // Return an error object if validation fails
                    return { kind: 'equals', message: 'Payment type is required' 
                };
                // Return null if validation passes
                return null;
              });
  })

  // Method to add a new product row to the sale details
  protected addProduct(): void {
    this.saleForm.details().value.update(current => ([
      ...current,
      { productName: '', 
        quantity: 1, 
        unitPrice: 0 
      }
    ]));
  }

  // Method to remove a product row from the sale details by index
  protected removeProduct(index: number): void {
    this.saleForm.details().value.update(
      // Filter out the product detail at the specified index
      current => current.filter((detail, i) => i !== index)
    ); 
  }

  // Computed signal to calculate total amount from details dynamically
  protected readonly total = computed(() => {
    return this.saleForm.details().value().reduce((sum, detail) => {
      // Calculate the total by summing up the product of quantity and unit price for each detail
     return sum + (detail.quantity * detail.unitPrice);
    }, 0)   
  })

  // Method to save the sale data by sending a request to the SaleService
  protected save(): void {
    // Extract values from the form signal
    const {customerName, paymentType, details} = this.saleForm().value();
    // Create a request object conforming to CreateSaleRequest interface
    const request : CreateSaleRequest = {
      customerName: customerName,
      paymentTypeValue: Number(paymentType),
      total: this.total(),
      details:details
    }
    
    // Call the create method of SaleService and subscribe to the response
    this.saleService.create(request).subscribe({
      next: (response) => {
        if (response.isSuccess) {
          this.saleModel.set(this.initialSale); // Reset the sale model to initial state
          this.saleForm().reset(); // Reset the form state
          this.saleForm.customerName().focusBoundControl(); // Focus on the customer name input field
          // Display a success alert using SweetAlert2
          Swal.fire({
            text: `sale number: ${response.data} registered successfully!`,
            icon: "success"
        });
        } else {
          // Display an error alert if the sale registration failed
          Swal.fire({
            // Show the error message returned from the API response
            text: response.message,
            icon: "error"
          });
        }
      }
    })
  }
}
