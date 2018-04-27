import { Component, OnInit } from '@angular/core';
import { ValidateService} from "../../services/validate.service";
import { FlashMessagesService } from "angular2-flash-messages";
import { AuthService } from "../../services/auth.service";
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  name: String;
  username: String;
  email: String;
  password: String;


  constructor(private validateService: ValidateService, private flashMessagesService: FlashMessagesService, private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  onRegisterSubmit(){
    const user = {
      name: this.name,
      email: this.email,
      username: this.username,
      password: this.password
    }

    // Required Fields
    if(!this.validateService.validateRegister(user)) {
      this.flashMessagesService.show('Please fill in all fields', {cssClass:'alert-danger', timeout: 3000});
      return false;
    }

    if(!this.validateService.validateEmail(user.email)) {
      this.flashMessagesService.show('Please fill in email', {cssClass:'alert-danger', timeout: 3000});
      return false;
    }

    // Register User
    let message = this.flashMessagesService;
    let naviagtion = this.router;
    this.authService.registerUser(user).subscribe(function(data) {
      console.log(data);
      if(data.success){
        console.log("success");
        message.show('You are now registered and can log in', {cssClass:'alert-success', timeout: 3000});
        naviagtion.navigate(['/login']);
      } else {
        message.show('Something went wrong', {cssClass:'alert-danger', timeout: 3000});
        naviagtion.navigate(['/register']);
      }
    })

  }

}
