import { TestBed } from '@angular/core/testing';

import { Connect4Resolver } from './connect4.resolver';

describe('Connect4Resolver', () => {
  let resolver: Connect4Resolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(Connect4Resolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
