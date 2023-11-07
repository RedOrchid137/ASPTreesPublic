import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  @Input()
  username:FormControl
  @Input()
  password:FormControl
  constructor() { }

  ngOnInit() {
    this.username=new FormControl("")
    this.password = new FormControl("")
  }

}
