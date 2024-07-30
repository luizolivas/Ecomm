import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PedidosService {
  
  private apiUrl = '/api/pedidoresponse';



  constructor( private http: HttpClient) { }

  getPedidoDetails(id: number): Observable<any[]>{
    const url = `${this.apiUrl}`
    return this.http.get<any[]>(this.apiUrl);
  }
}
