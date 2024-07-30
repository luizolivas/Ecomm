import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from './home/home.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, HomeComponent],
  template: `
    <main>
      <header>
        <nav>
          <a routerLink="/">Home</a>
          <a routerLink="/criar-pedido">Criar Pedido</a>
          <a routerLink="/criar-cliente">Criar Cliente</a>
        </nav>
      </header>
      <section class="content">
        <app-home></app-home>
      </section>
    </main>
  `,
})
export class AppComponent {
  title = 'Ecomm-Front';
}
