import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
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
  messages:Array<IMessage>=[];
  page:number=0;
  @Input() roomId:any="";
  @Input() isFriend:boolean=false;
  @ViewChild("messageField")inputField? : ElementRef;
  $changeUser!:Observable<any>;
  constructor(private fb:FormBuilder,private messageSignalRService:MessageSignalRService,private messageService:MessageService ) { 
    this.messageForm=this.fb.group({
      'contents':['',Validators.required]
    });
  }

  ngOnInit(): void {
    this.messageService.getObservableForChangeFriend().subscribe((friendUserName)=>
    {
      this.messages=[];
      this.page=0;
      this.roomId=friendUserName['id'];
      this.messageSignalRService.changeGroup(this.roomId,this.isFriend);
      this.getMessages();
    })
    this.messageSignalRService.joinGroup(this.roomId,this.isFriend);
    this.messageSignalRService.receiveMessage((message:IMessage)=> this.messages=[message].concat(this.messages));
    if(this.isFriend)
    {
      this.getMessages();
    }
  }
  getMessages(){
    return this.messageService.getMessages(this.page,this.roomId).subscribe(data=>this.messages=this.messages.concat(data));
  }
  sendMessage(){
    if(this.messageForm.valid && this.messageForm.value['contents']!=null)
    {
      this.messageSignalRService.sendMessageToRoom(this.roomId,this.messageForm.value['contents'],this.isFriend);
      this.messageForm.value['contents']=null;
      this.inputField!.nativeElement.value="";
    }
  }
  loadMore(){
    this.page++;
    this.getMessages();
  }
  ngOnDestroy():void{
    this.messageSignalRService.hubConnection.stop()
  }
}