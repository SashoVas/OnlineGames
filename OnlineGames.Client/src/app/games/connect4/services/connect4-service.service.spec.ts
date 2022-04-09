import { TestBed } from '@angular/core/testing';

import { Connect4ServiceService } from './connect4-service.service';

describe('Connect4ServiceService', () => {
  let service: Connect4ServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Connect4ServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
