import { TestBed } from '@angular/core/testing';

import { TicTacToeServiceService } from './tic-tac-toe-service.service';

describe('TicTacToeServiceService', () => {
  let service: TicTacToeServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TicTacToeServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
