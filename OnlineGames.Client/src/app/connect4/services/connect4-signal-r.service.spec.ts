import { TestBed } from '@angular/core/testing';

import { Connect4SignalRService } from './connect4-signal-r.service';

describe('Connect4SignalRService', () => {
  let service: Connect4SignalRService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Connect4SignalRService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
