import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClienteDTO } from './Models/ClienteDTO';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  private apiUrl = '/api/Cliente';

  constructor(private http: HttpClient) { }

  CreateCliente(cliente: ClienteDTO){
    return this.http.post<void>(`${this.apiUrl}`, cliente);
  }
}
