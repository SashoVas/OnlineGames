import { Component, OnInit } from '@angular/core';
import { Connect4ServiceService } from '../services/connect4-service.service';

@Component({
  selector: 'app-connect4-game',
  templateUrl: './connect4-game.component.html',
  styleUrls: ['./connect4-game.component.css']
})
export class Connect4GameComponent implements OnInit {
  board:string[][]=[[]];
  constructor(private connect4Service:Connect4ServiceService) {
    this.board=connect4Service.board;

   }

  ngOnInit(): void {
  }
  makeMove(row:number):void{
    this.connect4Service.makeMove(row);
    

  }
}
