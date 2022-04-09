import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-board-cell',
  templateUrl: './board-cell.component.html',
  styleUrls: ['./board-cell.component.css']
})
export class BoardCellComponent implements OnInit {
  @Input() row:any;
  @Input() col:any;
  @Input() value:any;
  @Output() makeMove:EventEmitter<number>=new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }
  buttonClick():void{
    this.makeMove.emit();
  }
}
