import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AccountService } from 'src/app/core/services/account.service';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements OnInit {
  isLoged:boolean=false;
  constructor(private acountService:AccountService) { }
  ngOnInit(): void {
    this.isLoged=this.acountService.getToken()!=null
  }
}