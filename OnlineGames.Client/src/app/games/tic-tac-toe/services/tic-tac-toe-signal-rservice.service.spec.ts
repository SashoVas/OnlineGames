import { TestBed } from '@angular/core/testing';

import { TicTacToeSignalRServiceService } from './tic-tac-toe-signal-rservice.service';

describe('TicTacToeSignalRServiceService', () => {
  let service: TicTacToeSignalRServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TicTacToeSignalRServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
