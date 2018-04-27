import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {FlashMessagesService} from 'angular2-flash-messages';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: String;
  password: String;



  constructor(private authService: AuthService, private router:Router, private flashMessage: FlashMessagesService) { }

  ngOnInit() {
  }

  onLoginSubmit(){
    const user = {
      username: this.username,
      password: this.password
    }
    const flashMessageSvc = this.flashMessage;
    const navigation = this.router;
    const authSvc = this.authService;
    this.authService.authenticateUser(user).subscribe(function(data) {
     
      if(data.success) {
        
        authSvc.storeUserData(data.token, data.user);
        flashMessageSvc.show('You are now logged in', {cssClass: 'alert-success', timeout: 5000});
        navigation.navigate(['dashboard']);



      } else {
        flashMessageSvc.show(data.msg, {cssClass: 'alert-danger', timeout: 5000});
        navigation.navigate(['/']);
      }
    })

  }

}
