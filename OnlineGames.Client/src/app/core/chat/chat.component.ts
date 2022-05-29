import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { UserService } from 'src/app/users/services/user.service';
import { IMessage } from '../interfaces/IMessage';
import { MessageSignalRService } from '../services/message-signal-r.service';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  messageForm:FormGroup;
  messages:Array<IMessage>=[]
  @Input() roomId:any="";
  @ViewChild("messageField")inputField? : ElementRef;
  $changeUser!:Observable<any>;
  constructor(private fb:FormBuilder,private messageSignalRService:MessageSignalRService,private userService:UserService,private messageService:MessageService ) { 
    this.messageForm=this.fb.group({
      'contents':['',Validators.required]
    });
    
  }

  ngOnInit(): void {
    this.messageService.getObservableForChangeFriend().subscribe((friendUserName:string)=>
    {
      console.log('change group')
      this.roomId=friendUserName;
      this.messageSignalRService.leaveGroup();
      this.messageSignalRService.joinGroup(this.roomId);
    })
    this.messageSignalRService.joinGroup(this.roomId);
    this.messageSignalRService.receiveMessage((message:IMessage)=>this.messages.push(message));
    
  }
  sendMessage(){
    if(this.messageForm.valid && this.messageForm.value['contents']!=null)
    {
      this.messageSignalRService.sendMessageToRoom(this.roomId,this.messageForm.value['contents']);
      this.messageForm.value['contents']=null;
      this.inputField!.nativeElement.value="";
    }
  }
  ngOnDestroy():void{
    this.messageSignalRService.hubConnection.stop()
  }
}