import { TestBed } from '@angular/core/testing';

import { CreatePedidoComponentService } from './create-pedido-component.service';

describe('CreatePedidoComponentService', () => {
  let service: CreatePedidoComponentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreatePedidoComponentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
