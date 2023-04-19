import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, UserSession } from 'src/services/AuthService';

@Component({
  selector: 'app-login-form',
  templateUrl: './catFact-home.component.html',
  styleUrls: ['./catFact-home.component.scss'],
  providers: [
    AuthService
  ]
})
export class CatFactHomeComponent {
  token = "Bearer token value";
  form: FormGroup;
  userSession: UserSession | undefined;
  constructor(private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) {
    this.form = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login() {
    console.log("Login method");
    const formValues = this.form.value;
    if (formValues.email && formValues.password) {
      this.authService.login(formValues.email, formValues.password)
        .subscribe(
          (data: UserSession) => {
            this.userSession = { ...data }
            console.log("user is logged in");
            console.log("Login request response body: ", this.userSession.token);
            this.token = this.token + this.userSession.token;
            // this.router.navigateByUrl('/');
          }
        );
    }
  }
}

