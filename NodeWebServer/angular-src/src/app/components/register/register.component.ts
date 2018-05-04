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
  user:Array<Object> = [];


  constructor(private validateService: ValidateService, private flashMessagesService: FlashMessagesService, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.authService.getAllUsers().subscribe(profiles => {
      //this.user = profile.user;
      var count = 0;
      profiles.forEach(element => {
        this.user[count] = element;
        console.log(this.user[count]);
        count++;
      });
    }, 
    function(err) {
      console.log(err);
      return false;
    });
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
        message.show('User was registered successfully and can now log in', {cssClass:'alert-success', timeout: 3000});
        naviagtion.navigate(['/register']);
        window.location.reload();
      } else {
        message.show('Something went wrong', {cssClass:'alert-danger', timeout: 3000});
        naviagtion.navigate(['/register']);
        
      }
    })

  }

  deleteAccount(user) {
    let naviagtion = this.router;
    console.log(user);
    this.authService.deleteUser({ id: user}).subscribe(function(data) {
      if(data.success){
        console.log("success");
        
        window.location.reload();
      }
      else {
        console.log("not succesful");
      }
    })
  }

}
