import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageSignalRService } from '../services/message-signal-r.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  messageForm:FormGroup;
  constructor(private fb:FormBuilder,private messageSignalRService:MessageSignalRService ) { 
    this.messageForm=this.fb.group({
      'contents':['',Validators.required]
    });
  }
  @Input() roomId:any="";
  ngOnInit(): void {
  }
  sendMessage(){
    console.log(this.roomId,this.messageForm)
    this.messageSignalRService.sendMessageToRoom(this.roomId,this.messageForm.value['contents'])
  }
  ngOnDestroy():void{
    this.messageSignalRService.hubConnection.stop()
  }
}
