import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Cliente } from './Models/Cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  private apiUrl = 'api/Chamado';

  constructor(private http: HttpClient) { }

  CreateCliente(cliente: Cliente){
    return this.http.post<void>(`${this.apiUrl}`, cliente);
  }
}
