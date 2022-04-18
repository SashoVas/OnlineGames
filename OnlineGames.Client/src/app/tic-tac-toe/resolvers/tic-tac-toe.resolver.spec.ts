import { TestBed } from '@angular/core/testing';

import { TicTacToeResolver } from './tic-tac-toe.resolver';

describe('TicTacToeResolver', () => {
  let resolver: TicTacToeResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(TicTacToeResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
