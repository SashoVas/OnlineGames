import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Connect4GameComponent } from './connect4-game.component';

describe('Connect4GameComponent', () => {
  let component: Connect4GameComponent;
  let fixture: ComponentFixture<Connect4GameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ Connect4GameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(Connect4GameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
