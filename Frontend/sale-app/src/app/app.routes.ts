import { Routes } from '@angular/router';
import { ListSalesPage } from './pages/list-sales-page/list-sales-page';
import { NewSalePage } from './pages/new-sale-page/new-sale-page';
import { DetailSalePage } from './pages/detail-sale-page/detail-sale-page';


export const routes: Routes = [
    // Default route: load sales list component
  { path: '', component:ListSalesPage},

  // Route to create a new sale
  { path: 'new', component: NewSalePage },

  // Route to display single sale details by ID parameter
  { path: 'detail/:id', component: DetailSalePage }
];
