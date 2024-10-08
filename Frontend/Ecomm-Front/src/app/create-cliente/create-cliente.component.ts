import { Component } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ClienteService } from '../cliente.service';
import { ClienteDTO } from '../Models/ClienteDTO';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-cliente',
  standalone: true,
  imports: [
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    ReactiveFormsModule,
  ],
  templateUrl: './create-cliente.component.html',
  styleUrl: './create-cliente.component.css'
})
export class CreateClienteComponent {

  clienteForm: FormGroup;
  

  constructor(private fb: FormBuilder, private clienteService: ClienteService, private router: Router) {
    this.clienteForm = this.fb.group({
      Name: ['', Validators.required], 
    });
  }

  onSubmit() {
    if (this.clienteForm.valid) {
      console.log('ClienteDTO cadastrado:', this.clienteForm.value);
      const cliente: ClienteDTO = this.clienteForm.value;

      this.clienteService.CreateCliente(cliente).subscribe({
        next: (data) =>{
          this.router.navigate(['/']);
        },
        error(err) {
          console.error('Erro ao adicionar cliente: ', err)
        },
      })
    }
  }
}
