import { Component, OnInit } from '@angular/core';
import { ValidateService} from "../../services/validate.service";
import { FlashMessagesService } from "angular2-flash-messages";
import { AuthService } from "../../services/auth.service";
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import {FormBuilder, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";


@Component({
  selector: 'app-assetmanager',
  templateUrl: './assetmanager.component.html',
  styleUrls: ['./assetmanager.component.css']
})
export class AssetmanagerComponent implements OnInit {
  form: FormGroup;
  bundles: Array<Object> = [];

  constructor(private validateService: ValidateService, private flashMessagesService: FlashMessagesService, private authService: AuthService, private router: Router, private fb: FormBuilder) { this.createForm() }

  createForm() {
    this.form = this.fb.group({
      title: ["", String],
      prefab: ["", String],
      assetbundle: null,
      platform: ["", String]
    });
  }

  onFileChange(event) {
    console.log(event.target.files[0]);
    var formLocation = this.form;
    if(event.target.files.length > 0) {
      let file = event.target.files[0];
      formLocation.get('assetbundle').setValue(file);
    }
  }

  private prepareSave(): any {
    let input = new FormData();
    input.append('title', this.form.get('title').value);
    input.append('prefab', this.form.get('prefab').value);
    input.append('assetbundle', this.form.get('assetbundle').value);
    input.append('platform', this.form.get('platform').value);
    console.log(this.form.get('platform').value);
    return input;
  }

  onSubmit() {
    const formModel = this.prepareSave();
    console.log(formModel);
    let message = this.flashMessagesService;
    let naviagtion = this.router;
      this.authService.uploadBundle(formModel).subscribe(function(data) {
        if (data.success) {
          message.show(data.msg, {cssClass:'alert-success', timeout: 3000});
          window.location.reload();
        }
        else {
          message.show(data.msg, {cssClass:'alert-danger', timeout: 3000});
        }
      })
    }

    deleteBundle(id) {
      let naviagtion = this.router;
      let message = this.flashMessagesService;
    this.authService.removeBundle({ id: id}).subscribe(function(data) {
      if(data.success){
        message.show(data.msg, {cssClass:'alert-success', timeout: 3000});
        window.location.reload();
      }
      else {
        message.show(data.msg, {cssClass:'alert-danger', timeout: 3000});
        window.location.reload();
      }
    })
    }
  

  ngOnInit() {
    this.authService.getAllAssetBundles().subscribe(assetbundles => {
      //this.user = profile.user;
      console.log(assetbundles);
      var count = 0;
      assetbundles.bundles.forEach(element => {
        this.bundles[count] = element;
        //console.log(this.bundles[count]);
        count++;
      });
    }, 
    function(err) {
      console.log(err);
      return false;
    });
  }

}
