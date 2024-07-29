import { Routes } from '@angular/router';
import { PedidoDetailsComponent } from './pedido-details/pedido-details.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'pedido/:id', component: PedidoDetailsComponent}
];



