import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, UserSession } from 'src/services/AuthService';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
  providers: [
    AuthService
  ]
})
export class LoginFormComponent {
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



C# .NET Framework 4.6/DP [Abs. F., Decorator, SP], Dependency injection/SQL Server
AWS[S3, APIGateway, EC2, Lambda]/Nodejs/ASP.NET/C# .NET Framework 4.6/C# REST API/SQL Server
AWS[S3, APIGateway, EC2, Lambda, SSQ, SFTP, Secrets]/Nodejs/C# REST API/SQL Server
ASP.NET/C#/SQL Server.
Microservice/AWS [Lambda, API Gateway, S3, SFTP, FTP, Identity Provider impl.]/Nodejs/xml-js
AWS [S3, Secrets, Lambda, API Gateway]
GIT/Attlassian Software [Jira, Bitbucket, Bamboo, Confluence]/AWS [Code Commit, Code Build]/Team building/SCRUMBAN
