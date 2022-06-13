import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {
  editForm!:FormGroup;
  @Input()userName!:string;
  @Input()description!:string;
  @Input()imgUrl!:string;
  @Output()updateUserEventEmitter=new EventEmitter();
  constructor(private fb:FormBuilder,private userService:UserService) { }

  ngOnInit(): void {
    this.editForm=this.fb.group({
      'userName':[this.userName],
      'description':[this.description],
      'imgUrl':[this.imgUrl]
    })
  }
  edit(){
    let userName=this.editForm.get('userName')?.value==''?null:this.editForm.get('userName')?.value;
    let description=this.editForm.get('description')?.value==''?null:this.editForm.get('description')?.value;
    let imgUrl=this.editForm.get('imgUrl')?.value==''?null:this.editForm.get('imgUrl')?.value;
    this.userService.updateUser(userName,description,imgUrl).subscribe((user)=>this.updateUserEventEmitter.emit({user}));
  }
}
