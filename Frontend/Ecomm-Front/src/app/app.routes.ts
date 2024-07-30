import { Routes } from '@angular/router';
import { PedidoDetailsComponent } from './pedido-details/pedido-details.component';
import { HomeComponent } from './home/home.component';
import { Component } from '@angular/core';
import { CreatePedidoComponent } from './create-pedido/create-pedido.component';
import { CreateClienteComponent } from './create-cliente/create-cliente.component';


export const routes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'pedido/:id', component: PedidoDetailsComponent},
    { path: 'createpedido', component: CreatePedidoComponent},
    { path: 'createcliente', component: CreateClienteComponent}
];



