import { Routes } from '@angular/router';
import { PedidoDetailsComponent } from './pedido-details/pedido-details.component';

export const routes: Routes = [
    { path: 'pedido/:id', component: PedidoDetailsComponent}
];



