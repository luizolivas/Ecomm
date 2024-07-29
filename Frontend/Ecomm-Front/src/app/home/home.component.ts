import { Component, OnInit } from '@angular/core';
import { PedidosService } from '../pedidos.service';
import { CommonModule } from '@angular/common';
import {MatCardModule} from '@angular/material/card';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatCardModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  
})
export class HomeComponent implements OnInit {

  pedido: any[] = []

  constructor(private pedidoService: PedidosService) {}

  ngOnInit(): void{
    this.getPedidoDetails();
    console.log('AAAAAAAAAA ', this.pedido)
  }

  getPedidoDetails(): void {
    this.pedidoService.getPedidoDetails(23)
      .subscribe({
        next: (data) => {
            this.pedido = data;
            console.log('Detalhes do Pedido:', this.pedido)
        },
        error(err) {
          console.error('Erro ao buscar pedidos:', err)
        },
      })
  }

}
