import { Component, OnInit } from '@angular/core';
import { PedidosService } from '../pedidos.service';
import { CommonModule, NgFor, NgIf, UpperCasePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pedido-details',
  templateUrl: './pedido-details.component.html',
  styleUrl: './pedido-details.component.css',
  standalone: true,
  imports: [CommonModule]
})
export class PedidoDetailsComponent implements OnInit{

  pedido: any[] = []

  constructor(private pedidoService: PedidosService) {}

  ngOnInit(): void{
    this.getPedidoDetails();
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

  // .subscribe((data) =>{
  //   this.pedido = data;
  //   console.log('Detalhes do Pedido:', this.pedido)
  // }, (error) =>{
  //   console.error('Erro ao buscar pedidos:', error)
  // })
}
