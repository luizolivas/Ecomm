import { TestBed } from '@angular/core/testing';

import { CreateClienteComponentService } from './create-cliente-component.service';

describe('CreateClienteComponentService', () => {
  let service: CreateClienteComponentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CreateClienteComponentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
